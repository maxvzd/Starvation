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
    private readonly IReadOnlyList<LabelViewModel> _statsToWatch;
    
    public BodyWeightGui(ICoreClientAPI coreApi, ElementBounds bounds) : base(coreApi)
    {
        _statsToWatch = new List<LabelViewModel>
        {
            new(BonusType.WalkSpeed),
            new(BonusType.MiningSpeed),
            new MaxHealthViewModel(BonusType.MaxHealth),
            new(BonusType.MeleeDamage),
            new(BonusType.RangedDamage),
            new(BonusType.RangeWeaponAccuracy),
            new(BonusType.RangedWeaponsSpeed),
            new(BonusType.RustyGearDropRate),
            new(BonusType.AnimalSeekingRange),
            new(BonusType.BowDrawStrength),
            new(BonusType.GliderLiftMax),
            new(BonusType.GliderSpeedMax),
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
        guiElements.Add(new StaticTextElement(ElementBounds.Fixed(0, 0, columnOneWidth, 100), Lang.Get("starvation:Nutrition"), CairoFont.WhiteSmallText().WithWeight(Cairo.FontWeight.Bold)));
        guiElements.Add(new StaticTextElement(ElementBounds.Fixed(0, 0, columnOneWidth, 100), Lang.Get("starvation:Weight"), CairoFont.WhiteDetailText(), true));
        guiElements.Add(new DynamicTextElement(ElementBounds.Fixed(columnOneWidth, 0, columnTwoWidth, 100), "weightText", "0 kg", CairoFont.WhiteDetailText()));
        guiElements.Add(new Spacer());
        
        //Effects
        guiElements.Add(new StaticTextElement(ElementBounds.Fixed(0, 0, columnOneWidth, 100), Lang.Get("starvation:Bonuses"), CairoFont.WhiteSmallText().WithWeight(Cairo.FontWeight.Bold), true));
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
}

internal static class ComposerExtension
{
    public static GuiComposer AddSimpleStarvationGuiElements(this GuiComposer composer, IEnumerable<GuiElement> elements)
    {
        foreach (var element in elements) element.AddElement(composer);
        return composer;
    }
}

