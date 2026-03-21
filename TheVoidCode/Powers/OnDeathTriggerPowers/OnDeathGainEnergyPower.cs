using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace TheVoid.TheVoidCode.Powers.OnDeathTriggerPowers;

public sealed class OnDeathGainEnergyPower : OnDeathTriggerPower
{
    public const string Name = nameof(OnDeathGainEnergyPower);
    
    protected override async Task TriggerEffect(PlayerChoiceContext choiceContext)
    {
        var player = Applier?.Player;
        if (player == null) return;
        
        await PlayerCmd.GainEnergy(Amount, player);
    }
}