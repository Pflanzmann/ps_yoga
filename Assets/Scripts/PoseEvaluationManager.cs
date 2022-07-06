

using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;
public class PoseEvaluationManager : BaseGameEventListener<PoseData> {

    [SerializeField] private BaseGameEvent<string> evaluationEvent;

    public override void OnEventRaised(PoseData value) {
        base.OnEventRaised(value);
        foreach(var jointData in value.JointDatas) {
            print("JoinType: " + jointData.JointType + " | ErrorValue: " + jointData.ErrorValue + " | IsCorrect: " + jointData.IsCorrect);


            //var ausgewerteterStreing = evaluate(value);

            //dataSuccessfullyEvaluatedEvent.Raise(ausgewerteterStreing);
            //print(ausgewerteterStreing);
            
        }
        var result = evaluate(value);
            print(result);

            evaluationEvent?.Raise(result);
    }

    private string evaluate(PoseData value) {
        
        
        
        
        
        
        
        
        
        //besondersFalscheKoerperteileList
        var magic = new List<JointData>();
        
        var handShoulder = new List<JointType>() { JointType.HandTipLeft, JointType.HandLeft, JointType.ThumbLeft, JointType.WristLeft, JointType.ElbowLeft, JointType.ShoulderLeft, JointType.HandTipRight, JointType.HandRight, JointType.ThumbRight, JointType.WristRight, JointType.ElbowRight, JointType.ShoulderRight, JointType.ShoulderRight };
        var bodyHead = new List<JointType>() { JointType.SpineBase , JointType.SpineMid , JointType.SpineShoulder , JointType.Neck , JointType.Head };
        var beine = new List<JointType>() { JointType.FootLeft, JointType.AnkleLeft, JointType.KneeLeft , JointType.HipLeft, JointType.FootRight , JointType.AnkleRight , JointType.KneeRight , JointType.HipRight };
        var countHand = 0;
        var countBody = 0;
        var countFoot = 0;
        var namePose ="Name von Pose: " + PoseManager.instance.GetCurrentPoseName() + "\n";
        foreach (var jointData in value.JointDatas) {
            if(jointData.IsCorrect == false && jointData.ErrorValue > 0.25f) { 
                magic.Add(jointData);
            if (handShoulder.Contains(jointData.JointType))
            {
                countHand++;
            } else if (bodyHead.Contains(jointData.JointType))
                {
                countBody++;
                } else if (beine.Contains(jointData.JointType))
                {
                countFoot++;
                }

            }
        }
        
        var time = string.Format("{0:HH:mm:ss}", System.DateTime.Now);
        var time2 = "Uhrzeit : " + time + ". \n";
        var stringHand = string.Concat("💪(☞ﾟヮﾟ)☞🦾🦶🖐Hand:", (handShoulder.Count - countHand).ToString(), "/", handShoulder.Count.ToString(), "\n");
        var stringBody = string.Concat("💪(☞ﾟヮﾟ)☞🦾🦶🖐👩‍👩‍👦👩‍👩‍👧🐮🐵☹🙁😓Body:", (bodyHead.Count - countBody).ToString(), "/", bodyHead.Count.ToString(), "\n");
        var stringFoot = string.Concat("💪(☞ﾟヮﾟ)☞🦾🦶🖐Foot:", (beine.Count - countFoot).ToString(), "/", beine.Count.ToString(), "\n");

        //var stringHand = "💪: " + (handShoulder.Count-countHand).ToString + "/" + handShoulder.Count.ToString;

        var allFalschPose = " Alle besondere falsche Körperteile : ";
        
        foreach(var falschepose in magic) {
            allFalschPose += falschepose.JointType + " ,";
        }
        allFalschPose += " .";
        //return allFalschPose;


        allFalschPose += "\n";
        

        return  namePose +time2 + stringHand +stringBody+stringFoot+ allFalschPose;
    }

}