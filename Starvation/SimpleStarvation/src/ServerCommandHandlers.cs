using System.Linq;
using Vintagestory.API.Common;
using Vintagestory.API.Server;

namespace SimpleStarvation;

public class ServerCommandHandlers(ICoreServerAPI serverApi
)
{
    public TextCommandResult SetWeightHandler(TextCommandCallingArgs args)
    {
        var weightBehaviour = args.Caller.Entity.GetBehavior<EntityBehaviourBodyWeight>();
        
        if(weightBehaviour is null || SimpleStarvationModSystem.Config is null) return TextCommandResult.Error("Simply Starving didn't initialise properly");

        var weightArg = args.Parsers.Find(x => x.ArgumentName == "weight")?.GetValue();
        if (weightArg is not float weightAsFloat) return TextCommandResult.Error($"Couldn't parse argument: {weightArg}");
        
        var weightSetTo = weightBehaviour.SetBodyWeight(weightAsFloat);
        return TextCommandResult.Success($"Player weight set to {weightSetTo}");
    }
    
    public TextCommandResult SetPlayerWeightHandler(TextCommandCallingArgs args)
    {
        var weightArg = args.Parsers.Find(x => x.ArgumentName == "weight")?.GetValue();
        if (weightArg is not float weightAsFloat) return TextCommandResult.Error($"Couldn't parse argument: {weightArg}");
        
        var playerArg = args.Parsers.Find(x => x.ArgumentName == "player")?.GetValue();
        if (playerArg is not string playerName) return TextCommandResult.Error($"Couldn't parse argument: {playerArg}");

        var player = serverApi.World.AllOnlinePlayers.FirstOrDefault(x => x.PlayerName == playerName);
        if(player is null) return TextCommandResult.Error($"Couldn't find player: {playerName}");
        
        var weightBehaviour = player.Entity.GetBehavior<EntityBehaviourBodyWeight>();
        if(weightBehaviour is null || SimpleStarvationModSystem.Config is null) return TextCommandResult.Error("Simply Starving didn't initialise properly");

        var weightSetTo = weightBehaviour.SetBodyWeight(weightAsFloat);
        return TextCommandResult.Success($"{playerName}'s weight set to {weightSetTo}");
    }

    public TextCommandResult ClearPlayerTrend(TextCommandCallingArgs args)
    {
        var weightBehaviour = args.Caller.Entity.GetBehavior<EntityBehaviourBodyWeight>();
        weightBehaviour?.ClearAverageGain();
        return TextCommandResult.Success($"Cleared average weight gain");
    }
}