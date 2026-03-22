using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using TheVoid.TheVoidCode.Character;
using TheVoid.TheVoidCode.Interfaces;
using TheVoid.TheVoidCode.Powers;

namespace TheVoid.TheVoidCode.Cards.Rare;

[Pool(typeof(TheVoidCardPool))]
public class ConsumeTest() : TheVoidCard(0, CardType.Skill, CardRarity.Rare, TargetType.AnyEnemy), ITargetCondition
{
    protected override IEnumerable<DynamicVar> CanonicalVars => 
    [
        new PowerVar<ConsumptionPower>(2m),
        new PowerVar<VoidClaimedPower>(1m)
    ];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<ConsumptionPower>(), HoverTipFactory.FromPower<VoidClaimedPower>()];
    public new bool IsValidTarget(Creature target) => !target.HasPower<VoidClaimedPower>();

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var target = cardPlay.Target;
        if (target == null) return;
        
        await PowerCmd.Apply<VoidClaimedPower>(target, DynamicVars[VoidClaimedPower.Name].BaseValue, Owner.Creature, this);
        await PowerCmd.Apply<ConsumptionPower>(target, DynamicVars[ConsumptionPower.Name].BaseValue, Owner.Creature, this);
        
        await PowerCmd.Apply<VoidClaimedPower>(Owner.Creature, DynamicVars[VoidClaimedPower.Name].BaseValue, Owner.Creature, this);
        await PowerCmd.Apply<ConsumptionPower>(Owner.Creature, DynamicVars[ConsumptionPower.Name].BaseValue, Owner.Creature, this);
    }
 
    protected override void OnUpgrade()
    {
        DynamicVars[ConsumptionPower.Name].UpgradeValueBy(5m);
    }
    
    
}