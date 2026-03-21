using MegaCrit.Sts2.Core.Entities.Powers;

namespace TheVoid.TheVoidCode.Powers;

public sealed class BloodRitePower : TheVoidPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;
}