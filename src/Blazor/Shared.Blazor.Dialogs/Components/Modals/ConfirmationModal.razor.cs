namespace Shared.Blazor.Dialogs.Components.Modals;

public sealed partial class ConfirmationModal(IJSRuntime jsRuntime)
    : BaseModal<ConfirmationModalContext, ConfirmationResult>(jsRuntime)
{
    private ElementReference _confirmButton = default!;
    private ElementReference _cancelButton = default!;

    private string _confirmText => Context.ConfirmText ?? _localization.OK;

    private string _cancelText => Context.CancelText ?? _localization.Cancel;

    protected override async Task OnModalShown() => await _confirmButton.FocusAsync();

    private async Task OnConfirm() => await Close(new Confirmed());

    private async Task OnCancel() => await Close(new Cancelled());
}
