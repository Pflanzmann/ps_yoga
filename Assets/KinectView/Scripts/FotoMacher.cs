using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FotoMacher : MonoBehaviour
{
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer < 5)
        {
            return;
        }

        GameObject newPose = KinectManager.instance.primaryBody;
        Instantiate(newPose);
        timer = 0;
    }
}
