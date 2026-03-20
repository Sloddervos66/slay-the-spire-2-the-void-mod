using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;

namespace TheVoid.TheVoidCode.Powers;

public sealed class BlindPower : TheVoidPower
{
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;
    protected override IEnumerable<DynamicVar> CanonicalVars => [new("BlindAmount", 1m)];

    private bool _willMiss;
    
    public override Task BeforeAttack(AttackCommand command)
    {
        if (command.Attacker != Owner) return Task.CompletedTask;
        
        var missChance = Math.Min(Amount, 100) / 100f;
        _willMiss = Random.Shared.NextSingle() < missChance;
        return Task.CompletedTask;
    }
    
    public override decimal ModifyDamageMultiplicative(Creature? target, decimal amount, ValueProp props, Creature? dealer, CardModel? cardSource)
    {
        if (dealer != Owner) return 1m;
        if (!props.IsPoweredAttack_()) return 1m;
        if (!_willMiss) return 1m;

        var vfxContainer = NCombatRoom.Instance?.CombatVfxContainer;
        vfxContainer?.AddChildSafely(NDamageBlockedVfx.Create(target));
        return 0m;
    }

    public override Task AfterAttack(AttackCommand command)
    {
        if (command.Attacker == Owner) _willMiss = false;
        return Task.CompletedTask;
    }
}