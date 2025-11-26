namespace BlazorDialogs.Components.Modals.Base;

public sealed partial class ModalFooter
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
}
