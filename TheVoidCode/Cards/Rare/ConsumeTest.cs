using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using TheVoid.TheVoidCode.Character;
using TheVoid.TheVoidCode.Powers;

namespace TheVoid.TheVoidCode.Cards.Rare;

[Pool(typeof(TheVoidCardPool))]
public class ConsumeTest() : TheVoidCard(0, CardType.Skill, CardRarity.Rare, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<ConsumptionPower>(2m)];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<ConsumptionPower>(), HoverTipFactory.FromPower<VoidClaimedPower>()];
 
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var target = cardPlay.Target;
        if (target == null) return;
 
        await PowerCmd.Apply<ConsumptionPower>(target, DynamicVars[ConsumptionPower.Name].BaseValue, Owner.Creature, this);
        await PowerCmd.Apply<ConsumptionPower>(Owner.Creature, DynamicVars[ConsumptionPower.Name].BaseValue, Owner.Creature, this);
    }
 
    protected override void OnUpgrade()
    {
        DynamicVars[ConsumptionPower.Name].UpgradeValueBy(5m);
    }
}