using System;
using HarmonyLib;
using Starvation.Config;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Server;

namespace Starvation;

public class SimpleStarvationModSystem : ModSystem
{
    private Harmony? _patcher;
    private GuiDialog? _dialog;

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
        
        _dialog = new BodyWeightGui(api);
        api.Input.RegisterHotKey("SimpleStarvationGui", "Show Body Weight Stats", GlKeys.U, HotkeyType.GUIOrOtherControls);
        api.Input.SetHotKeyHandler("SimpleStarvationGui", ToggleGui);
    }
    
    private bool ToggleGui(KeyCombination comb)
    {
        if (_dialog is null) return true;
        
        if (_dialog.IsOpened())
            _dialog.TryClose();
        else _dialog.TryOpen();

        return true;
    }
    
    public override void Dispose()
    {
        _patcher?.UnpatchAll(Mod.Info.ModID);
    }
}