public class PoseEvaluationManager : BaseGameEventListener<PoseData> {
    public override void OnEventRaised(PoseData value) {
        base.OnEventRaised(value);
        print("event called");
    }
}