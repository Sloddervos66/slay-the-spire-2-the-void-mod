using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Nodes.Cards;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using TheVoid.TheVoidCode.Interfaces;

namespace TheVoid.TheVoidCode.Patches;

[HarmonyPatch(typeof(NCard), nameof(NCard.SetPreviewTarget))]
public static class SetPreviewTargetPatch
{
    private const string OverlayName = "VoidClaimedOverlay";
    private static Creature? _lastTarget;
    
    public static void Postfix(NCard __instance, Creature? creature)
    {
        if (__instance.Model is not ITargetCondition conditional) return;
        if (creature == _lastTarget) return;
       
        _lastTarget = creature;

        ClearAllOverlays();

        if (creature != null && !conditional.IsValidTarget(creature))
        {
            __instance.CardHighlight.Modulate = new Color(1f, 0f, 0f);
            AddOverlayToCreature(creature);
        }
        else
        {
            __instance.CardHighlight.Modulate = NCardHighlight.playableColor;
        }
    }
    
    private static void ClearAllOverlays()
    {
        if (NCombatRoom.Instance == null) return;

        foreach (var creatureNode in NCombatRoom.Instance.CreatureNodes)
        {
            creatureNode.Hitbox.GetNodeOrNull<Control>(OverlayName)?.QueueFree();
        }
    }
    
    private static void AddOverlayToCreature(Creature creature)
    {
        var creatureNode = NCombatRoom.Instance?.GetCreatureNode(creature);
        if (creatureNode == null) return;

        creatureNode.Hitbox.AddChild(CreateOverlay());
    }
    
    private static Control CreateOverlay()
    {
        var overlay = new Control();
        overlay.Name = OverlayName;
        overlay.SetAnchorsPreset(Control.LayoutPreset.FullRect);
        overlay.ZIndex = 10;
        overlay.MouseFilter = Control.MouseFilterEnum.Ignore;
        overlay.AddChild(CreateLabel());
        return overlay;
    }
    
    private static Label CreateLabel()
    {
        var label = new Label();
        label.Text = "X";
        label.AddThemeFontSizeOverride("font_size", 120);
        label.Modulate = new Color(1f, 0f, 0f, 0.85f);
        label.SetAnchorsPreset(Control.LayoutPreset.FullRect);
        label.HorizontalAlignment = HorizontalAlignment.Center;
        label.VerticalAlignment = VerticalAlignment.Center;
        label.MouseFilter = Control.MouseFilterEnum.Ignore;
        return label;
    }
}