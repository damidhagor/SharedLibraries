namespace Shared.Blazor.Dialogs.Components.Modals.Base;

public abstract class BaseModal<TContext, TResult>(IJSRuntime jsRuntime) : ComponentBase, IAsyncDisposable
    where TContext : BaseModalContext<TResult>
{
    private readonly SemaphoreSlim _lock = new(1, 1);
    private TaskCompletionSource? _modalTransitioningTCS;

    private IJSObjectReference _jsModule = default!;
    private DotNetObjectReference<BaseModal<TContext, TResult>> _modalReference = default!;

    protected readonly IJSRuntime _jsRuntime = jsRuntime;

    [Parameter, EditorRequired]
    public TContext Context { get; set; } = default!;

    [Parameter]
    public bool AllowFullscreen { get; set; }

    [Parameter]
    public EventCallback<TContext> OnClosed { get; set; }

    [JSInvokable]
    public async Task OnBootstrapModalShow() => await OnModalShow();

    [JSInvokable]
    public async Task OnBootstrapModalShown()
    {
        await OnModalShown();
        _modalTransitioningTCS?.TrySetResult();
    }

    [JSInvokable]
    public async Task OnBootstrapModalHide() => await OnModalHide();

    [JSInvokable]
    public async Task OnBootstrapModalHidden()
    {
        await OnModalHidden();
        _modalTransitioningTCS?.TrySetResult();
    }

    [JSInvokable]
    public async Task OnBootstrapModalHidePrevented() => await OnModalHidePrevented();

    public async Task Show()
    {
        try
        {
            await _lock.WaitAsync();

            _modalTransitioningTCS = new();

            await _jsModule.InvokeVoidAsync("showModal", Context.Id);

            await _modalTransitioningTCS.Task;
        }
        finally
        {
            _modalTransitioningTCS = null;
            _lock.Release();
        }
    }

    public async Task Close(TResult result)
    {
        try
        {
            await _lock.WaitAsync();

            _modalTransitioningTCS = new();

            await _jsModule.InvokeVoidAsync("hideModal", Context.Id);

            await _modalTransitioningTCS.Task;

            await InvokeAsync(() => OnClosed.InvokeAsync(Context));

            Context.SetResult(result);
        }
        finally
        {
            _modalTransitioningTCS = null;
            _lock.Release();
        }
    }

    protected virtual Task OnModalShow() => Task.CompletedTask;

    protected virtual Task OnModalShown() => Task.CompletedTask;

    protected virtual Task OnModalHide() => Task.CompletedTask;

    protected virtual Task OnModalHidden() => Task.CompletedTask;

    protected virtual Task OnModalHidePrevented() => Task.CompletedTask;

    protected override async Task OnInitializedAsync()
    {
        _modalReference = DotNetObjectReference.Create(this);

        _jsModule = await _jsRuntime.InvokeAsync<IJSObjectReference>(
            "import",
            "./_content/Shared.Blazor.Dialogs/Components/Modals/Base/BaseModal.razor.js");

        await _jsModule.InvokeVoidAsync("initModal", Context.Id, _modalReference);

        await Show();
    }

    public async ValueTask DisposeAsync()
    {
        _modalReference?.Dispose();

        if (_jsModule is not null)
        {
            try
            {
                await _jsModule.DisposeAsync();
            }
            catch { }
        }

        GC.SuppressFinalize(this);
    }
}
