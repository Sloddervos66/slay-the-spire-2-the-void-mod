using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace TheVoid.TheVoidCode.Powers;

public sealed class BloodRitePower : TheVoidPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.ForEnergy(this)];

    public override async Task AfterDamageReceived(
        PlayerChoiceContext choiceContext, 
        Creature target,
        DamageResult result, 
        ValueProp props,
        Creature? dealer, 
        CardModel? cardSource)
    {
        var player = Applier?.Player;
        if (player == null) return;
        
        await PlayerCmd.GainEnergy(Amount, player);
    }
}