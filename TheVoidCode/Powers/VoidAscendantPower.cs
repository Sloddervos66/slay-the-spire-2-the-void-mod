using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.ValueProps;
using TheVoid.TheVoidCode.Extensions;
using TheVoid.TheVoidCode.Powers.OnDeathTriggerPowers;

namespace TheVoid.TheVoidCode.Powers;

public class VoidAscendantPower : TheVoidPower
{
    public const string Name = nameof(VoidAscendantPower);

    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromPower<VoidMendPower>(), 
        HoverTipFactory.FromPower<BlindPower>()
    ];

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        var enemies = player.Creature.CombatState?.Enemies
            .Where(e => e.IsAlive)
            .ToList();
        if (enemies is not { Count: > 0 }) return;

        foreach (var e in enemies)
        {
            await PowerCmd.Apply<VoidMendPower>(e, 1, Owner, null);

            if (e.HasBlind())
            {
                var blindStacks = e.GetPower<BlindPower>();
                var blockGain = blindStacks!.Amount / 5;
                
                await CreatureCmd.GainBlock(Owner, Amount * blockGain, ValueProp.Unpowered, null);
            }
        }
    }
}