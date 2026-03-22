using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using TheVoid.TheVoidCode.Character;
using TheVoid.TheVoidCode.Powers;
using TheVoid.TheVoidCode.Powers.OnDeathTriggerPowers;

namespace TheVoid.TheVoidCode.Cards.Rare;

[Pool(typeof(TheVoidCardPool))]
public sealed class VoidAscendant() : TheVoidCard(3, CardType.Power, CardRarity.Rare, TargetType.AllEnemies)
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips => 
    [
        HoverTipFactory.FromPower<VoidMendPower>(),
        HoverTipFactory.FromPower<BlindPower>()
    ];
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<VoidMendPower>(1m),
        new PowerVar<BlindPower>(5m),
        new BlockVar(1m, ValueProp.Move)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
        await PowerCmd.Apply<VoidAscendantPower>(Owner.Creature, DynamicVars.Block.BaseValue, Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars[VoidMendPower.Name].UpgradeValueBy(1m);
        DynamicVars.Block.UpgradeValueBy(1m);
    }
}