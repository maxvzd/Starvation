using System.Collections.Generic;
using Vintagestory.API.Client;
using Vintagestory.API.Config;

namespace Starvation.GUI;

public class BodyWeightGui : GuiDialog
{
    public override string ToggleKeyCombinationCode => "BodyWeightGui";
    
    public BodyWeightGui(ICoreClientAPI coreApi, ElementBounds bounds) : base(coreApi)
    {
        SetupDialog(bounds);
    }
    
    public override void OnGuiOpened()
    {
        base.OnGuiOpened();
        
        UpdateWeightText();
        var watchedAttributes = capi.World.Player.Entity.WatchedAttributes;
        watchedAttributes.RegisterModifiedListener(EntityBehaviourBodyWeight.BEHAVIOUR_KEY, UpdateWeightText);
    }

    public override void OnGuiClosed()
    {
        base.OnGuiClosed();
        
        var watchedAttributes = capi.World.Player.Entity.WatchedAttributes;
        watchedAttributes.UnregisterListener(UpdateWeightText);
    }
    
    private void SetupDialog(ElementBounds bounds)
    {
        var dialogBounds = ElementBounds.Fixed(
            (bounds.renderX + bounds.OuterWidth + GuiStyle.DialogToScreenPadding) / RuntimeEnv.GUIScale,
            bounds.renderY / RuntimeEnv.GUIScale,
            bounds.fixedWidth,
            bounds.fixedHeight);

        var bgBounds = ElementBounds.Fill.FlatCopy();

        var guiElements = new List<GuiElement>();
        var yOffset = 45f;
        const float newLineOffset = 20f;

        const float column1Percentage = 0.65f;
        const float colum2Percentage = 1 - column1Percentage;
        
        var columnOneWidth = (bounds.fixedWidth - GuiStyle.ElementToDialogPadding) * column1Percentage;
        var columnTwoWidth = (bounds.fixedWidth - GuiStyle.ElementToDialogPadding) * colum2Percentage;
        
        //Body Weight
        guiElements.Add(new StaticTextElement(ElementBounds.Fixed(0, 0, columnOneWidth, 100), "Nutrition", CairoFont.WhiteSmallText().WithWeight(Cairo.FontWeight.Bold)));
        guiElements.Add(new StaticTextElement(ElementBounds.Fixed(0, 0, columnOneWidth, 100), "Weight", CairoFont.WhiteDetailText(), true));
        guiElements.Add(new DynamicTextElement(ElementBounds.Fixed(columnOneWidth, 0, columnTwoWidth, 100), "weightText", "0 kg", CairoFont.WhiteDetailText()));
        guiElements.Add(new Spacer());
        
        //Effects
        guiElements.Add(new StaticTextElement(ElementBounds.Fixed(0, 0, columnOneWidth, 100), "Physical", CairoFont.WhiteSmallText().WithWeight(Cairo.FontWeight.Bold), true));

        foreach (var element in guiElements)
        {
            if (element.NewLine) yOffset += newLineOffset;
            
            element.Bounds
                .WithParent(bgBounds)
                .WithFixedAlignmentOffset(GuiStyle.ElementToDialogPadding, yOffset);
        }

        SingleComposer = capi.Gui.CreateCompo("bodyweightdialog", dialogBounds)
            .AddShadedDialogBG(bgBounds)
            .AddDialogTitleBar("Body Weight", OnTitleBarCloseClicked)
            .AddSimpleStarvationGuiElements(guiElements)
            .Compose();
    }

    private void UpdateWeightText()
    {
        var watchedAttributes = capi.World.Player.Entity.WatchedAttributes.GetTreeAttribute(EntityBehaviourBodyWeight.BEHAVIOUR_KEY);
        var weight = watchedAttributes.GetFloat("weight");
        
        SetDynamicText("weightText", $"{weight:0.0} kg");
    }

    private void SetDynamicText(string key, string text)
    {
        if (SingleComposer.GetElement(key) is not GuiElementDynamicText weightText) return;
        weightText.SetNewText(text);
    }
    
    private void OnTitleBarCloseClicked()
    {
        TryClose();
    }

    internal abstract class GuiElement(bool newLine) 
    {
        public bool NewLine { get; } = newLine;
        public virtual ElementBounds Bounds { get; } = ElementBounds.Empty;

        public abstract void AddElement(GuiComposer composer);
    }

    private abstract class TextElement(ElementBounds bounds, string text, CairoFont font, bool newLine) : GuiElement(newLine)
    {
        public override ElementBounds Bounds { get; } = bounds;
        public string Text { get; } = text;
        public CairoFont Font { get; } = font;
        
        public abstract override void AddElement(GuiComposer composer);
    }

    private class DynamicTextElement(ElementBounds bounds, string key, string text, CairoFont font, bool newLine = false) : TextElement(bounds, text, font, newLine)
    {
        public string Key { get; } = key;

        public override void AddElement(GuiComposer composer)
        {
            composer.AddDynamicText(Text, Font, Bounds, Key);
        }
    }

    private class StaticTextElement(ElementBounds bounds, string text, CairoFont font, bool newLine = false) : TextElement(bounds, text, font, newLine)
    {
        public override void AddElement(GuiComposer composer)
        {
            composer.AddStaticText(Text, Font, Bounds);
        }
    }

    private class Spacer() : GuiElement(true)
    {
        public override void AddElement(GuiComposer composer)
        {
            //Don't bother adding
        }
    }
}

internal static class ComposerExtension
{
    public static GuiComposer AddSimpleStarvationGuiElements(this GuiComposer composer, IEnumerable<BodyWeightGui.GuiElement> elements)
    {
        foreach (var element in elements) element.AddElement(composer);
        return composer;
    }
}

