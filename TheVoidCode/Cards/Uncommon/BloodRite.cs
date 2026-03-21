using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using TheVoid.TheVoidCode.Character;
using TheVoid.TheVoidCode.Powers;

namespace TheVoid.TheVoidCode.Cards.Uncommon;

[Pool(typeof(TheVoidCardPool))]
public sealed class BloodRite() : TheVoidCard(1, CardType.Power, CardRarity.Rare, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new EnergyVar(1),
        new CardsVar(0)
    ];

    protected override void AddExtraArgsToDescription(LocString description)
    {
        description.Add("IsUpgraded", 
            IsUpgraded
                ? $" and draw [blue]{DynamicVars.Cards.BaseValue}[/blue] card"
                : "");
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (!IsUpgraded)
        {
            await PowerCmd.Apply<BloodRitePower>(Owner.Creature, DynamicVars.Energy.BaseValue, Owner.Creature, this);
            return;
        }

        await PowerCmd.Apply<BloodRiteUpgradedPower>(Owner.Creature, DynamicVars.Cards.BaseValue, Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Cards.UpgradeValueBy(1m);
    }
}