using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace TheVoid.TheVoidCode.Powers;

public sealed class AbyssalGazePower : TheVoidPower
{
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Single;

    public override async Task AfterDeath(PlayerChoiceContext choiceContext, Creature creature, bool wasRemovalPrevented, float deathAnimLength)
    {
        if (!creature.IsEnemy) return;

        var remainingEnemies = Owner.CombatState?.Enemies
            .Where(e => e.IsAlive)
            .ToList();
        if (remainingEnemies is not { Count: > 0 }) return;

        foreach (var e in remainingEnemies)
        {
            await PowerCmd.Apply<BlindPower>(e, Amount, Owner, null);
        }
    }
}