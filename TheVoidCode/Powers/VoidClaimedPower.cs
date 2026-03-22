using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;

namespace TheVoid.TheVoidCode.Powers;

public sealed class VoidClaimedPower : TheVoidPower
{
    public const string Name = nameof(VoidClaimedPower);
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Single;
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<ConsumptionPower>()];
}