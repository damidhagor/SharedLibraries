namespace Shared.Blazor.Dialogs.Components.Modals;

public sealed partial class TextInputModal(IJSRuntime jsRuntime)
    : BaseModal<TextInputModalContext, TextInputResult>(jsRuntime)
{
    private ElementReference _input = default!;

    private string _text = "";

    private bool _isConfirmButtonDisabled => string.IsNullOrWhiteSpace(_text) && !Context.InputCanBeEmpty;

    private string _confirmText => Context.ConfirmText ?? _localization.OK;

    protected override void OnParametersSet()
    {
        _text = Context.InitialText ?? "";
        base.OnParametersSet();
    }

    protected override async Task OnModalShown() => await _input.FocusAsync();

    private async Task OnConfirm() => await Close(new TextInput(_text));

    private async Task OnKeyDown(KeyboardEventArgs e)
    {
        if (e.Code is "Enter" or "NumpadEnter"
            && !_isConfirmButtonDisabled)
        {
            await OnConfirm();
        }
    }
}
