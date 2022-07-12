using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour
{
    // gernerated tokens or from file 
    private String accessToken = null;
    // client credentials
    private readonly String clientID = "SGzrqOyvRceYFZIoiqB7uQ";
    private String stateOfAccount;
    // ngrok server uri to forward to localhost
    private readonly String redirectUri = "https://springboot-app-yoga-heorku.herokuapp.com";
    // send message body data
    public String message = "hallo";
    public String to_contact = "psyoga-trainer@zoom.htw-berlin.de";
    // for getting code to get token
    private bool gotToken = false;
    private bool isUrlOpen = false;

    public void Start()
    {
        this.to_contact = "psyoga-trainer@zoom.htw-berlin.de";
        this.message = "Hi wie gehts?";
    }
    public void SendRequest()
    {

        StartCoroutine(GetToken());
        StartCoroutine(SendMessage());

    }


    public void SendYogaResult()
    {

        if (this.accessToken == null)
        {
            StartCoroutine(GetToken());
        }
        else
        {
            this.gotToken = true;
        }
        StartCoroutine(SendMessage());

    }

    private IEnumerator SendMessage()
    {
        // timeout after certain amount of tries
        while (!this.gotToken)
        {
            yield return new WaitForSeconds(2);
        }
        // POST send message
        // after Bearer enter your OAuthToken
        // to_contact email has to be in your contacts
        var postSendMesData = new SendMessageData() { message = this.message, to_contact = this.to_contact };
        print(this.to_contact);
        var postReqSendMessage = CreateRequest("https://api.zoom.us/v2/chat/users/me/messages", RequestType.POST, postSendMesData);
        print(this.accessToken);
        AttachHeader(postReqSendMessage, "Authorization", "Bearer " + this.accessToken);
        using (postReqSendMessage)
        {
            yield return postReqSendMessage.SendWebRequest();
            if (postReqSendMessage.responseCode == 201)
            {
                print("no error");
            }
            else
            {
                // retry with and get new token
                this.gotToken = false;
                this.accessToken = null;
                SendYogaResult();
                print("error" + postReqSendMessage.downloadHandler.error);
            }
            // wenn ergebnis nicht "id: ...." dann fehlerbehandlung -> access token falsch => refresh token
            print("POST DATA1 " + postReqSendMessage.downloadHandler.text);
        }
    }

    private IEnumerator GetToken()
    {
        while (this.accessToken == null || this.accessToken.Equals("NO_TOKEN"))
        {
            yield return new WaitForSeconds(2f);
            var getCode = CreateRequest(redirectUri + "/token", RequestType.GET);
            using (getCode)
            {
                yield return getCode.SendWebRequest();
                // when server not accesible
                // and ngrok gives an error in html format
                // dont set authCode to html output
                if (getCode.downloadHandler.text.Equals("NO_TOKEN") && !isUrlOpen)
                {

                    this.stateOfAccount = System.Guid.NewGuid().ToString();
                    Application.OpenURL("https://zoom.us/oauth/authorize?response_type=code&client_id=" + this.clientID + "&redirect_uri=" + this.redirectUri + "&state=" + this.stateOfAccount);
                    this.isUrlOpen = true;
                }
                // error between ngrok and server for tokens
                // response is html
                if (!getCode.downloadHandler.text.Contains("<"))
                    this.accessToken = getCode.downloadHandler.text;
            }
        }
        this.isUrlOpen = false;
        print(this.accessToken);
        this.gotToken = true;

    }

    private UnityWebRequest CreateRequest(string path, RequestType type, object data = null)
    {
        var request = new UnityWebRequest(path, type.ToString());

        if (data != null)
        {
            var bodyRaw = Encoding.UTF8.GetBytes(JsonUtility.ToJson(data));
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        }

        request.downloadHandler = new DownloadHandlerBuffer();
        if (path.Equals("https://zoom.us/oauth/token"))
        {
            print("getToken Post");
            request.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
            return request;
        }
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

[Serializable]
public class SendMessageData
{
    public string message;
    public string to_contact;
}
[Serializable]
public class PostGetToken
{
    public string access_token;
    public String refresh_token;
}
