using System.Diagnostics.CodeAnalysis;

namespace Shared.Blazor.Dialogs.Extensions;

public static class ResultExtensions
{
    extension(ModalResult<ConfirmationResult> result)
    {
        public bool IsConfirmed() => result.IsT0 && result.AsT0.IsT0;
    }

    extension(ModalResult<TextInputResult> result)
    {
        public bool IsTextInput() => result.IsT0 && result.AsT0.IsT0;

        public bool TryGetText([NotNullWhen(true)] out string? text)
        {
            text = null;

            if (result.IsTextInput())
            {
                text = result.AsT0.AsT0.Text;
                return true;
            }

            return text is not null;
        }
    }
}
