using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;

namespace TheVoid.TheVoidCode.Powers;

public sealed class VoidClaimedPower : TheVoidPower
{
    public const string Name = nameof(VoidClaimedPower);
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Single;
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<ConsumptionPower>()];

    public override bool TryModifyPowerAmountReceived(PowerModel canonicalPower, Creature target, decimal amount, Creature? applier, out decimal modifiedAmount)
    {
        if (canonicalPower is not ConsumptionPower)
        {
            modifiedAmount = amount;
            return false;
        }

        modifiedAmount = 0m;
        return true;
    }
}