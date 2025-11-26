namespace Shared.Blazor.Dialogs.Extensions;

public static class ModalServiceExtensions
{
    extension(IModalService modalService)
    {
        public async Task<ModalResult<ConfirmationResult>> ShowConfirmation(
            string message,
            string? title = null,
            string? confirmText = null,
            string? cancelText = null)
            => await modalService.ShowModal<ConfirmationModalContext, ConfirmationResult>(new(message, title, confirmText, cancelText));

        public async Task<ModalResult<TextInputResult>> ShowTextInput(
            string? title = null,
            string? placeholder = null,
            string? initialText = null,
            bool inputCanBeEmpty = true,
            string? confirmText = null)
            => await modalService.ShowModal<TextInputModalContext, TextInputResult>(new(title, placeholder, initialText, inputCanBeEmpty, confirmText));

        public async Task<ModalResult<None>> ShowError(
            string message,
            string? title = null,
            string? details = null,
            Exception? exception = null,
            string? confirmText = null)
            => await modalService.ShowModal<ErrorModalContext, None>(new(message, title, details, exception, confirmText));
    }
}
