namespace BlazorDialogs.Components.Modals.Base;

public sealed partial class ModalBody
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
}
