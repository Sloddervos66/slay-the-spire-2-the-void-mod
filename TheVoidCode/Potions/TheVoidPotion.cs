using BaseLib.Abstracts;
using BaseLib.Utils;
using TheVoid.TheVoidCode.Character;

namespace TheVoid.TheVoidCode.Potions;

[Pool(typeof(TheVoidPotionPool))]
public abstract class TheVoidPotion : CustomPotionModel;