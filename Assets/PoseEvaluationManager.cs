public class PoseEvaluationManager : BaseGameEventListener<PoseData> {


    public override void OnEventRaised(PoseData value) {
        base.OnEventRaised(value);
        foreach(var jointData in value.JointDatas) {
            print("JoinType: " + jointData.JointType + " | ErrorValue: " + jointData.ErrorValue + " | IsCorrect: " + jointData.IsCorrect);
        }

        //var ausgewerteterStreing = evaluate(value);

        //dataSuccessfullyEvaluatedEvent.Raise(ausgewerteterStreing);
    }

    private string evaluate(PoseData data) {
        //magic
        return "";
    }

}