namespace BlazorDialogs.Models.Contexts;

public sealed record ConfirmationModalContext(
    string Message,
    string? Title = null,
    string? ConfirmText = null,
    string? CancelText = null)
    : BaseModalContext<ConfirmationResult>
{
    private static readonly Type _modalType = typeof(ConfirmationModal);

    public override Type ModalType => _modalType;
}
