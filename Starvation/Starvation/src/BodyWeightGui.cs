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
        UpdateWeightText();
        var watchedAttributes = capi.World.Player.Entity.WatchedAttributes;
        watchedAttributes.RegisterModifiedListener(EntityBehaviourBodyWeight.ENTITY_KEY, UpdateWeightText);
    }

    public override void OnGuiClosed()
    {
        base.OnGuiClosed();
        var watchedAttributes = capi.World.Player.Entity.WatchedAttributes;
        watchedAttributes.UnregisterListener(UpdateWeightText);
    }

    private void UpdateWeightText()
    {
        var watchedAttributes = capi.World.Player.Entity.WatchedAttributes.GetTreeAttribute(EntityBehaviourBodyWeight.ENTITY_KEY);
        var weight = watchedAttributes.GetFloat("weight", 70f);
             
        if (SingleComposer.GetElement("weightText") is not GuiElementDynamicText weightText) return;
        weightText.SetNewText($"Weight: {weight:0.0} kg");
    }
    
    private void OnTitleBarCloseClicked()
    {
        TryClose();
    }
}