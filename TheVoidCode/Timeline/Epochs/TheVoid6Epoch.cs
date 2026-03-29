using MegaCrit.Sts2.Core.Timeline;

namespace TheVoid.TheVoidCode.Timeline.Epochs;

public class TheVoid6Epoch : TheVoidEpochModel
{
    public override string Id => Prefix + "THE_VOID6_EPOCH";
    public override EpochEra Era => EpochEra.Flourish0;
    public override int EraPosition => 3;
    
    public override string StoryId => "The Void";
}