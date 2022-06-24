using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kinect = Windows.Kinect;

public class BodyParts : MonoBehaviour
{
    public Dictionary<String, Transform>
        bodyPartTransforms = new Dictionary<String, Transform>();

    public Dictionary<Kinect.JointType, Vector3> jointDirections = new Dictionary<Kinect.JointType, Vector3>();
    
    private Dictionary<Kinect.JointType, Kinect.JointType> _BoneMap =
        new Dictionary<Kinect.JointType, Kinect.JointType>()
        {
            { Kinect.JointType.FootLeft, Kinect.JointType.AnkleLeft },
            { Kinect.JointType.AnkleLeft, Kinect.JointType.KneeLeft },
            { Kinect.JointType.KneeLeft, Kinect.JointType.HipLeft },
            { Kinect.JointType.HipLeft, Kinect.JointType.SpineBase },

            { Kinect.JointType.FootRight, Kinect.JointType.AnkleRight },
            { Kinect.JointType.AnkleRight, Kinect.JointType.KneeRight },
            { Kinect.JointType.KneeRight, Kinect.JointType.HipRight },
            { Kinect.JointType.HipRight, Kinect.JointType.SpineBase },

            { Kinect.JointType.HandTipLeft, Kinect.JointType.HandLeft },
            { Kinect.JointType.ThumbLeft, Kinect.JointType.HandLeft },
            { Kinect.JointType.HandLeft, Kinect.JointType.WristLeft },
            { Kinect.JointType.WristLeft, Kinect.JointType.ElbowLeft },
            { Kinect.JointType.ElbowLeft, Kinect.JointType.ShoulderLeft },
            { Kinect.JointType.ShoulderLeft, Kinect.JointType.SpineShoulder },

            { Kinect.JointType.HandTipRight, Kinect.JointType.HandRight },
            { Kinect.JointType.ThumbRight, Kinect.JointType.HandRight },
            { Kinect.JointType.HandRight, Kinect.JointType.WristRight },
            { Kinect.JointType.WristRight, Kinect.JointType.ElbowRight },
            { Kinect.JointType.ElbowRight, Kinect.JointType.ShoulderRight },
            { Kinect.JointType.ShoulderRight, Kinect.JointType.SpineShoulder },

            { Kinect.JointType.SpineBase, Kinect.JointType.SpineMid },
            { Kinect.JointType.SpineMid, Kinect.JointType.SpineShoulder },
            { Kinect.JointType.SpineShoulder, Kinect.JointType.Neck },
            { Kinect.JointType.Neck, Kinect.JointType.Head },
        };

    void OnEnable()
    {
        var transforms = gameObject.GetComponentsInChildren<Transform>();

        foreach (Transform child in transform)
        {
            bodyPartTransforms[child.name] = child;
        }
        
        CalculateJointDirectionVectors();
    }

    public void CalculateJointDirectionVectors()
    {
        foreach (var jointPair in _BoneMap)
        {
            var primaryJoint = bodyPartTransforms[jointPair.Key.ToString()];
            var secondaryJoint = bodyPartTransforms[jointPair.Value.ToString()];

            var directionVector3 = secondaryJoint.position - primaryJoint.position;
            jointDirections[jointPair.Key] = directionVector3.normalized;
        }
    }
}
