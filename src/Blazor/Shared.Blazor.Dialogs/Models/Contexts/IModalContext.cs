namespace Shared.Blazor.Dialogs.Models.Contexts;

public interface IModalContext
{
    Type ModalType { get; }

    string Id { get; }
}
