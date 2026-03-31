using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using TheVoid.TheVoidCode.Powers;

namespace TheVoid.TheVoidCode.Relics.Uncommon;

public sealed class UmbralCarapace : TheVoidRelic
{
    public override RelicRarity Rarity => RelicRarity.Uncommon;
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<BlindPower>()];
    protected override IEnumerable<DynamicVar> CanonicalVars => [new BlockVar(2m, ValueProp.Move)];
    public override async Task AfterPowerAmountChanged(PowerModel power, decimal amount, Creature? applier, CardModel? cardSource)
    {
        if (applier == null) return;
        if (power is not BlindPower) return;
        if (applier != Owner.Creature) return;

        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, null);
    }
}