using HarmonyLib;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Relics;
using TheVoid.TheVoidCode.Relics;

namespace TheVoid.TheVoidCode.Patches;

[HarmonyPatch(typeof(TouchOfOrobas), nameof(TouchOfOrobas.GetUpgradedStarterRelic))]
public static class PatchTouchOfOrobas
{
    public static void Postfix(RelicModel starterRelic, ref RelicModel __result)
    {
        if (starterRelic.Id == ModelDb.Relic<PitchBlack>().Id) __result = ModelDb.Relic<TotalDarkness>().ToMutable();
    }
}