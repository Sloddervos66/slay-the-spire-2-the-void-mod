using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using TheVoid.TheVoidCode.Character;

namespace TheVoid.TheVoidCode.Cards.Rare;

[Pool(typeof(TheVoidCardPool))]
public sealed class TheVoidStaresBack() : TheVoidCard(4, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new("PercentDamage", 40m),
        new("Multiplier", 3m),
        new CalculationBaseVar(0m),
        new ExtraDamageVar(1m),
        new CalculatedDamageVar(ValueProp.Move).WithMultiplier((card, _) => 
            card.Owner.Creature.CurrentHp * (card.DynamicVars["PercentDamage"].BaseValue / 100m))
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var target = cardPlay.Target;
        if (target == null) return;

        await DamageCmd.Attack(DynamicVars.CalculatedDamage.PreviewValue).WithHitCount(DynamicVars["Multiplier"].IntValue)
            .FromCard(this)
            .Targeting(target)
            .WithHitFx(DefaultAttackVfx)
            .Execute(choiceContext);
        
        await CreatureCmd.Damage(choiceContext, Owner.Creature, DynamicVars.CalculatedDamage.PreviewValue,
            ValueProp.Unblockable | ValueProp.Unpowered | ValueProp.Move, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["Multiplier"].UpgradeValueBy(1m);
    }
}