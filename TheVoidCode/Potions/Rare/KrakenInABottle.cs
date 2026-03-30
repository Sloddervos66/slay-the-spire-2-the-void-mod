using System.Reflection;
using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.Assets;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Merchant;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Events;
using MegaCrit.Sts2.Core.Nodes;
using MegaCrit.Sts2.Core.Nodes.Events.Custom;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.TestSupport;
using TheVoid.TheVoidCode.Character;
using TheVoid.TheVoidCode.Powers;

namespace TheVoid.TheVoidCode.Potions.Rare;

[Pool(typeof(TheVoidPotionPool))]
public sealed class KrakenInABottle : TheVoidPotion
{
    private static readonly FieldInfo CostField = typeof(MerchantEntry).GetField("_cost", BindingFlags.NonPublic | BindingFlags.Instance)!;
    public bool ShopIsFree { get; set; }

    public override PotionRarity Rarity => PotionRarity.Rare;
    public override PotionUsage Usage => PotionUsage.AnyTime;
    public override TargetType TargetType => !CombatManager.Instance.IsInProgress ? TargetType.TargetedNoCreature : TargetType.AllEnemies;

    public override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<BlindPower>()];
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<BlindPower>(20m)];

    public override bool PassesCustomUsabilityCheck
    {
        get
        {
            if (CombatManager.Instance.IsInProgress) return true;
            return Owner.RunState.CurrentRoom switch
            {
                MerchantRoom or EventRoom { CanonicalEvent: FakeMerchant } => true,
                _ => false
            };
        }
    }

    protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {
        if (CombatManager.Instance.IsInProgress)
        {
            var enemies = Owner.Creature.CombatState?.Enemies;
            if (enemies == null) return;
            await PowerCmd.Apply<BlindPower>(enemies, DynamicVars[BlindPower.Name].BaseValue, Owner.Creature, null);
        }
        else if (Owner.RunState.CurrentRoom is MerchantRoom)
        {
            var nMerchantRoom = NRun.Instance?.MerchantRoom;
            if (nMerchantRoom == null) return;
            ShopIsFree = true;
            
            var items = nMerchantRoom.Inventory.Inventory?.AllEntries.ToList();
            MakeInventoryFree(items);
            ShowPotionVfx(nMerchantRoom.MerchantButton);
        }
        else if (Owner.RunState.CurrentRoom is EventRoom { CanonicalEvent: FakeMerchant } eventRoom)
        {
            var localMutableEvent = eventRoom.LocalMutableEvent;
            if (localMutableEvent is not { Node: NFakeMerchant nFakeMerchant }) return;
            
            var fakeMerchant = (FakeMerchant)RunManager.Instance.EventSynchronizer.GetEventForPlayer(Owner);
            var items = fakeMerchant.Inventory.AllEntries.ToList();
                
            MakeInventoryFree(items);
            ShowPotionVfx(nFakeMerchant.MerchantButton);
        }
    }

    public override Task BeforeRoomEntered(AbstractRoom room)
    {
        ShopIsFree = false; 
        return Task.CompletedTask;
    }
    
    private static void ShowPotionVfx(NMerchantButton? merchantButton)
    {
        if (!TestMode.IsOn && merchantButton != null)
        {
            var scenePath = SceneHelper.GetScenePath("vfx/vfx_slime_impact");
            var node2D = PreloadManager.Cache.GetScene(scenePath).Instantiate<Node2D>(PackedScene.GenEditState.Disabled);
            merchantButton.GetParent().AddChildSafely(node2D);
            node2D.GlobalPosition = merchantButton.GlobalPosition;
        }
    }

    private static void MakeInventoryFree(IEnumerable<MerchantEntry>? entries)
    {
        if (entries == null) return;
        foreach (var entry in entries)
        {
            CostField.SetValue(entry, 0);
            entry.OnMerchantInventoryUpdated();
        }
    }
}