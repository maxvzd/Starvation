using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.GameContent;

namespace SimpleStarvation.Patches;

[HarmonyPatchCategory("simplestarvation")]
public class EntityBehaviourHungerPatches
{
    [HarmonyTranspiler]
    [HarmonyPatch(typeof(EntityBehaviorHunger), "SlowTick")]
    //Removes damage when saturation = 0;
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
    
    [HarmonyTranspiler]
    [HarmonyPatch(typeof(EntityBehaviorHunger), "ReduceSaturation")]
    private static IEnumerable<CodeInstruction> ReduceSaturationTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        var codes = new List<CodeInstruction>(instructions);

        // Identify local index of isondelay (initialized with ldc.i4.0; stloc)
        int? isOnDelayLocal = null;
        for (var i = 0; i < codes.Count - 1; i++)
        {
            if (codes[i].opcode != OpCodes.Ldc_I4_0 || !IsStloc(codes[i + 1], out var idx)) continue;
            
            isOnDelayLocal = idx;
            break;
        }

        // Force isondelay to remain false by rewriting any assignments of true to false
        if (isOnDelayLocal.HasValue)
        {
            for (var i = 0; i < codes.Count - 1; i++)
            {
                if (codes[i].opcode == OpCodes.Ldc_I4_1 && IsStloc(codes[i + 1], out var idx) && idx == isOnDelayLocal.Value)
                {
                    codes[i].opcode = OpCodes.Ldc_I4_0;
                }
            }
        }
        return codes;
    }

    private static bool IsStloc(CodeInstruction ci, out int index)
    {
        index = -1;
        if (ci.opcode == OpCodes.Stloc_0) { index = 0; return true; }
        if (ci.opcode == OpCodes.Stloc_1) { index = 1; return true; }
        if (ci.opcode == OpCodes.Stloc_2) { index = 2; return true; }
        if (ci.opcode == OpCodes.Stloc_3) { index = 3; return true; }
        if (ci.opcode == OpCodes.Stloc_S || ci.opcode == OpCodes.Stloc)
        {
            if (ci.operand is LocalBuilder lb) { index = lb.LocalIndex; return true; }
            if (ci.operand is int idx) { index = idx; return true; }
        }
        return false;
    }
}