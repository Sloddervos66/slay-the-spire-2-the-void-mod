using System.Reflection;
using MegaCrit.Sts2.Core.Timeline;

namespace TheVoid.TheVoidCode.Patches;

public static class EpochModelPatch
{
    public static void Register(Type epochType)
    {
        var dictField = typeof(EpochModel).GetField(
            "_epochTypeDictionary",
            BindingFlags.NonPublic | BindingFlags.Static);
        var typeToIdField = typeof(EpochModel).GetField(
            "_typeToIdDictionary",
            BindingFlags.NonPublic | BindingFlags.Static);

        var epochDict = (Dictionary<string, Type>)dictField.GetValue(null);
        var typeDict = (Dictionary<Type, string>)typeToIdField.GetValue(null);

        var instance = (EpochModel)Activator.CreateInstance(epochType);
        epochDict[instance.Id] = epochType;
        typeDict[epochType] = instance.Id;
    }
}