using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using TheVoid.TheVoidCode.Character;
using TheVoid.TheVoidCode.Extensions;
using TheVoid.TheVoidCode.Powers;

namespace TheVoid.TheVoidCode.Cards.Uncommon;

[Pool(typeof(TheVoidCardPool))]
public sealed class BlackMirror() : TheVoidCard(2, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy)
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<BlindPower>()];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);

        var target = cardPlay.Target;
        if (target == null) return;
        
        if (Owner.Creature.HasBlind())
        {
            var blindAmount = Owner.Creature.GetPowerAmount<BlindPower>();
            await PowerCmd.Apply<BlindPower>(target, blindAmount, Owner.Creature, this);
        }
    }

    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Innate);
    }
}