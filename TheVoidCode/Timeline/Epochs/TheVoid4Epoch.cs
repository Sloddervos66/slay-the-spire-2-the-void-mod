using MegaCrit.Sts2.Core.Timeline;

namespace TheVoid.TheVoidCode.Timeline.Epochs;

public class TheVoid4Epoch : TheVoidEpochModel
{
    public override string Id => Prefix + "THE_VOID4_EPOCH";
    public override EpochEra Era => EpochEra.FarFuture0;
    public override int EraPosition => 2;
    
    public override string StoryId => "The Void";
}