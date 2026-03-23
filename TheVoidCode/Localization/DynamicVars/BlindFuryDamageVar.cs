using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using TheVoid.TheVoidCode.Extensions;
using TheVoid.TheVoidCode.Powers;

namespace TheVoid.TheVoidCode.Localization.DynamicVars;

public class BlindFuryDamageVar(decimal baseValue) : DynamicVar(Name, baseValue)
{
    public new const string Name = "BlindFury";
    private readonly decimal _additionalDamagePerStack;
    private readonly decimal _stacksPerBonus;

    public BlindFuryDamageVar(decimal baseValue, decimal additionalDamagePerStack, decimal stacksPerBonus)
        : this(baseValue)
    {
        _additionalDamagePerStack = additionalDamagePerStack;
        _stacksPerBonus = stacksPerBonus;
    }

    public override void UpdateCardPreview(
        CardModel card, 
        CardPreviewMode previewMode, 
        Creature? target, 
        bool runGlobalHooks)
    {
        var owner = card.Owner?.Creature;
        if (owner != null && owner.HasBlind())
        {
            var blindStacks = owner.GetPowerAmount<BlindPower>();
            var bonus = _additionalDamagePerStack * (blindStacks / _stacksPerBonus);
            PreviewValue = BaseValue + bonus;
        }
        else
        {
            PreviewValue = BaseValue;
        }
        
        base.UpdateCardPreview(card, previewMode, target, runGlobalHooks);
    }
}