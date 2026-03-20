using BaseLib.Abstracts;
using BaseLib.Extensions;
using TheVoid.TheVoidCode.Extensions;
using Godot;

namespace TheVoid.TheVoidCode.Powers;

public abstract class TheVoidPower : CustomPowerModel
{
    //Loads from TheVoid/images/powers/your_power.png
    public override string CustomPackedIconPath
    {
        get
        {
            var path = $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".PowerImagePath();
            return ResourceLoader.Exists(path) ? path : "power.png".PowerImagePath();
        }
    }

    public override string CustomBigIconPath
    {
        get
        {
            var path = $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigPowerImagePath();
            return ResourceLoader.Exists(path) ? path : "power.png".BigPowerImagePath();
        }
    }
}