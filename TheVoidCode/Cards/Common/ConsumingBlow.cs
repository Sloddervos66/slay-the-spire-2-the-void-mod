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
        new DamageVar(7m, ValueProp.Move),
        new PowerVar<LastGaspPower>(0m)
    ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => IsUpgraded
        ? [HoverTipFactory.FromPower<LastGaspPower>()]
        : [];

    protected override void AddExtraArgsToDescription(LocString description)
    {
        description.Add(Constants.DescriptionArg.IsUpgraded, IsUpgraded ? $" Apply [blue]{DynamicVars[LastGaspPower.Name].BaseValue}[/blue] [gold]Last Gasp[/gold]." : "");
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
            await PowerCmd.Apply<LastGaspPower>(target, DynamicVars[LastGaspPower.Name].BaseValue, Owner.Creature, this);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(2m);
        DynamicVars[LastGaspPower.Name].UpgradeValueBy(1m);
    }
}