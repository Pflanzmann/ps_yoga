/*using System.Collections.Generic;

public class PoseEvaluationManager : BaseGameEventListener<PoseData> {


    public override void OnEventRaised(PoseData value) {
        base.OnEventRaised(value);
        foreach(var jointData in value.JointDatas) {
            //print("JoinType: " + jointData.JointType + " | ErrorValue: " + jointData.ErrorValue + " | IsCorrect: " + jointData.IsCorrect);
            if(jointData.IsCorrect == false)
            {
                print("false Pose: " + jointData.JointType);
            }
            
        }

        //var ausgewerteterStreing = evaluate(value);

        //dataSuccessfullyEvaluatedEvent.Raise(ausgewerteterStreing);
        //evaluate(value);
        //print("sdonl" );
    }
    


    /*private void evaluate(PoseData data) {
        //magic
        base.OnEventRaised(data);
        List<JointData> magic = new List<JointData>();
        foreach (var jointData in data.JointDatas)
        {
            if(jointData.IsCorrect == false)
            {
                magic.Add(jointData);
            }
        }
        print("All falsh jointtype: " );
        //return "abc";
    }*/

//}

using System.Collections.Generic;
using UnityEngine;
public class PoseEvaluationManager : BaseGameEventListener<PoseData> {

    [SerializeField] private BaseGameEvent<string> evaluationEvent;

    public override void OnEventRaised(PoseData value) {
        base.OnEventRaised(value);
        //  print("JoinType: " + jointData.JointType + " | ErrorValue: " + jointData.ErrorValue + " | IsCorrect: " + jointData.IsCorrect);

        var result = evaluate(value);
        // print(result);

        evaluationEvent?.Raise(result);
    }

    private string evaluate(PoseData value) {
        //magic
        var magic = new List<JointData>();
        foreach(var jointData in value.JointDatas) {
            if(jointData.IsCorrect == false) {
                magic.Add(jointData);
            }
        }
        var allFalschPose = " All false Poses : ";
        foreach(var falschepose in magic) {
            allFalschPose += falschepose.JointType + " ,";
        }
        allFalschPose += " .";
        return allFalschPose;
    }

}