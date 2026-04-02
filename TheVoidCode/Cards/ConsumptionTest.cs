using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using TheVoid.TheVoidCode.Powers;

namespace TheVoid.TheVoidCode.Cards;

public class ConsumptionTest() : TheVoidCard(0, CardType.Skill, CardRarity.Basic, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<ConsumptionPower>(3m),
        new PowerVar<VoidClaimedPower>(1m)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var target = cardPlay.Target;
        if (target == null) return;

        await PowerCmd.Apply<ConsumptionPower>(target, DynamicVars[ConsumptionPower.Name].BaseValue, Owner.Creature,
            this);
        await PowerCmd.Apply<VoidClaimedPower>(target, DynamicVars[VoidClaimedPower.Name].BaseValue, Owner.Creature,
            this);
    }
}