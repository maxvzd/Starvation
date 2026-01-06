using System.Collections.Generic;
using System.Linq;
using Vintagestory.API.Client;
using Vintagestory.API.Config;
using Vintagestory.API.Datastructures;

namespace Starvation.GUI;

public class BodyWeightGui : GuiDialog
{
    private readonly ITreeAttribute _bodyWeightTree;
    private readonly ITreeAttribute _weightBonusTree;
    public override string ToggleKeyCombinationCode => "BodyWeightGui";
    private IReadOnlyList<LabelViewModel> _statsToWatch;
    
    public BodyWeightGui(ICoreClientAPI coreApi, ElementBounds bounds) : base(coreApi)
    {
        _statsToWatch = new List<LabelViewModel>
        {
            new("Walk Speed", BonusType.WalkSpeed),
            new("Mining Speed", BonusType.MiningSpeed),
            new MaxHealthViewModel("Max Health", BonusType.MaxHealth),
            new("Melee Damage", BonusType.MeleeDamage),
            new("Ranged Damage", BonusType.RangedDamage),
            new("Ranged Accuracy", BonusType.RangeWeaponAccuracy),
            new("Projectile Speed", BonusType.RangedWeaponsSpeed),
            new("Luck", BonusType.RustyGearDropRate),
            new("Tastiness", BonusType.AnimalSeekingRange),
            new("Bow Draw Strength", BonusType.BowDrawStrength),
            new("Glider Lift", BonusType.GliderLiftMax),
            new("Glider Speed", BonusType.GliderSpeedMax),
        };
        
        
        SetupDialog(bounds);
        
        _bodyWeightTree = capi.World.Player.Entity.WatchedAttributes.GetTreeAttribute(EntityBehaviourBodyWeight.BEHAVIOUR_KEY);
        _weightBonusTree = capi.World.Player.Entity.WatchedAttributes.GetTreeAttribute(EntityBehaviourWeightBonuses.BEHAVIOUR_KEY);
    }
    
    public override void OnGuiOpened()
    {
        base.OnGuiOpened();
        
        UpdateWeightText();
        var watchedAttributes = capi.World.Player.Entity.WatchedAttributes;
        watchedAttributes.RegisterModifiedListener(EntityBehaviourBodyWeight.BEHAVIOUR_KEY, UpdateWeightText);
        watchedAttributes.RegisterModifiedListener(EntityBehaviourWeightBonuses.BEHAVIOUR_KEY, UpdateWeightText);
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
        guiElements.Add(new StaticTextElement(ElementBounds.Fixed(0, 0, columnOneWidth, 100), "Bonuses", CairoFont.WhiteSmallText().WithWeight(Cairo.FontWeight.Bold), true));
        guiElements.AddRange(_statsToWatch.Select(viewModel => viewModel.ConstructGui(columnOneWidth, columnTwoWidth)));

        foreach (var element in guiElements)
        {
            if (element.NewLine) yOffset += newLineOffset;
            
            element.SetParent(bgBounds, GuiStyle.ElementToDialogPadding, yOffset);
        }

        SingleComposer = capi.Gui.CreateCompo("bodyweightdialog", dialogBounds)
            .AddShadedDialogBG(bgBounds)
            .AddDialogTitleBar("Body Weight", OnTitleBarCloseClicked)
            .AddSimpleStarvationGuiElements(guiElements)
            .Compose();
    }

    private void UpdateWeightText()
    {
        var weight = _bodyWeightTree.GetFloat("weight");
        SetDynamicText("weightText", $"{weight:0.0} kg");

        foreach (var viewModel in _statsToWatch)
        {
            SetDynamicText(viewModel.Key, viewModel.GetValue(_weightBonusTree));
        }
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
        public virtual void SetParent(ElementBounds parent, double x, double y)
        {
            Bounds
                .WithParent(parent)
                .WithFixedAlignmentOffset(x, y);
        }
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

    private class LabelElement : GuiElement
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

    private class LabelViewModel(string title, BonusType type)
    {
        public string Key => $"{title.Replace(" ", "")}-key";
        
        public virtual string GetValue(ITreeAttribute bonusTree)
        {
            var value = bonusTree.GetFloat(BonusTypeToKey.GetKey(type));
            return $"{value * 100:0}%";
        }

        public LabelElement ConstructGui(double columnOneWidth, double columnTwoWidth)
        {
            return new LabelElement(columnOneWidth, columnTwoWidth,title, Key);
        }
    }

    private class MaxHealthViewModel(string title, BonusType type) : LabelViewModel(title, type)
    {
        public override string GetValue(ITreeAttribute bonusTree)
        {
            var value = bonusTree.GetFloat(BonusTypeToKey.GetKey(type));
            var prefix = string.Empty;
            if (value > 0)
            {
                prefix = "+";
            }
            
            return $"{prefix}{value}";
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

