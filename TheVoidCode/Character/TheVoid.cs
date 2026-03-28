using BaseLib.Abstracts;
using TheVoid.TheVoidCode.Extensions;
using Godot;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Models;
using TheVoid.TheVoidCode.Cards.Basic;
using TheVoid.TheVoidCode.Relics.Starter;

namespace TheVoid.TheVoidCode.Character;

public class TheVoid : PlaceholderCharacterModel
{
    public const string CharacterId = "TheVoid";

    public static readonly Color Color = new("#2B0A3D");

    public override Color NameColor => Color;
    public override CharacterGender Gender => CharacterGender.Neutral;
    public override int StartingHp => 100;
    
    public override CardPoolModel CardPool => ModelDb.CardPool<TheVoidCardPool>();
    public override RelicPoolModel RelicPool => ModelDb.RelicPool<TheVoidRelicPool>();
    public override PotionPoolModel PotionPool => ModelDb.PotionPool<TheVoidPotionPool>();

    public override IEnumerable<CardModel> StartingDeck =>
    [
        ModelDb.Card<StrikeTheVoid>(),
        ModelDb.Card<StrikeTheVoid>(),
        ModelDb.Card<StrikeTheVoid>(),
        ModelDb.Card<StrikeTheVoid>(),
        ModelDb.Card<StrikeTheVoid>(),
        ModelDb.Card<DefendTheVoid>(),
        ModelDb.Card<DefendTheVoid>(),
        ModelDb.Card<DefendTheVoid>(),
        ModelDb.Card<DefendTheVoid>(),
        ModelDb.Card<DefendTheVoid>()
    ];

    public override IReadOnlyList<RelicModel> StartingRelics =>
    [
        ModelDb.Relic<PitchBlack>()
    ];

    /*  PlaceholderCharacterModel will utilize placeholder basegame assets for most of your character assets until you
        override all the other methods that define those assets.
        These are just some of the simplest assets, given some placeholders to differentiate your character with.
        You don't have to, but you're suggested to rename these images. */
    public override string CustomIconTexturePath => "character_icon_char_name.png".CharacterUiPath();
    public override string CustomCharacterSelectIconPath => "char_select_char_name.png".CharacterUiPath();
    public override string CustomCharacterSelectLockedIconPath => "char_select_char_name_locked.png".CharacterUiPath();
    public override string CustomMapMarkerPath => "map_marker_char_name.png".CharacterUiPath();
}