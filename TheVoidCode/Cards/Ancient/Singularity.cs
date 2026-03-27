using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using TheVoid.TheVoidCode.Character;
using TheVoid.TheVoidCode.Extensions;
using TheVoid.TheVoidCode.Powers;

namespace TheVoid.TheVoidCode.Cards.Ancient;

[Pool(typeof(TheVoidCardPool))]
public sealed class Singularity() : TheVoidCard(2, CardType.Attack, CardRarity.Ancient, TargetType.AnyEnemy)
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromPower<ConsumptionPower>(),
        HoverTipFactory.FromPower<VoidClaimedPower>()
    ];
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DamageVar(50m, ValueProp.Move),
        new PowerVar<ConsumptionPower>(10m),
        new PowerVar<VoidClaimedPower>(1m)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var target = cardPlay.Target;
        if (target == null) return;
        
        var combatState = Owner.Creature.CombatState;
        if (combatState == null) return;
        
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this).TargetingAllOpponents(combatState)
            .WithHitFx(DefaultAttackVfx)
            .Execute(choiceContext);

        if (!Owner.Creature.HasVoidClaimed())
        {
            await PowerCmd.Apply<ConsumptionPower>(Owner.Creature, DynamicVars[ConsumptionPower.Name].BaseValue, Owner.Creature, this);
            await PowerCmd.Apply<VoidClaimedPower>(Owner.Creature, DynamicVars[VoidClaimedPower.Name].BaseValue, Owner.Creature, this);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(20m);
        DynamicVars[ConsumptionPower.Name].UpgradeValueBy(-3m);
    }
}