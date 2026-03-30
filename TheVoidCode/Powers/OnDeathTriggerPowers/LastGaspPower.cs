using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace TheVoid.TheVoidCode.Powers.OnDeathTriggerPowers;

public sealed class LastGaspPower : OnDeathTriggerPower
{
    public const string Name = nameof(LastGaspPower);
    
    public override async Task TriggerEffect(PlayerChoiceContext choiceContext)
    {
        var player = Applier?.Player;
        if (player == null) return;
        
        await CardPileCmd.Draw(choiceContext, Amount, player);

        if (Owner.IsAlive)
        {
            await PowerCmd.Remove(this);
        }
    }
}