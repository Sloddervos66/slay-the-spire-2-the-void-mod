using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace TheVoid.TheVoidCode.Powers.OnDeathTriggerPowers;

public abstract class OnDeathTriggerPower : TheVoidPower
{
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterDeath(PlayerChoiceContext choiceContext, Creature creature, bool wasRemovalPrevented, float deathAnimLength)
    {
        if (creature == Owner)
        {
            for (var i = 0; i < Amount; i++)
            {
                await TriggerEffect(choiceContext);
            }
        }
    }
    
    protected abstract Task TriggerEffect(PlayerChoiceContext choiceContext);
}