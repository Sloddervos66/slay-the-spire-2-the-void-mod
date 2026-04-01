using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using TheVoid.TheVoidCode.Extensions;

namespace TheVoid.TheVoidCode.Relics.Rare;

public sealed class GraveConvergence : TheVoidRelic
{
    public override RelicRarity Rarity => RelicRarity.Rare;
    private bool _enemyDied;

    public override async Task AfterDeath(PlayerChoiceContext choiceContext, Creature creature, bool wasRemovalPrevented, float deathAnimLength)
    {
        if (creature.Side != CombatSide.Enemy) return;
        if (_enemyDied) return;
        _enemyDied = true;

        Flash();
        foreach (var trigger in creature.GetOnDeathTriggerPowers())
        {
            await trigger.TriggerEffect(choiceContext);
        }
    }

    public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side == CombatSide.Enemy)
        {
            _enemyDied = false;
        }
        await Task.CompletedTask;
    }
}