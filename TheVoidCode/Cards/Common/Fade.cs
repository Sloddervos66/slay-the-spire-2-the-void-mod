using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using TheVoid.TheVoidCode.Character;
using TheVoid.TheVoidCode.Powers;

namespace TheVoid.TheVoidCode.Cards.Common;

[Pool(typeof(TheVoidCardPool))]
public sealed class Fade() : TheVoidCard(3, CardType.Power, CardRarity.Common, TargetType.Self)
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips => 
    [
        HoverTipFactory.ForEnergy(this), 
        HoverTipFactory.FromPower<BlindPower>()
    ];
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new EnergyVar(1),
        new PowerVar<FadePower>(2m)
    ];
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PowerCmd.Apply<FadePower>(Owner.Creature, DynamicVars[FadePower.Name].BaseValue, Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars[FadePower.Name].UpgradeValueBy(-1m);
    }
}