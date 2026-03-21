using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace TheVoid.TheVoidCode.Powers;

public class BloodRiteUpgradedPower : TheVoidPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;

    public override async Task AfterDamageReceivedLate(
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
        await CardPileCmd.Draw(choiceContext, player);
    }
}