using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using TheVoid.TheVoidCode.Character;
using TheVoid.TheVoidCode.Powers.OnDeathTriggerPowers;

namespace TheVoid.TheVoidCode.Cards.Common;

[Pool(typeof(TheVoidCardPool))]
public sealed class ConsumingBlow() : TheVoidCard(2, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DamageVar(5m, ValueProp.Move),
        new PowerVar<OnDeathDrawPower>(0m)
    ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => IsUpgraded
        ? [HoverTipFactory.FromPower<OnDeathDrawPower>()]
        : [];

    protected override void AddExtraArgsToDescription(LocString description)
    {
        description.Add("IsUpgraded", IsUpgraded ? $" Apply [blue]{DynamicVars[OnDeathDrawPower.Name].BaseValue}[/blue] [gold]Last Gasp[/gold]." : "");
    }
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var target = cardPlay.Target;
        if (target == null) return;

        await DamageCmd.Attack(DynamicVars.Damage.BaseValue).WithHitCount(2).FromCard(this)
            .Targeting(target)
            .WithHitFx(DefaultAttackVfx)
            .Execute(choiceContext);

        if (IsUpgraded)
        {
            await PowerCmd.Apply<OnDeathDrawPower>(target, DynamicVars[OnDeathDrawPower.Name].BaseValue, Owner.Creature, this);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(1m);
        DynamicVars[OnDeathDrawPower.Name].UpgradeValueBy(1m);
    }
}