using UnityEngine;

[RequireComponent(typeof (BodyParts))] //make sure BodyParts is there as component
public class PoseComparer : MonoBehaviour
{
    [SerializeField]
    private float errorOffset;

    private BodyParts poseBodyParts;

    private void Start()
    {
        poseBodyParts = GetComponent<BodyParts>();
    }

    private void Update()
    {
        //check if there is a live body on screen
        if (KinectManager.instance.HasRegisteredBody())
        {
            comparePoses();
        }
    }

    private void comparePoses()
    {
        var liveParts = KinectManager.instance.primaryBody.GetComponent<BodyParts>();
        liveParts.CalculateJointDirectionVectors();

        foreach (var jointDirection in poseBodyParts.jointDirections)
        {
            var partKey = jointDirection.Key;

            var poseJointDirection = jointDirection.Value;
            var liveJointDirection = liveParts.jointDirections[partKey];

            var distanceVector = poseJointDirection - liveJointDirection;
            var distanceLength = new Vector2(distanceVector.x , distanceVector.y).magnitude;

            var liveTransform = liveParts.bodyPartTransforms[partKey.ToString()];
            LineRenderer bodyPartLineRenderer = liveTransform.GetComponent<LineRenderer>();
            if (distanceLength > errorOffset)
            {
                bodyPartLineRenderer.SetColors(Color.red, Color.red);
            }
            else
            {
                bodyPartLineRenderer.SetColors(Color.green, Color.green);
            }
        }
    }
}
