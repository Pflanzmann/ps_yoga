using System.Collections.Generic;
using Windows.Kinect;
using UnityEngine;
using System;
using System.Linq;
public class PoseEvaluationManager : BaseGameEventListener<PoseData>
{

    [SerializeField] private BaseGameEvent<string> evaluationEvent;

    private string resultMessage = "";
    private List<List<JointData>> allPoseDataList = new List<List<JointData>>();



    public override void OnEventRaised(PoseData value)
    {
        base.OnEventRaised(value);
        foreach (var jointData in value.JointDatas)
        {
            // print("JoinType: " + jointData.JointType + " | ErrorValue: " + jointData.ErrorValue + " | IsCorrect: " + jointData.IsCorrect);


            //var ausgewerteterStreing = evaluate(value);

            //dataSuccessfullyEvaluatedEvent.Raise(ausgewerteterStreing);
            //print(ausgewerteterStreing);
        }
        Exception e;
       // resultMessage = evaluate(value);
        allPoseDataList.Add(value.JointDatas);
       // print("AllposeList : " + allPoseDataList.Count);
        //print(resultMessage);
    }
    private PoseData helpfunction(List<List<JointData>> allPoseDataList)
    {
        var listJointData1 = new List<JointData>();
        var listJointData2 = new List<JointData>();
        var listJointData3 = new List<JointData>();
        var listJointData4 = new List<JointData>();
        var listJointData5 = new List<JointData>();
        var listJointData6 = new List<JointData>();
        var listJointData7 = new List<JointData>();
        var listJointData8 = new List<JointData>();
        var listJointData9 = new List<JointData>();
        var listJointData10 = new List<JointData>();
        var listJointData11 = new List<JointData>();
        var listJointData12 = new List<JointData>();
        var listJointData13 = new List<JointData>();
        var listJointData14 = new List<JointData>();
        var listJointData15 = new List<JointData>();
        var listJointData16 = new List<JointData>();
        var listJointData17 = new List<JointData>();

        var listJointData18 = new List<JointData>();
        var listJointData19 = new List<JointData>();
        var listJointData20 = new List<JointData>();
        var listJointData21 = new List<JointData>();
        var listJointData22 = new List<JointData>();
        var listJointData23 = new List<JointData>();
        var listJointData24 = new List<JointData>();
        var listJointData25 = new List<JointData>();
        foreach (var listJointData in allPoseDataList)
        {
            foreach(var jointData in listJointData)
            {
                switch (jointData.JointType)
                {
                    case JointType.HandTipLeft:
                        listJointData1.Add(jointData);
                        break;

                    case JointType.HandLeft:
                        listJointData2.Add(jointData);
                        break;

                    case JointType.ThumbLeft:
                        listJointData3.Add(jointData);
                        break;

                    case JointType.WristLeft:
                        listJointData4.Add(jointData);
                        break;

                    case JointType.ElbowLeft:
                        listJointData5.Add(jointData);
                        break;

                    case JointType.ShoulderLeft:
                        listJointData6.Add(jointData);
                        break;

                    case JointType.HandTipRight:
                        listJointData7.Add(jointData);
                        break;

                    case JointType.HandRight:
                        listJointData8.Add(jointData);
                        break;

                    case JointType.ThumbRight:
                        listJointData9.Add(jointData);
                        break;

                    case JointType.WristRight:
                        listJointData10.Add(jointData);
                        break;

                    case JointType.ElbowRight:
                        listJointData11.Add(jointData);
                        break;

                    case JointType.ShoulderRight:
                        listJointData12.Add(jointData);
                        break;

                    case JointType.SpineBase:
                        listJointData13.Add(jointData);
                        break;

                    case JointType.SpineMid:
                        listJointData14.Add(jointData);
                        break;

                    case JointType.SpineShoulder:
                        listJointData15.Add(jointData);
                        break;

                    case JointType.Neck:
                        listJointData16.Add(jointData);
                        break;

                    case JointType.Head:
                        listJointData17.Add(jointData);
                        break;

                    case JointType.FootLeft:
                        listJointData18.Add(jointData);
                        break;

                    case JointType.AnkleLeft:
                        listJointData19.Add(jointData);
                        break;

                    case JointType.KneeLeft:
                        listJointData20.Add(jointData);
                        break;

                    case JointType.HipLeft:
                        listJointData21.Add(jointData);
                        break;

                    case JointType.FootRight:
                        listJointData22.Add(jointData);
                        break;

                    case JointType.AnkleRight:
                        listJointData23.Add(jointData);
                        break;

                    case JointType.KneeRight:
                        listJointData24.Add(jointData);
                        break;

                    case JointType.HipRight:
                        listJointData25.Add(jointData);
                        break;
                }
            }
        }
        
        
        foreach (var jointData in listJointData17)
        {
            print("JoinType: " + jointData.JointType + " | ErrorValue: " + jointData.ErrorValue + " | IsCorrect: " + jointData.IsCorrect);
        }
        var average1 = listJointData1.Select(x => x.ErrorValue).Average();
        
        var average2 = listJointData2.Select(x => x.ErrorValue).Average(); var average3 = listJointData3.Select(x => x.ErrorValue).Average();

        
        var average4 = listJointData4.Select(x => x.ErrorValue).Average(); var average5 = listJointData5.Select(x => x.ErrorValue).Average(); var average6 = listJointData6.Select(x => x.ErrorValue).Average();
       
        var average7 = listJointData7.Select(x => x.ErrorValue).Average(); var average8 = listJointData8.Select(x => x.ErrorValue).Average(); var average9 = listJointData9.Select(x => x.ErrorValue).Average();
        var average10 = listJointData10.Select(x => x.ErrorValue).Average(); var average11 = listJointData11.Select(x => x.ErrorValue).Average(); var average12 = listJointData12.Select(x => x.ErrorValue).Average();
        var average13 = listJointData13.Select(x => x.ErrorValue).Average(); var average14 = listJointData14.Select(x => x.ErrorValue).Average(); var average15 = listJointData15.Select(x => x.ErrorValue).Average();
        var average16 = listJointData16.Select(x => x.ErrorValue).Average();
        
        foreach (var jointData in listJointData17)
        {
            print("JoinType: " + jointData.JointType + " | ErrorValue: " + jointData.ErrorValue + " | IsCorrect: " + jointData.IsCorrect);
        }
       // var average17 = listJointData17.Select(x => x.ErrorValue).Average();
        //print("17:" + average17);
        var average18 = listJointData18.Select(x => x.ErrorValue).Average();
        var average19 = listJointData19.Select(x => x.ErrorValue).Average(); var average20 = listJointData20.Select(x => x.ErrorValue).Average(); var average21 = listJointData21.Select(x => x.ErrorValue).Average();
        var average22 = listJointData22.Select(x => x.ErrorValue).Average(); var average23 = listJointData23.Select(x => x.ErrorValue).Average(); var average24 = listJointData24.Select(x => x.ErrorValue).Average();
        var average25 = listJointData25.Select(x => x.ErrorValue).Average();
        
       

        var newJointData1 = new JointData(JointType.HandTipLeft, average1, average1 < 0.2f);
        var newJointData2 = new JointData(JointType.HandLeft, average2, average2 < 0.2f);
        var newJointData3 = new JointData(JointType.ThumbLeft, average3, average3 < 0.2f);
        var newJointData4 = new JointData(JointType.WristLeft, average4, average4 < 0.2f);
        var newJointData5 = new JointData(JointType.ElbowLeft, average5, average5 < 0.2f);
        var newJointData6 = new JointData(JointType.ShoulderLeft, average6, average6 < 0.2f);
        var newJointData7 = new JointData(JointType.HandTipRight, average7, average7 < 0.2f);
        var newJointData8 = new JointData(JointType.HandRight, average8, average8 < 0.2f);
        var newJointData9 = new JointData(JointType.ThumbRight, average9, average9 < 0.2f);
        var newJointData10 = new JointData(JointType.WristRight, average10, average10 < 0.2f);
        var newJointData11 = new JointData(JointType.ElbowRight, average11, average11 < 0.2f);
        var newJointData12 = new JointData(JointType.ShoulderRight, average12, average12 < 0.2f);
        var newJointData13 = new JointData(JointType.SpineBase, average13, average13 < 0.2f);
        var newJointData14 = new JointData(JointType.SpineMid, average14, average14 < 0.2f);
        var newJointData15 = new JointData(JointType.SpineShoulder, average15, average15 < 0.2f);
        var newJointData16 = new JointData(JointType.Neck, average16, average16 < 0.2f);
        //var newJointData17 = new JointData(JointType.Head, average17, average17 < 0.2f);
        var newJointData18 = new JointData(JointType.FootLeft, average18, average18 < 0.2f);
        var newJointData19 = new JointData(JointType.AnkleLeft, average19, average19 < 0.2f);
        var newJointData20 = new JointData(JointType.KneeLeft, average20, average20 < 0.2f);
        var newJointData21 = new JointData(JointType.HipLeft, average21, average21 < 0.2f);
        var newJointData22 = new JointData(JointType.FootRight, average22, average22 < 0.2f);
        var newJointData23 = new JointData(JointType.AnkleRight, average23, average23 < 0.2f);
        var newJointData24 = new JointData(JointType.KneeRight, average24, average24 < 0.2f);
        var newJointData25 = new JointData(JointType.HipRight, average25, average25 < 0.2f);
        var newListJointData = new List<JointData>() { newJointData1, newJointData2, newJointData3, newJointData4, newJointData5, newJointData6, newJointData7, newJointData8,
            newJointData9, newJointData10, newJointData11, newJointData12, newJointData13, newJointData14, newJointData15, newJointData16, newJointData18,
            newJointData19, newJointData20, newJointData21, newJointData22, newJointData23, newJointData24, newJointData25};
        var averagePoseData = new PoseData(newListJointData);
        
        return averagePoseData;

    }
    public void OnSendPoseData()
    {
        var value = helpfunction(allPoseDataList);
        resultMessage = evaluate(value);
        print(resultMessage);
        evaluationEvent?.Raise(resultMessage);
        allPoseDataList.Clear();
        try
        {
           
        }
        catch (Exception e)
        {
            print(e);
        }
    }

    private string evaluate(PoseData value)
    {
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
        foreach (var jointData in value.JointDatas)
        {
            if (jointData.IsCorrect == false && jointData.ErrorValue > 0.4f)
            {
                magic.Add(jointData);
            }
        }
        var allFalschPose = " ";

        //var stringHand = "💪: " + (handShoulder.Count-countHand).ToString + "/" + handShoulder.Count.ToString;
        //var newMagic = magic.Sort((x, y) => x.ErrorValue.CompareTo(y.ErrorValue));

        foreach (var falschepose in magic)
        {
            if (falschepose.ErrorValue > 0.4f && falschepose.ErrorValue <= 0.6f)
            {
                allFalschPose += "😕";
            }
            else if (falschepose.ErrorValue > 0.6f && falschepose.ErrorValue <= 0.8f)
            {
                allFalschPose += "🙁";
            }
            else if (falschepose.ErrorValue > 0.8f)
            {
                allFalschPose += "☹";
            }

            switch (falschepose.JointType)
            {
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