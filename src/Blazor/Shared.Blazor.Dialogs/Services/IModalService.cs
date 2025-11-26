namespace Shared.Blazor.Dialogs.Services;

public interface IModalService
{
    Task<ModalResult<TResult>> ShowModal<TContext, TResult>(TContext context)
        where TContext : BaseModalContext<TResult>;

    void RegisterModalDisplay(ModalDisplay modalDisplay);

    void UnregisterModalDisplay(ModalDisplay modalDisplay);
}
