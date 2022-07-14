using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseMaker : MonoBehaviour
{
    float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer < 10)
        {
            return;
        }

        GameObject newPose = KinectManager.instance.primaryBody;
        Instantiate(newPose);
        timer = 0;
    }
}
