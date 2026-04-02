using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using TheVoid.TheVoidCode.Extensions;
using TheVoid.TheVoidCode.Powers;

namespace TheVoid.TheVoidCode.Relics.Rare;

public sealed class AbyssalThreshold : TheVoidRelic
{
    public override RelicRarity Rarity => RelicRarity.Rare;
    protected override IEnumerable<IHoverTip> ExtraHoverTips => 
    [
        HoverTipFactory.FromPower<BlindPower>(), 
        HoverTipFactory.FromPower<ConsumptionPower>(),
        HoverTipFactory.FromPower<VoidClaimedPower>()
    ];
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<BlindPower>(50m),
        new PowerVar<ConsumptionPower>(3m)
    ];

    public override async Task AfterPowerAmountChanged(PowerModel power, decimal amount, Creature? applier, CardModel? cardSource)
    {
        if (power is not BlindPower) return;

        var target = power.Owner;
        if (!target.IsMonster || 
            !target.HasBlind() || 
            target.HasVoidClaimed() ||
            target.GetPowerAmount<BlindPower>() < DynamicVars[BlindPower.Name].BaseValue
        ) return;
        
        Flash();
        await PowerCmd.Apply<ConsumptionPower>(target, DynamicVars[ConsumptionPower.Name].BaseValue, Owner.Creature, null);
        await PowerCmd.Apply<VoidClaimedPower>(target, 1, Owner.Creature, null);
    }
}