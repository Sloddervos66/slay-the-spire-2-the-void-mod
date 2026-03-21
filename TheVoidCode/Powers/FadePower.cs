using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;

namespace TheVoid.TheVoidCode.Powers;

public sealed class FadePower : TheVoidPower
{
    public static string Name => nameof(FadePower);
    
    public override PowerType Type => PowerType.None;
    public override PowerStackType StackType => PowerStackType.Counter;
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => 
    [
        HoverTipFactory.ForEnergy(this),
        HoverTipFactory.FromPower<BlindPower>()
    ];

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (player != Owner.Player) return;
        await PowerCmd.Apply<BlindPower>(Owner, Amount, Owner, null);
    }

    public override decimal ModifyMaxEnergy(Player player, decimal amount)
    {
        if (player != Owner.Player)
        {
            return amount;
        }
        return amount + 1;
    }
}