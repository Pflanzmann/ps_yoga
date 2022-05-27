using UnityEngine;

public class KinectManager : MonoBehaviour
{
    public static KinectManager instance;

    public GameObject primaryBody;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void RegisterBody(GameObject liveBody)
    {
        if (primaryBody == null)
        {
            primaryBody = liveBody;
            primaryBody.AddComponent<BodyParts>();
        }
    }

    public void DeregisterBody(GameObject liveBody)
    {
        if (primaryBody == liveBody)
        {
            primaryBody = null;
        }
    }

    public bool HasRegisteredBody()
    {
        return primaryBody != null;
    }
}