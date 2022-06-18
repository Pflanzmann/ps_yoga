using System.Collections.Generic;
using UnityEngine;

public class PoseManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> poseList;
    [SerializeField] private float poseDuration;
    
    private float timer;
    private int currentPoseIndex = 0;

    private void Start()
    {
        foreach (var pose in poseList)
        {
            pose.SetActive(false);
        }   
        
        poseList[0].SetActive(true);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer < poseDuration)
        {
            return;
        }
        timer = 0;
        
        StartNextPose();
    }

    private void StartNextPose()
    {
        foreach (var pose in poseList)
        {
            pose.SetActive(false);
        }   
        
        currentPoseIndex += 1;
        poseList[currentPoseIndex].SetActive(true);
    }
    
}