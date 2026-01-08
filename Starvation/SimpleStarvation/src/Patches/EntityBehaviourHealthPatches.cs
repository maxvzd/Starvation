using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using HarmonyLib;
using Vintagestory.GameContent;

namespace Starvation.Patches;

[HarmonyPatchCategory("simplestarvation")]
public class EntityBehaviourHealthPatches
{
    [HarmonyTranspiler]
    [HarmonyPatch(typeof(EntityBehaviorHealth), "ApplyRegenAndHunger")]
    private static IEnumerable<CodeInstruction> RemoveSaturationDependentHealthRegen(IEnumerable<CodeInstruction> instructions)
    {
        var codes = instructions.ToList();

        var clamp = AccessTools.Method(
            typeof(Vintagestory.API.MathTools.GameMath),
            "Clamp",
            [typeof(float), typeof(float), typeof(float)]
        );

        for (var i = 0; i < codes.Count; i++)
        {
            if (codes[i].Calls(clamp))
            {
                // Remove Clamp call
                codes[i] = new CodeInstruction(OpCodes.Pop); // pop c
                codes.Insert(i + 1, new CodeInstruction(OpCodes.Pop)); // pop b
                codes.Insert(i + 2, new CodeInstruction(OpCodes.Pop)); // pop a

                // Replace stloc.3 with ldloc.3; stloc.3
                if (codes[i + 3].opcode != OpCodes.Stloc_3)
                    throw new InvalidOperationException("Expected stloc.3 after Clamp");

                codes[i + 3] = new CodeInstruction(OpCodes.Ldloc_3);
                codes.Insert(i + 4, new CodeInstruction(OpCodes.Stloc_3));

                break;
            }
        }

        return codes;
    }
}