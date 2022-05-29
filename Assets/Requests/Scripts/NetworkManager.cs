using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour
{
    public void sendRequest()
    {
        StartCoroutine(MakeRequests());
    }

    private IEnumerator MakeRequests()
    {
        // GET
        var getRequest = CreateRequest("https://jsonplaceholder.typicode.com/todos/1");
        // AttachHeader(getRequest, "Authorization", "bearer eeuirhiu23hr2r8934hr983");
        yield return getRequest.SendWebRequest();
        var deserializedGetData = JsonUtility.FromJson<Todo>(getRequest.downloadHandler.text);
        print(deserializedGetData.title);

        // GET List Users
        var postSendMesData = new SendMessageData() { message = "hallo", to_contact = "felix.kunath@web.de" };
        var postReqSendMessage = CreateRequest("https://api.zoom.us/v2/chat/users/me/messages", RequestType.POST, postSendMesData);
        AttachHeader(postReqSendMessage,"Authorization", "Bearer eyJhbGciOiJIUzUxMiIsInYiOiIyLjAiLCJraWQiOiI2ZjgxMmIxMS00ZWIzLTRmNDMtYjJiYS03ODUyMGM5MTFmYzQifQ.eyJ2ZXIiOjcsImF1aWQiOiJmNGNhNGYyZGM1MzJhODJiMzJiMzQ0ZjI1NDQ1YTczYSIsImNvZGUiOiJLTlBiQzk2OFBjX3lKdy1mMGozUnphX0ZZYmhXR0I4UWciLCJpc3MiOiJ6bTpjaWQ6TkZBREJrRXlTUUdQMThaY002aUZEUSIsImdubyI6MCwidHlwZSI6MCwidGlkIjowLCJhdWQiOiJodHRwczovL29hdXRoLnpvb20udXMiLCJ1aWQiOiJ5SnctZjBqM1J6YV9GWWJoV0dCOFFnIiwibmJmIjoxNjUzMzIyOTM3LCJleHAiOjE2NTMzMjY1MzcsImlhdCI6MTY1MzMyMjkzNywiYWlkIjoiT3JIOXBTb29TcE9xMHNtNDlBX1U0QSIsImp0aSI6Ijc5M2M3Y2E2LWFjNjktNDhmNy04ODQ1LWZjNmQ5NjRiYTY1NiJ9.uk3Soc9GbMPigSCj6er7jGoGtKFF7wVxB5v2EAd0UHYkoXVwtyvVGekOAQAi7jR6yBdy9Ulf9NulrCUAmc0C8Q");
        //print(postReqSendMessage.GetRequestHeader);
        yield return postReqSendMessage.SendWebRequest();
        var deserializedPostData1 = JsonUtility.FromJson<PostResult>(postReqSendMessage.downloadHandler.text);

        // POST
        var dataToPost = new PostData() { Hero = "John Wick", PowerLevel = 9001 };
        var postRequest = CreateRequest("https://reqbin.com/echo/post/json", RequestType.POST, dataToPost);
        yield return postRequest.SendWebRequest();
        var deserializedPostData = JsonUtility.FromJson<PostResult>(postRequest.downloadHandler.text);
        // Trigger continuation of game flow
    }


    private UnityWebRequest CreateRequest(string path, RequestType type = RequestType.POST, object data = null)
    {
        var request = new UnityWebRequest(path, type.ToString());

        if (data != null)
        {
            var bodyRaw = Encoding.UTF8.GetBytes(JsonUtility.ToJson(data));
            print("PostBody"+ bodyRaw);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        }

        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        return request;
    }

    private void AttachHeader(UnityWebRequest request, string key, string value)
    {
        request.SetRequestHeader(key, value);
    }
}

public enum RequestType
{
    GET = 0,
    POST = 1,
    PUT = 2
}


public class Todo
{
    // Ensure no getters / setters
    // Typecase has to match exactly
    public int userId;
    public int id;
    public string title;
    public bool completed;
}

[Serializable]
public class PostData
{
    public string Hero;
    public int PowerLevel;
}
[Serializable]
public class SendMessageData
{
    public string message;
    public string to_contact;
}

public class PostResult
{
    public string success { get; set; }
}
