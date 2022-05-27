using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyParts : MonoBehaviour
{
    public Dictionary<String, Transform>
        bodyPartTransforms = new Dictionary<String, Transform>();

    void OnEnable()
    {
        var transforms = gameObject.GetComponentsInChildren<Transform>();

        foreach (Transform child in transform)
        {
            bodyPartTransforms[child.name] = child;
        }
    }
}
