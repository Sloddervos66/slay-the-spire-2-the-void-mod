using MegaCrit.Sts2.Core.Timeline;

namespace TheVoid.TheVoidCode.Timeline.Epochs;

public class TheVoid7Epoch : TheVoidEpochModel
{
    public override string Id => Prefix + "THE_VOID7_EPOCH";
    public override EpochEra Era => EpochEra.Flourish1;
    public override int EraPosition => 3;
    
    public override string StoryId => "The Void";
}