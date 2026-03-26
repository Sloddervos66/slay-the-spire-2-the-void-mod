using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using TheVoid.TheVoidCode.Character;
using TheVoid.TheVoidCode.Localization.DynamicVars;

namespace TheVoid.TheVoidCode.Cards.Uncommon;

[Pool(typeof(TheVoidCardPool))]
public sealed class VoidPact() : TheVoidCard(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new VoidPactVar(15m),
        new EnergyVar(1)
    ];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.ForEnergy(this)];

    protected override void AddExtraArgsToDescription(LocString description)
    {
        var voidPactVar = (VoidPactVar)DynamicVars[VoidPactVar.Name];
        description.Add("TargetSelected", voidPactVar.HasTarget);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var percent = DynamicVars[VoidPactVar.Name].BaseValue / 100m;
        var actualDamage = Owner.Creature.MaxHp * percent;
        
        await CreatureCmd.Damage(choiceContext, Owner.Creature, actualDamage, ValueProp.Unblockable | ValueProp.Unpowered | ValueProp.Move, this);
        await PlayerCmd.GainEnergy(DynamicVars.Energy.BaseValue, Owner);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Energy.UpgradeValueBy(1m);
        
        RemoveKeyword(CardKeyword.Exhaust);
        AddKeyword(CardKeyword.Ethereal);
    }
}