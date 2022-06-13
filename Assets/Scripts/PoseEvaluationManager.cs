using System.Collections.Generic;

public class PoseEvaluationManager : BaseGameEventListener<PoseData> {


    public override void OnEventRaised(PoseData value) {
        base.OnEventRaised(value);
        foreach(var jointData in value.JointDatas) {
            print("JoinType: " + jointData.JointType + " | ErrorValue: " + jointData.ErrorValue + " | IsCorrect: " + jointData.IsCorrect);
        }

        //var ausgewerteterStreing = evaluate(value);

        //dataSuccessfullyEvaluatedEvent.Raise(ausgewerteterStreing);
    }

    private void evaluate(PoseData data) {
        //magic
        List<JointData> magic = new List<JointData>();
        foreach (var jointData in data.JointDatas)
        {
            if(jointData.IsCorrect == false)
            {
                magic.Add(jointData);
            }
        }
        print("All falsh jointtype: " + magic);
        
    }

}