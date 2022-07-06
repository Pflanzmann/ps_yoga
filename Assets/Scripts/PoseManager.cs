using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PoseManager : MonoBehaviour
{
    public static PoseManager instance;

    [Serializable]
    public class PoseObject
    {
        public GameObject gameObject;
        public Sprite poseSprite;
        public string poseName;
    }

    [SerializeField] private List<PoseObject> poseList;
    [SerializeField] private float poseDuration;
    [SerializeField] private TextMeshProUGUI poseText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Image poseImage;

    private float timer;
    private int currentPoseIndex = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        foreach (var pose in poseList)
        {
            pose.gameObject.SetActive(false);
        }

        currentPoseIndex = -1;
        StartNextPose();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        timerText.text = (poseDuration - timer).ToString().Substring(0, 4);

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
            pose.gameObject.SetActive(false);
        }

        currentPoseIndex += 1;
        poseList[currentPoseIndex % poseList.Count].gameObject.SetActive(true);

        poseText.text = poseList[currentPoseIndex % poseList.Count].poseName + " halten für:";
        poseImage.sprite = poseList[currentPoseIndex % poseList.Count].poseSprite;
    }

    public String GetCurrentPoseName()
    {
        return poseText.text = poseList[currentPoseIndex % poseList.Count].poseName;
    }
}