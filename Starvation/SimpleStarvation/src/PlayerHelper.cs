using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;

namespace Starvation;

public static class PlayerHelper
{
    public static bool IsPlayerInCreative(Entity entity) => entity is EntityPlayer player && IsPlayerInCreative(player);
    public static bool IsPlayerInCreative(EntityPlayer player) => player.World.PlayerByUid(player.PlayerUID).WorldData.CurrentGameMode == EnumGameMode.Creative;
}