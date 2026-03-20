using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using TheVoid.TheVoidCode.Powers;

namespace TheVoid.TheVoidCode.Relics;

public class TotalDarkness : TheVoidRelic
{
    public override RelicRarity Rarity => RelicRarity.Starter;
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<BlindPower>(10)];

    public override async Task BeforeCombatStartLate()
    {
        Flash();
        foreach (var enemy in Owner.Creature.CombatState.Enemies)
        {
            await PowerCmd.Apply<BlindPower>(enemy, DynamicVars["BlindPower"].IntValue, Owner.Creature, null);
        }
    }
}