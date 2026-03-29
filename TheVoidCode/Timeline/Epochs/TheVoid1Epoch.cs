using MegaCrit.Sts2.Core.Timeline;

namespace TheVoid.TheVoidCode.Timeline.Epochs;

public class TheVoid1Epoch : TheVoidEpochModel
{
    public override string Id => Prefix + "THE_VOID1_EPOCH";
    
    public override EpochEra Era => EpochEra.Blight0;
    public override int EraPosition => 1;

    public override string StoryId => "The Void";
}