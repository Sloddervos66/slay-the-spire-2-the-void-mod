using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace TheVoid.TheVoidCode.Powers.OnDeathTriggerPowers;

public sealed class OnDeathDrawPower : OnDeathTriggerPower
{
    public const string Name = nameof(OnDeathDrawPower);
    
    protected override async Task TriggerEffect(PlayerChoiceContext choiceContext)
    {
        var player = Applier?.Player;
        if (player == null) return;
        
        await CardPileCmd.Draw(choiceContext, Amount, player);
    }
}