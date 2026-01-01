using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.GameContent;

namespace Starvation;

[HarmonyPatchCategory("starvation")]
public class RemoveHungerDamage
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
}