using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using TheVoid.TheVoidCode.Extensions;

namespace TheVoid.TheVoidCode.Localization.DynamicVars;

public class VoidPactVar(decimal baseValue) : DynamicVar(Name, baseValue)
{
    public new const string Name = "VoidPact";
    public bool HasTarget;
    
    public override void UpdateCardPreview(
        CardModel card, 
        CardPreviewMode previewMode, 
        Creature? target, 
        bool runGlobalHooks)
    {
        HasTarget = true;
        PreviewValue = target.GetHpThreshold(BaseValue);
    }
}