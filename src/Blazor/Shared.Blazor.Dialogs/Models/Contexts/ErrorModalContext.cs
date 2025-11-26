namespace Shared.Blazor.Dialogs.Models.Contexts;

public sealed record ErrorModalContext(
    string Message,
    string? Title = null,
    string? Details = null,
    Exception? Exception = null,
    string? ConfirmText = null)
    : BaseModalContext<None>
{
    private static readonly Type _modalType = typeof(ErrorModal);

    public override Type ModalType => _modalType;
}
