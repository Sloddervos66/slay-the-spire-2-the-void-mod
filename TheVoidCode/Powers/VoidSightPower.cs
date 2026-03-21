using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;

namespace TheVoid.TheVoidCode.Powers;

public sealed class VoidSightPower : TheVoidPower
{
    public const string Name = nameof(VoidSightPower);
    
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<BlindPower>()];

    public override async Task AfterPowerAmountChanged(PowerModel power, decimal amount, Creature? applier, CardModel? cardSource)
    {
        if (power is not BlindPower) return;
        if (applier != Owner) return;
        if (amount <= 0) return;
        if (Owner.Player == null) return;

        Flash();
        await CardPileCmd.Draw(new BlockingPlayerChoiceContext(), Amount, Owner.Player);
    }
}