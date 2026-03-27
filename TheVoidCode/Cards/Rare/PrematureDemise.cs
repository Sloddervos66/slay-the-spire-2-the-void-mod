using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using TheVoid.TheVoidCode.Character;
using TheVoid.TheVoidCode.Extensions;
using TheVoid.TheVoidCode.Powers.OnDeathTriggerPowers;

namespace TheVoid.TheVoidCode.Cards.Rare;

[Pool(typeof(TheVoidCardPool))]
public sealed class PrematureDemise() : TheVoidCard(3, CardType.Skill, CardRarity.Rare, TargetType.AnyEnemy)
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromPower<LastGaspPower>(),
        HoverTipFactory.FromPower<VoidFlashPower>(),
        HoverTipFactory.FromPower<VoidMendPower>(),
        HoverTipFactory.FromPower<VoidPulsePower>()
    ];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Ethereal];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);

        var target = cardPlay.Target;
        if (target == null) return;

        var onDeathPowers = target.GetOnDeathTriggerPowers().ToList();

        foreach (var power in onDeathPowers)
        {
            await power.TriggerEffect(choiceContext);
            await PowerCmd.Remove(power);
        }
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}