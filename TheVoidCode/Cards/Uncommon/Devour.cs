using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using TheVoid.TheVoidCode.Character;
using TheVoid.TheVoidCode.Extensions;
using TheVoid.TheVoidCode.Localization.DynamicVars;

namespace TheVoid.TheVoidCode.Cards.Uncommon;

[Pool(typeof(TheVoidCardPool))]
public sealed class Devour() : TheVoidCard(2, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DamageVar(10m, ValueProp.Move),
        new InstantDeathVar(10m),
        new HealVar(10m)
    ];

    protected override void AddExtraArgsToDescription(LocString description)
    {
        var instantDeathVar = (InstantDeathVar)DynamicVars[InstantDeathVar.Name];
        description.Add("TargetSelected", instantDeathVar.HasTarget);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var target = cardPlay.Target;
        if (target == null) return;

        if (target.CurrentHp <= target.GetHpThreshold(DynamicVars[InstantDeathVar.Name].BaseValue))
        {
            await CreatureCmd.Kill(target);
        }
        else
        {
            await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this).Targeting(target)
                .WithHitFx(DefaultAttackVfx)
                .Execute(choiceContext);
        }
        await CreatureCmd.Heal(Owner.Creature, DynamicVars.Heal.BaseValue);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(3m);
        DynamicVars[InstantDeathVar.Name].UpgradeValueBy(5m);
        DynamicVars.Heal.UpgradeValueBy(3m);
    }
}