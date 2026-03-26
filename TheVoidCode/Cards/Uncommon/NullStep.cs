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
public sealed class NullStep() : TheVoidCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy)
{
    private const string AdditionalBlock = "AdditionalBlock";
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<BlindPower>()];
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new CalculationBaseVar(8m),
        new BlockVar(8m, ValueProp.Move),
        new(AdditionalBlock, 4m),
        new PowerVar<BlindPower>(5m)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
        var target = cardPlay.Target;
        if (target == null) return;
        
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, cardPlay);
        
        if (target.HasBlind())
        {
            var blindAmount = target.GetPowerAmount<BlindPower>();
            var multiplier = Math.Floor(blindAmount / DynamicVars[BlindPower.Name].BaseValue);
            var additionalBlock = DynamicVars[AdditionalBlock].BaseValue * multiplier;
            
            await CreatureCmd.GainBlock(Owner.Creature, additionalBlock, ValueProp.Move, cardPlay);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Block.UpgradeValueBy(2m);
        DynamicVars[BlindPower.Name].UpgradeValueBy(-2m);
    }
}