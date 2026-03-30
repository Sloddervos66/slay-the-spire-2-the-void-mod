using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace TheVoid.TheVoidCode.Powers.OnDeathTriggerPowers;

public sealed class VoidMendPower : OnDeathTriggerPower
{
    public const string Name = nameof(VoidMendPower);
    
    public override async Task TriggerEffect(PlayerChoiceContext choiceContext)
    {
        var player = Applier?.Player;
        if (player == null) return;

        await CreatureCmd.Heal(player.Creature, Amount);

        if (Owner.IsAlive)
        {
            await PowerCmd.Remove(this);
        }
    }
}