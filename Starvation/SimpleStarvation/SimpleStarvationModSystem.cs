using System;
using System.Linq;
using HarmonyLib;
using Starvation.Config;
using Starvation.GUI;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Server;

namespace Starvation;

public class SimpleStarvationModSystem : ModSystem
{
    private Harmony? _patcher;

    public static SimpleStarvationConfig? Config
    {
        private set;
        get;
    }

    public override void StartServerSide(ICoreServerAPI api)
    {
        base.StartServerSide(api);
        api.RegisterEntityBehaviorClass(EntityBehaviourBodyWeight.ENTITY_KEY, typeof(EntityBehaviourBodyWeight));
        
        if (!Harmony.HasAnyPatches(Mod.Info.ModID))
        {
            _patcher = new Harmony(Mod.Info.ModID);
            _patcher.PatchCategory(Mod.Info.ModID);
        }
        Config = LoadModConfig(api);
    }

    private SimpleStarvationConfig LoadModConfig(ICoreAPICommon api)
    {
        try
        {
            var config = api.LoadModConfig<SimpleStarvationConfig>("SimpleStarvation.json") ?? new SimpleStarvationConfig();

            api.StoreModConfig(config, "SimpleStarvation.json");

            return config;
        }
        catch(Exception e)
        {
            Mod.Logger.Error("Failed to load Simple Starvation Config");
            Mod.Logger.Error(e);
            return new SimpleStarvationConfig();
        }
    }
    
    public override void StartClientSide(ICoreClientAPI api)
    {
        base.StartClientSide(api);

        if(api.Gui.LoadedGuis.FirstOrDefault(x => x is GuiDialogCharacterBase) is not GuiDialogCharacterBase statsDialog) return;
        
        statsDialog.ComposeExtraGuis += () =>
        {
            var composer = statsDialog.Composers["playerstats"];
            if (composer is null) return;
            
            var dialog = new BodyWeightGui(api, composer.Bounds);
                    
            statsDialog.OnOpened += () => dialog.TryOpen();
            statsDialog.OnClosed += () => dialog.TryClose();
        };
    }
    
    public override void Dispose()
    {
        _patcher?.UnpatchAll(Mod.Info.ModID);
    }
}