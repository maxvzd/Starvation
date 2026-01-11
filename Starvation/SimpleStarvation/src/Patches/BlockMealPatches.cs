using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using Vintagestory.GameContent;

namespace SimpleStarvation.Patches;

[HarmonyPatchCategory("consumeAllOfMeal")]
public class BlockMealPatches
{
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
}