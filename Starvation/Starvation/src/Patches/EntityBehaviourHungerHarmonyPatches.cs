using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.API.Config;
using Vintagestory.GameContent;

namespace Starvation;

[HarmonyPatchCategory("starvation")]
public class EntityBehaviourHungerHarmonyPatches
{
    [HarmonyTranspiler]
    [HarmonyPatch(typeof(EntityBehaviorHunger), "SlowTick")]
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        var receiveDamage = AccessTools.Method(
            typeof(Entity),
            "ReceiveDamage",
            [typeof(DamageSource), typeof(float)]
        );

        foreach (var instr in instructions)
        {
            if (instr.opcode == OpCodes.Callvirt &&
                instr.operand is MethodInfo mi &&
                mi == receiveDamage)
            {
                yield return new CodeInstruction(OpCodes.Pop);
                yield return new CodeInstruction(OpCodes.Pop);
                yield return new CodeInstruction(OpCodes.Pop);

                yield return new CodeInstruction(OpCodes.Ldc_I4_0);

                continue;
            }

            yield return instr;
        }
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(EntityBehaviorHunger), "OnEntityReceiveSaturation")]
    public static void ReceiveSaturationPrefix(EntityBehaviorHunger __instance, float saturation, EnumFoodCategory foodCat = EnumFoodCategory.Unknown, float saturationLossDelay = 10, float nutritionGainMultiplier = 1f)
    {
        //if saturation + currentSaturation is > MaxSat + someThreshold then vomit
        // Lose all current saturation 
    }
    
    // [HarmonyPrefix]
    // [HarmonyPatch(typeof(EntityBehaviorHunger), "ReduceSaturation")]
    // private static void ReduceSaturationPrefix(EntityBehaviorHunger __instance, float satLossMultiplier)
    // {
    //     //Replication of vanilla hunger loss (TODO: Find better way to do this)
    //     satLossMultiplier *= GlobalConstants.HungerSpeedModifier;
    //     satLossMultiplier *= 10;
    //
    //     __instance.entity.GetBehavior<EntityBehaviourBodyWeight>()?.ReduceBodyWeight(satLossMultiplier);
    // } 
    
    // [HarmonyPostfix]
    // [HarmonyPatch(typeof(EntityBehaviorHunger), "ReduceSaturation")]
    // private static void ReduceSaturationPostfix()
    // {
    //     
    // } 
}