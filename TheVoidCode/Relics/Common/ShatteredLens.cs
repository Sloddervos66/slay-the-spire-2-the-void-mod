using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using TheVoid.TheVoidCode.Extensions;
using TheVoid.TheVoidCode.Powers;

namespace TheVoid.TheVoidCode.Relics.Common;

public sealed class ShatteredLens : TheVoidRelic
{
    public override RelicRarity Rarity => RelicRarity.Common;
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<BlindPower>(2m)
    ];

    public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side == CombatSide.Player) return;

        var combatState = Owner.Creature.CombatState;
        if (combatState == null) return;

        var allCreatures = combatState.Creatures.Where(e => e.HasBlind()).ToList();
        foreach (var target in allCreatures)
        {
            await PowerCmd.Apply<BlindPower>(target, -DynamicVars[BlindPower.Name].BaseValue, Owner.Creature, null);
            if (target.GetPowerAmount<BlindPower>() <= 0)
            {
                await PowerCmd.Remove<BlindPower>(target);
            }
        }
    }
}