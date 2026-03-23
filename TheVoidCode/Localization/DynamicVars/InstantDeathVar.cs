using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using TheVoid.TheVoidCode.Extensions;

namespace TheVoid.TheVoidCode.Localization.DynamicVars;

public class InstantDeathVar(decimal baseValue) : DynamicVar(Name, baseValue)
{
    public new const string Name = "InstantDeath";
    public bool HasTarget;

    public override void UpdateCardPreview(
        CardModel card, 
        CardPreviewMode previewMode, 
        Creature? target, 
        bool runGlobalHooks)
    {
        if (target != null)
        {
            HasTarget = true;
            PreviewValue = target.GetHpThreshold(BaseValue);
        }
        else
        {
            HasTarget = false;
            PreviewValue = BaseValue;
        }
    }
}