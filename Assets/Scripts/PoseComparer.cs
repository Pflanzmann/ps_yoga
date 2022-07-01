using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BodyParts))] //make sure BodyParts is there as component
public class PoseComparer : MonoBehaviour {
    [SerializeField] private PoseDataEvent poseDataEvent;

    [SerializeField] private float updateInterval = 2f;
    private float timer = 0f;

    [SerializeField] private float errorOffset;

    private BodyParts poseBodyParts;

    private void Start() {
        poseBodyParts = GetComponent<BodyParts>();
    }

    private void Update() {
        timer += Time.deltaTime;
        if(timer < updateInterval) {
            return;
        }
        timer = 0;

        //check if there is a live body on screen
        if(KinectManager.instance.HasRegisteredBody()) {
            var jointDatas = comparePoses();
            poseDataEvent.Raise(new PoseData(jointDatas));
        }
    }

    private List<JointData> comparePoses() {
        var liveParts = KinectManager.instance.primaryBody.GetComponent<BodyParts>();
        liveParts.CalculateJointDirectionVectors();

        var jointDatas = new List<JointData>();

        foreach(var jointDirection in poseBodyParts.jointDirections) {
            var partKey = jointDirection.Key;

            var poseJointDirection = jointDirection.Value;
            var liveJointDirection = liveParts.jointDirections[partKey];

            var distanceVector = poseJointDirection - liveJointDirection;
            var distanceLength = new Vector2(distanceVector.x, distanceVector.y).magnitude;

            var liveTransform = liveParts.bodyPartTransforms[partKey.ToString()];
            LineRenderer bodyPartLineRenderer = liveTransform.GetComponent<LineRenderer>();
            if(distanceLength > errorOffset) {
                bodyPartLineRenderer.SetColors(Color.red, Color.red);
            } else {
                bodyPartLineRenderer.SetColors(Color.green, Color.green);
            }

            jointDatas.Add(new JointData(partKey, distanceLength, distanceLength < errorOffset));
        }

        return jointDatas;
    }
}