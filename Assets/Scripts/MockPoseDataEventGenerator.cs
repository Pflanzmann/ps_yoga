using System.Collections.Generic;
using Windows.Kinect;
using UnityEngine;
public class MockPoseDataEventGenerator : MonoBehaviour {
    [SerializeField] private PoseDataEvent poseDataEvent;

    [SerializeField] private float updateInterval = 2f;
    private float timer = 0f;

    private Dictionary<JointType, JointType> _BoneMap = new Dictionary<JointType, JointType>()
    {
        {JointType.FootLeft, JointType.AnkleLeft},
        {JointType.AnkleLeft, JointType.KneeLeft},
        {JointType.KneeLeft, JointType.HipLeft},
        {JointType.HipLeft, JointType.SpineBase},

        {JointType.FootRight, JointType.AnkleRight},
        {JointType.AnkleRight, JointType.KneeRight},
        {JointType.KneeRight, JointType.HipRight},
        {JointType.HipRight, JointType.SpineBase},

        {JointType.HandTipLeft, JointType.HandLeft},
        {JointType.ThumbLeft, JointType.HandLeft},
        {JointType.HandLeft, JointType.WristLeft},
        {JointType.WristLeft, JointType.ElbowLeft},
        {JointType.ElbowLeft, JointType.ShoulderLeft},
        {JointType.ShoulderLeft, JointType.SpineShoulder},

        {JointType.HandTipRight, JointType.HandRight},
        {JointType.ThumbRight, JointType.HandRight},
        {JointType.HandRight, JointType.WristRight},
        {JointType.WristRight, JointType.ElbowRight},
        {JointType.ElbowRight, JointType.ShoulderRight},
        {JointType.ShoulderRight, JointType.SpineShoulder},

        {JointType.SpineBase, JointType.SpineMid},
        {JointType.SpineMid, JointType.SpineShoulder},
        {JointType.SpineShoulder, JointType.Neck},
        {JointType.Neck, JointType.Head},
    };

    private void Update() {
        timer += Time.deltaTime;
        if(timer < updateInterval) {
            return;
        }
        timer = 0;

        var jointDatas = new List<JointData>();

        foreach(var jointPair in _BoneMap) {
            var errorValue = Random.Range(0f, 0.6f);
            jointDatas.Add(new JointData(jointPair.Key, errorValue, errorValue < 0.2f));
        }

        poseDataEvent.Raise(new PoseData(jointDatas));
    }
}