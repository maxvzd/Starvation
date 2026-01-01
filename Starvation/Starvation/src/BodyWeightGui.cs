using Vintagestory.API.Client;

namespace Starvation;

public class BodyWeightGui : GuiDialog
{
    public override string ToggleKeyCombinationCode => "BodyWeightGui";
    
    public BodyWeightGui(ICoreClientAPI coreApi) : base(coreApi)
    {
        SetupDialog();
    }

    private void SetupDialog()
    {
        var bodyWeight = capi.World.Player?.Entity.WatchedAttributes.GetTreeAttribute("BodyWeight").GetFloat("weight");
        
        // Auto-sized dialog at the center of the screen
        var dialogBounds = ElementStdBounds.AutosizedMainDialog.WithAlignment(EnumDialogArea.CenterMiddle);

        // Just a simple 300x300 pixel box
        var textBounds = ElementBounds.Fixed(0, 40, 300, 100);

        // Background boundaries. Again, just make it fit it's child elements, then add the text as a child element
        var bgBounds = ElementBounds.Fill.WithFixedPadding(GuiStyle.ElementToDialogPadding);
        bgBounds.BothSizing = ElementSizing.FitToChildren;
        bgBounds.WithChildren(textBounds);

        // Lastly, create the dialog
        SingleComposer = capi.Gui.CreateCompo("myAwesomeDialog", dialogBounds)
                .AddShadedDialogBG(bgBounds)
                .AddDialogTitleBar("Body Weight", OnTitleBarCloseClicked)
                .AddDynamicText("Weight: 0 kg", CairoFont.WhiteDetailText(), textBounds, "weightText")
                .Compose();
    }
    
    public override void OnGuiOpened()
    {
        base.OnGuiOpened();
        var attrs = capi.World.Player.Entity.WatchedAttributes;

        attrs.RegisterModifiedListener("bodyweight", () =>
        {
            var weight = attrs.GetFloat("bodyweight", 70f);
            
            if (SingleComposer.GetElement("weightText") is not GuiElementDynamicText weightText) return;
            weightText.SetNewText($"Weight: {weight:0.0} kg");
        });
    }
    
    private void OnTitleBarCloseClicked()
    {
        TryClose();
    }
}