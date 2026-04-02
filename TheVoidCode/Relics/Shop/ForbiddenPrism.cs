using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using TheVoid.TheVoidCode.Powers;

namespace TheVoid.TheVoidCode.Relics.Shop;

public sealed class ForbiddenPrism : TheVoidRelic
{
    public override RelicRarity Rarity => RelicRarity.Shop;

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromPower<BlindPower>(), 
        HoverTipFactory.FromPower<WeakPower>()
    ];

    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<WeakPower>(2m)];

    public override async Task BeforePowerAmountChanged(PowerModel power, decimal amount, Creature target, Creature? applier, CardModel? cardSource)
    {
        if (power is not BlindPower) return;
        if (Owner.Creature != target) return;

        var enemies = Owner.Creature.CombatState?.Enemies;
        if (enemies == null) return;
        
        Flash();
        foreach (var enemy in enemies)
        {
            await PowerCmd.Apply<BlindPower>(enemy, amount, Owner.Creature, null);
        }

        await PowerCmd.Apply<WeakPower>(Owner.Creature, DynamicVars.Weak.BaseValue, Owner.Creature, null);
    }
    
    public override bool TryModifyPowerAmountReceived(PowerModel canonicalPower, Creature target, decimal amount, Creature? applier, out decimal modifiedAmount)
    {
        if (canonicalPower is not BlindPower || target != Owner.Creature)
        {
            modifiedAmount = amount;
            return false;
        }

        modifiedAmount = 0;
        return true;
    }
}