using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using TheVoid.TheVoidCode.Character;
using TheVoid.TheVoidCode.Extensions;
using TheVoid.TheVoidCode.Localization.DynamicVars;
using TheVoid.TheVoidCode.Powers;

namespace TheVoid.TheVoidCode.Cards.Uncommon;

[Pool(typeof(TheVoidCardPool))]
public sealed class BlindFury() : TheVoidCard(2, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
{
    private const decimal AdditionalDamagePerStack = 3m;
    private const decimal StacksPerBonus = 2m;
    
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new BlindFuryDamageVar(5m, AdditionalDamagePerStack, StacksPerBonus),
        new(Constants.DynamicVars.AdditionalDamage, AdditionalDamagePerStack),
        new PowerVar<BlindPower>(StacksPerBonus)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var target = cardPlay.Target;
        if (target == null) return;

        var additionalDamage = Owner.Creature.HasBlind()
            ? DynamicVars[Constants.DynamicVars.AdditionalDamage].BaseValue * (Owner.Creature.GetPowerAmount<BlindPower>() / DynamicVars[BlindPower.Name].BaseValue)
            : 0m;
        var totalDamage = DynamicVars[BlindFuryDamageVar.Name].BaseValue + additionalDamage;
        await DamageCmd.Attack(totalDamage).FromCard(this).Targeting(target)
            .WithHitFx(DefaultAttackVfx)
            .Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        DynamicVars[BlindFuryDamageVar.Name].UpgradeValueBy(2m);
    }
}