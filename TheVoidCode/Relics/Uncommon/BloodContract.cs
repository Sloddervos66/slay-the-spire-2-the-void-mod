using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace TheVoid.TheVoidCode.Relics.Uncommon;

public sealed class BloodContract : TheVoidRelic
{
    public override RelicRarity Rarity => RelicRarity.Uncommon;
    protected override IEnumerable<DynamicVar> CanonicalVars => [new CardsVar(2)];
    private bool _hasDrawn;

    public override async Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult result, ValueProp props, Creature? dealer, CardModel? cardSource)
    {
        if (target == Owner.Creature && result.UnblockedDamage > 0 && target.CombatState?.CurrentSide == Owner.Creature.Side)
        {
            if (cardSource == null) return;
            if (_hasDrawn) return;
            
            await CardPileCmd.Draw(choiceContext, DynamicVars.Cards.BaseValue, Owner);
            _hasDrawn = true;
        }
    }

    public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side == CombatSide.Enemy)
        {
            _hasDrawn = false;
        }

        await Task.CompletedTask;
    }
}