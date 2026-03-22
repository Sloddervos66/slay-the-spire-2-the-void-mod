using MegaCrit.Sts2.Core.Entities.Creatures;
using TheVoid.TheVoidCode.Powers;

namespace TheVoid.TheVoidCode.Extensions;

public static class CreatureExtensions
{
    public static bool HasBlind(this Creature creature)
    {
        return creature.GetPower<BlindPower>() is { Amount: > 0 };
    }

    public static int GetHpThreshold(this Creature creature, decimal percentage)
    {
        return (int)Math.Floor(creature.MaxHp * (double)percentage / 100.0);
    }
}