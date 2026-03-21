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
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<TunnelVisionPower>()];
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new EnergyVar(1),
        new PowerVar<TunnelVisionPower>(2m)
    ];
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PowerCmd.Apply<TunnelVisionPower>(Owner.Creature, DynamicVars["TunnelVisionPower"].BaseValue, Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["TunnelVisionPower"].UpgradeValueBy(-1m);
    }
}