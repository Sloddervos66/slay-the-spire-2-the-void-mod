using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using TheVoid.TheVoidCode.Character;
using TheVoid.TheVoidCode.Extensions;

namespace TheVoid.TheVoidCode.Potions.Uncommon;

[Pool(typeof(TheVoidPotionPool))]
public sealed class EssenceOfCollapse : TheVoidPotion
{
    public override PotionRarity Rarity => PotionRarity.Uncommon;
    public override PotionUsage Usage => PotionUsage.CombatOnly;
    public override TargetType TargetType => TargetType.AllEnemies;

    protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {
        var enemies = Owner.Creature.CombatState?.Enemies;
        if (enemies == null) return;

        foreach (var enemy in enemies)
        {
            var onDeathTriggers = enemy.GetOnDeathTriggerPowers();
            foreach (var onDeathTrigger in onDeathTriggers)
            {
                await onDeathTrigger.TriggerEffect(choiceContext);
            }
        }
    }
}