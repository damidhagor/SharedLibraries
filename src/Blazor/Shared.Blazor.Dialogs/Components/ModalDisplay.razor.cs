namespace Shared.Blazor.Dialogs.Components;

public sealed partial class ModalDisplay(IModalService modalService) : IDisposable
{
    private readonly IModalService _modalService = modalService;
    private readonly List<(IModalContext Context, Dictionary<string, object> Parameters)> _modals = [];

    public async Task AddModal<T>(T modalContext)
        where T : IModalContext
    {
        _modals.Add((
            modalContext,
            new()
            {
                { nameof(BaseModal<,>.Context), modalContext },
                { nameof(BaseModal<,>.OnClosed), EventCallback.Factory.Create<T>(this, (context) => RemoveModal(context)) }
            }));

        await InvokeAsync(StateHasChanged);
    }

    private async Task RemoveModal(IModalContext modalContext)
    {
        if (_modals.RemoveAll(m => m.Context == modalContext) > 0)
        {
            await InvokeAsync(StateHasChanged);
        }
    }

    protected override void OnInitialized() => _modalService.RegisterModalDisplay(this);

    public void Dispose() => _modalService.UnregisterModalDisplay(this);
}
