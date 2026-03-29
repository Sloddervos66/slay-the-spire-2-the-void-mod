using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace TheVoid.TheVoidCode.Powers.OnDeathTriggerPowers;

public sealed class VoidFlashPower : OnDeathTriggerPower
{
    public const string Name = nameof(VoidFlashPower);
    public override async Task TriggerEffect(PlayerChoiceContext choiceContext)
    {
        var player = Applier?.Player?.Creature;
        if (player == null) return;
        
        var enemies = Owner.CombatState?.Enemies
            .Where(e => e.IsAlive && e != Owner)
            .ToList();
        if (enemies == null || enemies.Count == 0) return;

        foreach (var e in enemies)
        {
            await PowerCmd.Apply<BlindPower>(e, Amount, player, null);
        }
    }
}