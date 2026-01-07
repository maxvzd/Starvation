using System;
using Vintagestory.API.Client;
using Vintagestory.API.Config;
using Vintagestory.API.Datastructures;

namespace Starvation.GUI;

internal abstract class GuiElement(bool newLine)
{
    public bool NewLine { get; } = newLine;
    public virtual ElementBounds Bounds { get; } = ElementBounds.Empty;

    public abstract void AddElement(GuiComposer composer);

    public virtual void SetParent(ElementBounds parent, double x, double y)
    {
        Bounds
            .WithParent(parent)
            .WithFixedAlignmentOffset(x, y);
    }
}

internal abstract class TextElement(ElementBounds bounds, string text, CairoFont font, bool newLine) : GuiElement(newLine)
{
    public override ElementBounds Bounds { get; } = bounds;
    public string Text { get; } = text;
    public CairoFont Font { get; } = font;

    public abstract override void AddElement(GuiComposer composer);
}

internal class DynamicTextElement(ElementBounds bounds, string key, string text, CairoFont font, bool newLine = false) : TextElement(bounds, text, font, newLine)
{
    public string Key { get; } = key;

    public override void AddElement(GuiComposer composer)
    {
        composer.AddDynamicText(Text, Font, Bounds, Key);
    }
}

internal class StaticTextElement(ElementBounds bounds, string text, CairoFont font, bool newLine = false) : TextElement(bounds, text, font, newLine)
{
    public override void AddElement(GuiComposer composer)
    {
        composer.AddStaticText(Text, Font, Bounds);
    }
}

internal class Spacer() : GuiElement(true)
{
    public override void AddElement(GuiComposer composer)
    {
        //Don't bother adding
    }
}

internal class LabelElement : GuiElement
{
    private readonly StaticTextElement _label;
    private readonly DynamicTextElement _value;

    public LabelElement(double columnOneWidth, double columnTwoWidth, string name, string key) : base(true)
    {
        var labelBounds = ElementBounds.Fixed(0, 0, columnOneWidth, 100);
        var valueBounds = ElementBounds.Fixed(columnOneWidth, 0, columnTwoWidth, labelBounds.fixedHeight);

        _label = new StaticTextElement(labelBounds, name, CairoFont.WhiteDetailText(), true);
        _value = new DynamicTextElement(valueBounds, key, "", CairoFont.WhiteDetailText());
    }

    public override void AddElement(GuiComposer composer)
    {
        _label.AddElement(composer);
        _value.AddElement(composer);
    }

    public override void SetParent(ElementBounds parent, double x, double y)
    {
        _value.Bounds
            .WithParent(_label.Bounds);

        _label.Bounds
            .WithParent(parent)
            .WithFixedAlignmentOffset(x, y);
    }
}

internal class LabelViewModel(BonusType type)
{
    public string Key => $"{Enum.GetName(type)}-key";

    public virtual string GetValue(ITreeAttribute bonusTree)
    {
        var value = bonusTree.GetFloat(Enum.GetName(type));
        return $"{value * 100:0}%";
    }

    public LabelElement ConstructGui(double columnOneWidth, double columnTwoWidth)
    {
        return new LabelElement(columnOneWidth, columnTwoWidth, Lang.Get($"starvation:{Enum.GetName(type)}"), Key);
    }
}

internal class MaxHealthViewModel(BonusType type) : LabelViewModel(type)
{
    public override string GetValue(ITreeAttribute bonusTree)
    {
        var value = bonusTree.GetFloat(Enum.GetName(type));
        var prefix = string.Empty;
        if (value > 0)
        {
            prefix = "+";
        }
        return $"{prefix}{value:0.0}";
    }
}