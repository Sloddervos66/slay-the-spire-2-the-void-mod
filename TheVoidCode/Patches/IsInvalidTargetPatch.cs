using HarmonyLib;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Models;
using TheVoid.TheVoidCode.Interfaces;

namespace TheVoid.TheVoidCode.Patches;

[HarmonyPatch(typeof(CardModel), nameof(CardModel.IsValidTarget))]
public static class IsInvalidTargetPatch
{
    public static void Postfix(CardModel __instance, Creature? target, ref bool __result)
    {
        if (!__result) return;

        if (__instance is ITargetCondition conditional)
        {
            __result = conditional.IsValidTarget(target);
        }
    }
}