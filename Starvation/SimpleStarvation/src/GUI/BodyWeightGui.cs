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
        watchedAttributes.RegisterModifiedListener(EntityBehaviourBodyWeight.ENTITY_KEY, UpdateWeightText);
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

        var textElements = new List<TextElement>();
        var yOffset = 50f;
        const float newLineOffset = 20f;

        const float column1Percentage = 0.65f;
        const float colum2Percentage = 1 - column1Percentage;
        
        var columnOneWidth = (bounds.fixedWidth - GuiStyle.ElementToDialogPadding) * column1Percentage;
        var columnTwoWidth = (bounds.fixedWidth - GuiStyle.ElementToDialogPadding) * colum2Percentage;
        
        textElements.Add(new StaticTextElement(ElementBounds.Fixed(0, 0, columnOneWidth, 100), "weightLabel", "Weight"));
        textElements.Add(new DynamicTextElement(ElementBounds.Fixed(columnOneWidth, 0, columnTwoWidth, 100), "weightText", "0 kg"));

        foreach (var element in textElements)
        {
            if (element.NewLine) yOffset += newLineOffset;
            
            element.Bounds
                .WithParent(bgBounds)
                .WithFixedAlignmentOffset(GuiStyle.ElementToDialogPadding, yOffset);
        }

        SingleComposer = capi.Gui.CreateCompo("bodyweightdialog", dialogBounds)
            .AddShadedDialogBG(bgBounds)
            .AddDialogTitleBar("Body Weight", OnTitleBarCloseClicked)
            .AddTextElements(textElements)
            .Compose();
    }

    private void UpdateWeightText()
    {
        var watchedAttributes = capi.World.Player.Entity.WatchedAttributes.GetTreeAttribute(EntityBehaviourBodyWeight.ENTITY_KEY);
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
    
    internal abstract class TextElement(ElementBounds bounds, string text, bool newLine = false)
    {
        public ElementBounds Bounds { get; } = bounds;
        public string Text { get; } = text;
        public bool NewLine { get; } = newLine;

        public abstract void AddElement(GuiComposer composer);
    }

    private class DynamicTextElement(ElementBounds bounds, string key, string text, bool newLine = false) : TextElement(bounds, text, newLine)
    {
        public string Key { get; } = key;

        public override void AddElement(GuiComposer composer)
        {
            composer.AddDynamicText(Text, CairoFont.WhiteDetailText(), Bounds, Key);
        }
    }

    private class StaticTextElement(ElementBounds bounds, string key, string text, bool newLine = false) : TextElement(bounds, text, newLine)
    {
        public override void AddElement(GuiComposer composer)
        {
            composer.AddStaticText(Text, CairoFont.WhiteDetailText(), Bounds);
        }
    }
}

internal static class ComposerExtension
{
    public static GuiComposer AddTextElements(this GuiComposer composer, IEnumerable<BodyWeightGui.TextElement> elements)
    {
        foreach (var element in elements) element.AddElement(composer);
        return composer;
    }
}

