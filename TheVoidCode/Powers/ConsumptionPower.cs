using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;

namespace TheVoid.TheVoidCode.Powers;

public sealed class ConsumptionPower : TheVoidPower
{
    public const string Name = nameof(ConsumptionPower);
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<VoidClaimedPower>()];

    public override async Task AfterApplied(Creature? applier, CardModel? cardSource)
    {
        await PowerCmd.Apply<VoidClaimedPower>(Owner, 1, applier, null);
    }

    public override Task BeforeApplied(Creature target, decimal amount, Creature? applier, CardModel? cardSource)
    {
        if (target.HasPower<VoidClaimedPower>() && Amount > 1)
        {
            return Task.CompletedTask;
        }
        return base.BeforeApplied(target, amount, applier, cardSource);
    }

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        await PowerCmd.Decrement(this);

        if (Amount <= 0)
        {
            await CreatureCmd.Kill(Owner);
        }
    }
}