using MegaCrit.Sts2.Core.Entities.Creatures;
using TheVoid.TheVoidCode.Powers;

namespace TheVoid.TheVoidCode.Extensions;

public static class CreatureExtensions
{
    public static bool HasBlind(this Creature creature)
    {
        return creature.GetPower<BlindPower>() is { Amount: > 0 };
    }
}