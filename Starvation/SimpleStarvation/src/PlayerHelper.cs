using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;

namespace Starvation;

public static class PlayerHelper
{
    public static bool IsPlayerInSurvival(Entity entity) => entity is EntityPlayer player && player.World.PlayerByUid(player.PlayerUID).WorldData.CurrentGameMode is EnumGameMode.Survival;
}