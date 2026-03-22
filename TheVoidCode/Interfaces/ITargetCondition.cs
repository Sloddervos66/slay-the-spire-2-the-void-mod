using MegaCrit.Sts2.Core.Entities.Creatures;

namespace TheVoid.TheVoidCode.Interfaces;

public interface ITargetCondition
{
    bool IsValidTarget(Creature target);
}