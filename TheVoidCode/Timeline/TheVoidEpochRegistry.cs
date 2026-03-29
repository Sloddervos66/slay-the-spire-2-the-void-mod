using TheVoid.TheVoidCode.Patches;
using TheVoid.TheVoidCode.Timeline.Epochs;

namespace TheVoid.TheVoidCode.Timeline;

public class TheVoidEpochRegistry
{
    public static void Register()
    {
        EpochModelPatch.Register(typeof(TheVoid1Epoch));
        EpochModelPatch.Register(typeof(TheVoid2Epoch));
        EpochModelPatch.Register(typeof(TheVoid3Epoch));
        EpochModelPatch.Register(typeof(TheVoid4Epoch));
        EpochModelPatch.Register(typeof(TheVoid5Epoch));
        EpochModelPatch.Register(typeof(TheVoid6Epoch));
        EpochModelPatch.Register(typeof(TheVoid7Epoch));
    }
}