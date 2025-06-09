namespace BlazorDialogs.Services;

internal sealed class ModalService : IModalService
{
    private readonly Lock _lock = new();
    private ModalDisplay? _modalDisplay;

    public async Task<ModalResult<TResult>> ShowModal<TContext, TResult>(TContext context)
        where TContext : BaseModalContext<TResult>
    {
        _lock.Enter();

        try
        {
            if (_modalDisplay is null)
            {
                throw new InvalidOperationException("No ModalDisplayComponent is registered.");
            }

            await _modalDisplay.AddModal(context);
        }
        finally
        {
            _lock.Exit();
        }

        return await context.WaitForResult();
    }

    public void RegisterModalDisplay(ModalDisplay modalDisplay)
    {
        lock (_lock)
        {
            if (_modalDisplay is not null && modalDisplay != _modalDisplay)
            {
                throw new InvalidOperationException("A ModalDisplayComponent is already registered.");
            }

            _modalDisplay = modalDisplay;
        }
    }

    public void UnregisterModalDisplay(ModalDisplay modalDisplay)
    {
        lock (_lock)
        {
            if (_modalDisplay is null)
            {
                throw new InvalidOperationException("No ModalDisplayComponent is registered.");
            }

            if (_modalDisplay != modalDisplay)
            {
                throw new InvalidOperationException("The specified ModalDisplayComponent is not registered.");
            }

            _modalDisplay = null;
        }
    }
}
