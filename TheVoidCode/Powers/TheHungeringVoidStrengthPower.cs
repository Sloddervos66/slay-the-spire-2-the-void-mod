using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using TheVoid.TheVoidCode.Relics.Rare;

namespace TheVoid.TheVoidCode.Powers;

public sealed class TheHungeringVoidStrengthPower : TemporaryStrengthPower
{
    public override AbstractModel OriginModel => ModelDb.Relic<TheHungeringVoid>();
}