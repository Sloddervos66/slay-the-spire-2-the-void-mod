using MegaCrit.Sts2.Core.Timeline;

namespace TheVoid.TheVoidCode.Timeline.Epochs;

public class TheVoid5Epoch : TheVoidEpochModel
{
    public override string Id => Prefix + "THE_VOID5_EPOCH";
    public override EpochEra Era => EpochEra.FarFuture1;
    public override int EraPosition => 2;
    
    public override string StoryId => "The Void";
}