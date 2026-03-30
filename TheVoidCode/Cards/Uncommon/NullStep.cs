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

namespace TheVoid.TheVoidCode.Cards.Uncommon;

[Pool(typeof(TheVoidCardPool))]
public sealed class NullStep() : TheVoidCard(2, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy)
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<BlindPower>()];
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new BlockVar(8m, ValueProp.Move),
        new(Constants.DynamicVars.AdditionalBlock, 4m),
        new PowerVar<BlindPower>(5m),
        new CalculationBaseVar(8m),
        new CalculationExtraVar(1m),
        new CalculatedBlockVar(ValueProp.Move).WithMultiplier((card, target) =>
        {
            if (target == null) return 0m;
            if (!target.HasBlind()) return 0m;

            var blindAmount = target.GetPowerAmount<BlindPower>();
            var multiplier = Math.Floor(blindAmount / card.DynamicVars[BlindPower.Name].BaseValue);
            
            return card.DynamicVars[Constants.DynamicVars.AdditionalBlock].BaseValue * multiplier;
        })
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, Constants.TriggerAnim.Cast, Owner.Character.CastAnimDelay);
        var target = cardPlay.Target;
        if (target == null) return;
        
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.CalculatedBlock.Calculate(target), ValueProp.Move, cardPlay);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Block.UpgradeValueBy(2m);
        DynamicVars[BlindPower.Name].UpgradeValueBy(-2m);
    }
}