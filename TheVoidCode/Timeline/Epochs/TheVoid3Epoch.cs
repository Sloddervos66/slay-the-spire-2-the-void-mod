using MegaCrit.Sts2.Core.Timeline;

namespace TheVoid.TheVoidCode.Timeline.Epochs;

public class TheVoid3Epoch : TheVoidEpochModel
{
    public override string Id => Prefix + "THE_VOID3_EPOCH";
    public override EpochEra Era => EpochEra.Blight2;
    public override int EraPosition => 2;
    
    public override string StoryId => "The Void";
}