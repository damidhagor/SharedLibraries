namespace Shared.Blazor.Dialogs.Models.Contexts;

public sealed record TextInputModalContext(
    string? Title = null,
    string? Placeholder = null,
    string? InitialText = null,
    bool InputCanBeEmpty = false,
    string? ConfirmText = null)
    : BaseModalContext<TextInputResult>
{
    private static readonly Type _modalType = typeof(TextInputModal);

    public override Type ModalType => _modalType;
}
