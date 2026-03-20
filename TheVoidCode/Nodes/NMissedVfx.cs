using Godot;
using MegaCrit.Sts2.addons.mega_text;
using MegaCrit.Sts2.Core.Assets;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Random;
using MegaCrit.Sts2.Core.TestSupport;

namespace TheVoid.TheVoidCode.nodes;

public partial class NMissedVfx : Node2D
{
    private static readonly LocString _missedLoc = new("vfx", "THEVOID-MISSED");
    private MegaLabel _label;
    private Tween? _tween;
    private static string ScenePath => SceneHelper.GetScenePath("vfx/vfx_blocked_text");
    public static IEnumerable<string> AssetPath => [ScenePath];

    public static NMissedVfx? Create(Creature target)
    {
        if (TestMode.IsOn) return null;

        var creatureNode = NCombatRoom.Instance.GetCreatureNode(target);
        if (!creatureNode.IsInteractable) return null;

        var nMissedVfx = PreloadManager.Cache.GetScene(ScenePath)
            .Instantiate<NMissedVfx>(PackedScene.GenEditState.Disabled);
        nMissedVfx.GlobalPosition = creatureNode.VfxSpawnPosition +
                                    new Vector2(Rng.Chaotic.NextFloat(-20f, 20f), Rng.Chaotic.NextFloat(-60f, -40f));
        nMissedVfx.RotationDegrees = Rng.Chaotic.NextFloat(-2f, 2f);
        return nMissedVfx;
    }

    public override void _Ready()
    {
        _label = GetNode<MegaLabel>("Label");
        _label.SetText(_missedLoc.GetRawText());
        TaskHelper.RunSafely(MissAnim());
    }

    private async Task MissAnim()
    {
        _tween = CreateTween().SetParallel();
        _tween.TweenProperty(this, "scale", Vector2.One * 0.6f, 2.0).From(Vector2.One).SetEase(Tween.EaseType.Out)
            .SetTrans(Tween.TransitionType.Quad);
        _tween.TweenProperty(this, "position:y", base.Position.Y - 250f, 2.0).SetEase(Tween.EaseType.Out)
            .SetTrans(Tween.TransitionType.Quad);
        _tween.TweenProperty(_label, "modulate", Colors.White, 2.0).From(new Color("AAAAAA")).SetEase(Tween.EaseType.Out)
            .SetTrans(Tween.TransitionType.Cubic);
        _tween.TweenProperty(this, "modulate:a", 0f, 1.5).SetEase(Tween.EaseType.In).SetTrans(Tween.TransitionType.Sine)
            .SetDelay(0.5);
        await ToSignal(_tween, Tween.SignalName.Finished);
        this.QueueFreeSafely();
    }
}