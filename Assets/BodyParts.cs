using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyParts : MonoBehaviour {
    public Dictionary<String, Transform> bodyPartTransforms = new Dictionary<string, Transform>();

    void Start() {
        var transforms = gameObject.GetComponentsInChildren<Transform>();

        foreach(var transform in transforms) {
            bodyPartTransforms.Add(transform.name, transform);
            print(transform.name);
        }

        var parts = KinectManager.instance.primaryBody.GetComponent<BodyParts>();
        foreach(var temp in bodyPartTransforms) {
            var partKey = temp.Key;

            var poseTransform = temp.Value;
            var liveTransform = parts.bodyPartTransforms[partKey];

            var distance = Vector3.Distance(poseTransform.position, liveTransform.position);
        }
    }

    void Update() {
    }
}