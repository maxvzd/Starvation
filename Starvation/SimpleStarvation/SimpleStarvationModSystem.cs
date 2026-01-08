using System;
using System.Collections.Generic;
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
    private BodyWeightGui? _dialog;

    public static SimplyStarvingConfig? Config { private set; get; }

    public override void StartServerSide(ICoreServerAPI api)
    {
        base.StartServerSide(api);

        Config = LoadModConfig(api);

        if (Config.HealthyWeight > Config.MaxWeight || Config.CriticalWeight > Config.MaxWeight || Config.CriticalWeight > Config.HealthyWeight)
        {
            api.Logger.Error("Simple Starvation config is setup incorrectly, make sure CriticalWeight < HealthyWeight < MaxWeight. Not applying mod.");
            return;
        }
        
        api.RegisterEntityBehaviorClass(EntityBehaviourBodyWeight.BEHAVIOUR_KEY, typeof(EntityBehaviourBodyWeight));
        api.RegisterEntityBehaviorClass(EntityBehaviourWeightBonuses.BEHAVIOUR_KEY, typeof(EntityBehaviourWeightBonuses));

        if (!Harmony.HasAnyPatches(Mod.Info.ModID))
        {
            _patcher = new Harmony(Mod.Info.ModID);
            _patcher.PatchCategory(Mod.Info.ModID);
        }

        CreateCommands(api);

        //Make sure we update the weight bonuses on a player when they switch gamemode so there's no 10sec gap and creative gets updated
        api.Event.PlayerSwitchGameMode += player =>
        {
            player.Entity.GetBehavior<EntityBehaviourWeightBonuses>()?.SetWeightBonuses();
            player.Entity.GetBehavior<EntityBehaviourBodyWeight>()?.ResetTicks();
        };
    }

    private static void CreateCommands(ICoreServerAPI api)
    {
        var commandsHandler = new ServerCommandHandlers(api);

        api.ChatCommands
            .Create("bodyweight")
            .RequiresPrivilege(Privilege.root)
            .RequiresPlayer()
            .WithDescription("Commands for Simple Starvation. Type /help for available commands.")
            
            .BeginSubCommand("set")
            .HandleWith(commandsHandler.SetWeightHandler)
            .WithDescription("Sets your body weight")
            .WithArgs(api.ChatCommands.Parsers.Float("weight"))
            .EndSubCommand()
            
            .BeginSubCommand("playerset")
            .HandleWith(commandsHandler.SetPlayerWeightHandler)
            .WithDescription("Sets the players body weight")
            .WithArgs(api.ChatCommands.Parsers.Word("player"), api.ChatCommands.Parsers.Float("weight"))
            .EndSubCommand();
    }

    private SimplyStarvingConfig LoadModConfig(ICoreAPICommon api)
    {
        try
        {
            var mutableConfig = api.LoadModConfig<MutableConfig>("SimpleStarvation.json") ?? new MutableConfig();
            var config = mutableConfig.Freeze();
            api.StoreModConfig(config, "SimpleStarvation.json");

            return config;
        }
        catch (Exception e)
        {
            Mod.Logger.Error("Failed to load Simple Starvation Config");
            Mod.Logger.Error(e);
            return new MutableConfig().Freeze();
        }
    }

    public override void StartClientSide(ICoreClientAPI api)
    {
        base.StartClientSide(api);
        
        if (api.Gui.LoadedGuis.FirstOrDefault(x => x is GuiDialogCharacterBase) is not GuiDialogCharacterBase statsDialog) return;

        statsDialog.ComposeExtraGuis += () =>
        {
            if (PlayerHelper.IsPlayerInCreative(api.World.Player.Entity))
            {
                _dialog?.Dispose();
                _dialog = null;
                return;
            }
            
            var composer = statsDialog.Composers["playerstats"];
            if (composer is null) return;

            _dialog ??= new BodyWeightGui(api);
            _dialog?.SetupDialog(composer.Bounds);
        };
        statsDialog.OnOpened += OnStatsDialogOpened;
        statsDialog.OnClosed += OnStatsDialogClosed;
    }

    private void OnStatsDialogClosed()
    {
        _dialog?.TryClose();
    }

    private void OnStatsDialogOpened()
    {
        _dialog?.TryOpen();
    }

    public override void Dispose()
    {
        _patcher?.UnpatchAll(Mod.Info.ModID);
    }
}