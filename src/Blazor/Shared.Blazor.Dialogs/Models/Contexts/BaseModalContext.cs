namespace Shared.Blazor.Dialogs.Models.Contexts;

public abstract record BaseModalContext<T> : IModalContext
{
    protected readonly TaskCompletionSource<ModalResult<T>> _tcs = new();

    public abstract Type ModalType { get; }

    public string Id { get; } = Guid.NewGuid().ToString();

    public void SetResult(ModalCancelled result) => _tcs.TrySetResult(result);

    public void SetResult(T result) => _tcs.TrySetResult(result);

    public async Task<ModalResult<T>> WaitForResult() => await _tcs.Task;
}
