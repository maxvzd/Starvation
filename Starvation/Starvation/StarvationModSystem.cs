using HarmonyLib;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Server;

namespace Starvation;

public class StarvationModSystem : ModSystem
{
    private Harmony? _patcher;
    private GuiDialog? _dialog;
    
    public override void Start(ICoreAPI api)
    {
        base.Start(api);
        Globals.CoreApiInstance = api;
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
    }
    
    // public override bool ShouldLoad(EnumAppSide forSide)
    // {
    //     return forSide == EnumAppSide.Client;
    // }
    
    public override void StartClientSide(ICoreClientAPI api)
    {
        base.StartClientSide(api);
        
        _dialog = new BodyWeightGui(api);
        api.Input.RegisterHotKey("BodyWeightGui", "Show Body Weight Stats", GlKeys.U, HotkeyType.GUIOrOtherControls);
        api.Input.SetHotKeyHandler("BodyWeightGui", ToggleGui);
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