using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (BodyParts))] //make sure BodyParts is there as component
public class PoseComparer : MonoBehaviour
{
    [SerializeField]
    private double errorOffset;

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
        var liveParts =
            KinectManager.instance.primaryBody.GetComponent<BodyParts>();

        foreach (var poseBodyPart in poseBodyParts.bodyPartTransforms)
        {
            var partKey = poseBodyPart.Key;

            var poseTransform = poseBodyPart.Value;
            var liveTransform = liveParts.bodyPartTransforms[partKey];

            var distance =
                Vector2
                    .Distance(poseTransform.position, liveTransform.position);

            LineRenderer bodyPartLineRenderer =
                liveTransform.GetComponent<LineRenderer>();
            if (distance > errorOffset)
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
