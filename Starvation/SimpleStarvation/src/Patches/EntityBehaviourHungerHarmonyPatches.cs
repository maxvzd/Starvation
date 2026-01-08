using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.GameContent;

namespace Starvation.Patches;

[HarmonyPatchCategory("simplestarvation")]
public class EntityBehaviourHungerHarmonyPatches
{
    [HarmonyTranspiler]
    [HarmonyPatch(typeof(EntityBehaviorHunger), "SlowTick")]
    private static IEnumerable<CodeInstruction> HungerSlowTickTranspiler(IEnumerable<CodeInstruction> instructions)
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

    [HarmonyTranspiler]
    [HarmonyPatch(typeof(BlockMeal), nameof(BlockMeal.Consume))]
    // In BlockMeal->Consume(), makes the line
    // float servingsToEat = Math.Min(remainingServings, servingsNeeded);
    // to
    // float servingsToEat = remainingServings;
    private static IEnumerable<CodeInstruction> MealConsumeTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        var codes = new List<CodeInstruction>(instructions);

        var minMethod = AccessTools.Method(
            typeof(Math),
            nameof(Math.Min),
            [typeof(float), typeof(float)]
        );

        for (var i = 0; i < codes.Count; i++)
        {
            // ldarg.s remainingServings
            // ldloc.s servingsNeeded
            // call Math.Min(float, float)
            // stloc.s servingsToEat
            if (codes[i].opcode == OpCodes.Ldarg_S &&
                codes[i + 1].opcode == OpCodes.Ldloc_S &&
                codes[i + 2].Calls(minMethod) &&
                codes[i + 3].opcode == OpCodes.Stloc_S)
            {
                // remove servings needed
                codes[i + 1] = new CodeInstruction(OpCodes.Nop);

                // Remove Math.Min call
                codes[i + 2] = new CodeInstruction(OpCodes.Nop);

                break;
            }
        }
        return codes;
    }

    [HarmonyTranspiler]
    [HarmonyPatch(typeof(EntityBehaviorHunger), nameof(EntityBehaviorHunger.OnEntityReceiveSaturation))]
    // In EntityBehaviorHunger->OnEntityReceiveSaturation(), makes the line
    // Saturation = Math.Min(maxsat, Saturation + saturation);
    // to
    // Saturation += saturation;
    private static IEnumerable<CodeInstruction> HungerReceiveSaturationTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        var codes = new List<CodeInstruction>(instructions);

        var minMethod = AccessTools.Method(
            typeof(Math),
            nameof(Math.Min),
            [typeof(float), typeof(float)]
        );
        
        var getSat = AccessTools.PropertyGetter(
            typeof(EntityBehaviorHunger),
            nameof(EntityBehaviorHunger.Saturation)
        );

        var setSat = AccessTools.PropertySetter(
            typeof(EntityBehaviorHunger),
            nameof(EntityBehaviorHunger.Saturation)
        );

        for (var i = 0; i < codes.Count - 6; i++)
        {
            if (codes[i].opcode == OpCodes.Ldarg_0 &&
                codes[i + 1].opcode == OpCodes.Ldloc_0 &&
                codes[i + 2].opcode == OpCodes.Ldarg_0 &&
                codes[i + 3].Calls(getSat) &&
                codes[i + 4].opcode == OpCodes.Ldarg_1 &&
                codes[i + 5].opcode == OpCodes.Add &&
                codes[i + 6].Calls(minMethod) &&
                codes[i + 7].Calls(setSat))
            {
                // Remove maxsat load
                codes[i + 1].opcode = OpCodes.Nop;

                // Remove Math.Min
                codes[i + 6].opcode = OpCodes.Nop;

                break;
            }
        }

        return codes;
    }
    
    [HarmonyPostfix]
    [HarmonyPatch(typeof(EntityBehaviorHunger), "OnEntityReceiveSaturation")]
    private static void ReceiveSaturationPostfix(EntityBehaviorHunger __instance)
    {
        var bodyWeight = __instance.entity.GetBehavior<EntityBehaviourBodyWeight>();
        bodyWeight?.CheckForThrowUp();
    }

    //Remove satuaration loss
    [HarmonyPrefix]
    [HarmonyPatch(typeof(EntityBehaviorHunger), "OnEntityReceiveSaturation")]
    private static void ReceiveSaturationPrefix(ref float saturationLossDelay)
    {
        saturationLossDelay = 0f;
    }
}