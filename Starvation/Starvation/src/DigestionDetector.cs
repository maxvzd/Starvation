// using HarmonyLib;
// using Vintagestory.GameContent;
//
// namespace Starvation;
//
// //[HarmonyPatchCategory("starvation")]
// //[HarmonyPatch(typeof(EntityBehaviorHunger), "OnGameTick")]
// public class DigestionDetector
// {
//     // private static float _hungerCounter = 0f;
//     // private static float _saturationBeforeLoss;
//     // private static float _bodyWeight = 2000f;
//     
//     [HarmonyPrefix]
//     private static bool Prefix(EntityBehaviorHunger __instance, float deltaTime)
//     {
//         // _hungerCounter += deltaTime;
//         // if (_hungerCounter > 10)
//         // {
//         //     _saturationBeforeLoss = __instance.Saturation;
//         // }
//         
//         //__instance.entity.GetBehavior<EntityBehaviorBodyWeight>();
//         return true;
//     }
//     
//     [HarmonyPostfix]
//     private static void Postfix(EntityBehaviorHunger __instance)
//     {
//         // if (!(_hungerCounter > 10)) return;
//         //
//         // var satDiff = _saturationBeforeLoss - __instance.Saturation;
//         // if (satDiff > 0)
//         // {
//         //     //As saturation goes down we add to the body-weight and "digest" the food
//         //     _bodyWeight = Math.Max(_bodyWeight + satDiff, 0);
//         //     Globals.CoreApiInstance?.Logger.Debug($"Digesting food and adding to body weight by {satDiff}");
//         // }
//         // _hungerCounter = 0f;
//     }
// }