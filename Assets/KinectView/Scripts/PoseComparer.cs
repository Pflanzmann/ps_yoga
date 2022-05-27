using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BodyParts))] //make sure BodyParts is there as component
public class PoseComparer : MonoBehaviour
{

    private BodyParts poseBodyParts;

    private void Start()
    {
        poseBodyParts = GetComponent<BodyParts>();
        print("2: " + poseBodyParts.bodyPartTransforms.Count);
    }

    private void Update()
    {
        //check if there is a live body on screen
        if (KinectManager.instance.HasRegisteredBody()) {
            comparePoses();
        } 
    }

    private void comparePoses() {

        var liveParts = KinectManager.instance.primaryBody.GetComponent<BodyParts>();
        
        foreach(var poseBodyPart in poseBodyParts.bodyPartTransforms) {
            var partKey = poseBodyPart.Key;
                print("1: " + poseBodyPart);
                

            var poseTransform = poseBodyPart.Value;
            var liveTransform = liveParts.bodyPartTransforms[partKey];

            var distance = Vector3.Distance(poseTransform.position, liveTransform.position);
            if (partKey.Equals("Head")) {
                print(distance);
            }
        }
    }
}
