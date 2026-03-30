using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using TheVoid.TheVoidCode.Character;
using TheVoid.TheVoidCode.Powers;

namespace TheVoid.TheVoidCode.Potions.Common;

[Pool(typeof(TheVoidPotionPool))]
public sealed class BottleOfInk : TheVoidPotion
{
    public override PotionRarity Rarity =>  PotionRarity.Common;
    public override PotionUsage Usage => PotionUsage.CombatOnly;
    public override TargetType TargetType => TargetType.AnyEnemy;

    public override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<BlindPower>()];
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<BlindPower>(10m)];

    protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {
        if (target == null) return;
        await PowerCmd.Apply<BlindPower>(target, DynamicVars[BlindPower.Name].BaseValue, Owner.Creature, null);
    }
}