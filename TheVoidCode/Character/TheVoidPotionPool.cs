using BaseLib.Abstracts;
using TheVoid.TheVoidCode.Extensions;
using Godot;

namespace TheVoid.TheVoidCode.Character;

public class TheVoidPotionPool : CustomPotionPoolModel
{
    public override Color LabOutlineColor => TheVoid.Color;


    public override string BigEnergyIconPath => "charui/big_energy.png".ImagePath();
    public override string TextEnergyIconPath => "charui/text_energy.png".ImagePath();
}