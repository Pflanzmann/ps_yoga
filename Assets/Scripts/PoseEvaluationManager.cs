using System.Collections.Generic;
using Windows.Kinect;
using UnityEngine;
using System;
public class PoseEvaluationManager : BaseGameEventListener<PoseData> {

    [SerializeField] private BaseGameEvent<string> evaluationEvent;

    private string resultMessage = "";

    public override void OnEventRaised(PoseData value) {
        base.OnEventRaised(value);
        foreach(var jointData in value.JointDatas) {
            // print("JoinType: " + jointData.JointType + " | ErrorValue: " + jointData.ErrorValue + " | IsCorrect: " + jointData.IsCorrect);


            //var ausgewerteterStreing = evaluate(value);

            //dataSuccessfullyEvaluatedEvent.Raise(ausgewerteterStreing);
            //print(ausgewerteterStreing);
        }
        Exception e;
        resultMessage = evaluate(value);
        print(resultMessage);
    }

    public void OnSendPoseData() {
      try {
        evaluationEvent?.Raise(resultMessage);
            } catch (Exception e)
        {
            print(e);
        }
    }

    private string evaluate(PoseData value) {
        //besondersFalscheKoerperteileList
        var magic = new List<JointData>();

        var handShoulder = new List<JointType>()
        {
            JointType.HandTipLeft, JointType.HandLeft, JointType.ThumbLeft, JointType.WristLeft, JointType.ElbowLeft,
            JointType.ShoulderLeft, JointType.HandTipRight, JointType.HandRight, JointType.ThumbRight, JointType.WristRight,
            JointType.ElbowRight, JointType.ShoulderRight, JointType.ShoulderRight
        };
        var bodyHead = new List<JointType>()
            {JointType.SpineBase, JointType.SpineMid, JointType.SpineShoulder, JointType.Neck, JointType.Head};
        var beine = new List<JointType>()
        {
            JointType.FootLeft, JointType.AnkleLeft, JointType.KneeLeft, JointType.HipLeft, JointType.FootRight, JointType.AnkleRight,
            JointType.KneeRight, JointType.HipRight
        };

        var namePose = "(" + PoseManager.instance.GetCurrentPoseName() + "):\n";
        foreach(var jointData in value.JointDatas) {
            if(jointData.IsCorrect == false && jointData.ErrorValue > 0.25f) {
                magic.Add(jointData);
            }
        }
        var allFalschPose = " ";

        //var stringHand = "💪: " + (handShoulder.Count-countHand).ToString + "/" + handShoulder.Count.ToString;
        //var newMagic = magic.Sort((x, y) => x.ErrorValue.CompareTo(y.ErrorValue));

        foreach(var falschepose in magic) {
            if(falschepose.ErrorValue > 0.25f && falschepose.ErrorValue < 0.3f) {
                allFalschPose += "😕(leicht falsch)";
            } else if(falschepose.ErrorValue > 0.3f && falschepose.ErrorValue < 0.4f) {
                allFalschPose += "🙁(falsch)";
            } else if(falschepose.ErrorValue > 0.4f) {
                allFalschPose += "☹(ernsthaft falsch)";
            }

            switch(falschepose.JointType) {
                case JointType.HandTipLeft:
                    allFalschPose += "Handspitze(links)\n";
                    break;

                case JointType.HandLeft:
                    allFalschPose += "Hand(links)\n";
                    break;

                case JointType.ThumbLeft:
                    allFalschPose += "Daumen(links)\n";
                    break;

                case JointType.WristLeft:
                    allFalschPose += "Handgelenk(links)\n";
                    break;

                case JointType.ElbowLeft:
                    allFalschPose += "Ellbogen(links)\n";
                    break;

                case JointType.ShoulderLeft:
                    allFalschPose += "Schulter(links)\n";
                    break;

                case JointType.HandTipRight:
                    allFalschPose += "Handspitze(rechts)\n";
                    break;

                case JointType.HandRight:
                    allFalschPose += "Hand(rechts)\n";
                    break;

                case JointType.ThumbRight:
                    allFalschPose += "Daumen(rechts)\n";
                    break;

                case JointType.WristRight:
                    allFalschPose += "Handgelenk(rechts)\n";
                    break;

                case JointType.ElbowRight:
                    allFalschPose += "Ellbogen(rechts)\n";
                    break;

                case JointType.ShoulderRight:
                    allFalschPose += "Schulter(rechts)\n";
                    break;

                case JointType.SpineBase:
                    allFalschPose += "Kreuzbein\n";
                    break;

                case JointType.SpineMid:
                    allFalschPose += "Lendenwirbelsäule\n";
                    break;

                case JointType.SpineShoulder:
                    allFalschPose += "Brustwirbelsäule\n";
                    break;

                case JointType.Neck:
                    allFalschPose += "Nacken\n";
                    break;

                case JointType.Head:
                    allFalschPose += "Kopf\n";
                    break;

                case JointType.FootLeft:
                    allFalschPose += "Fuss(links)\n";
                    break;

                case JointType.AnkleLeft:
                    allFalschPose += "Knöchel(links)\n";
                    break;

                case JointType.KneeLeft:
                    allFalschPose += "Knie(links)\n";
                    break;

                case JointType.HipLeft:
                    allFalschPose += "Hüfte(links)\n";
                    break;

                case JointType.FootRight:
                    allFalschPose += "Fuss(rechts)\n";
                    break;

                case JointType.AnkleRight:
                    allFalschPose += "Knöchel(rechts)\n";
                    break;

                case JointType.KneeRight:
                    allFalschPose += "Knie(rechts)\n";
                    break;

                case JointType.HipRight:
                    allFalschPose += "Hüfte(rechts)\n";
                    break;
            }
            // allFalschPose += falschepose.JointType + " ,";
        }

        //return allFalschPose;


        allFalschPose += "\n";


        return namePose + allFalschPose;
    }

}