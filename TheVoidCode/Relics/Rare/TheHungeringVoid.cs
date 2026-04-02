using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using TheVoid.TheVoidCode.Powers;

namespace TheVoid.TheVoidCode.Relics.Rare;

public sealed class TheHungeringVoid : TheVoidRelic
{
    public override RelicRarity Rarity => RelicRarity.Rare;
    protected override IEnumerable<IHoverTip> ExtraHoverTips => 
    [
        HoverTipFactory.FromPower<BlindPower>(), 
        HoverTipFactory.FromPower<StrengthPower>()
    ];

    public override async Task AfterPowerAmountChanged(PowerModel power, decimal amount, Creature? applier, CardModel? cardSource)
    {
        if (power is not BlindPower) return;
        if (Owner.Creature != applier) return;
        if (Owner.Creature != power.Owner) return;
        
        await PowerCmd.Apply<TheHungeringVoidStrengthPower>(Owner.Creature, amount, Owner.Creature, null);
    }
}