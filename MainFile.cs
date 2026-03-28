using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Modding;
using TheVoid.TheVoidCode.Timeline;

namespace TheVoid;

[ModInitializer(nameof(Initialize))]
public partial class MainFile : Node
{
    public const string ModId = "TheVoid"; //Used for resource filepath

    public static MegaCrit.Sts2.Core.Logging.Logger Logger { get; } =
        new(ModId, MegaCrit.Sts2.Core.Logging.LogType.Generic);

    public static void Initialize()
    {
        Harmony harmony = new(ModId);
        harmony.PatchAll();
        
        TheVoidEpochRegistry.Register();
    }
}