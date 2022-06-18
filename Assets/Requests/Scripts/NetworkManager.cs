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
    private String refreshToken = null;

    // client credentials
    private readonly String clientID = "dvdxc0Z2SsWaOebq32qlvA";
    private readonly String clientSecret = "u8Wmy1Emv3p1V1ieP68GPjmDgvyVdJuV";
    private String stateOfAccount;
    // ngrok server uri to forward to localhost
    private readonly String redirectUri = "https://bb5e-2a00-20-6041-c0c1-d98c-a8a4-ea1d-a59f.eu.ngrok.io";
    // send message body data
    public String message = "hallo";
    public String to_contact = "timo.ji317@web.de";
    // for getting code to get token
    private String authCode = null;
    private bool gotToken = false;
    private bool isUrlOpen = false;

    public void Start()
    {
        this.to_contact = "timo.ji317@web.de";
    }
    public void SendRequest()
    {
        
        //JSONWriter jsonWriter = new JSONWriter();
        //jsonWriter.WriteToken("abc","def");
        //jsonWriter.ReadTokens();

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
            this.gotToken = true;
            StartCoroutine(SendMessage());
    }

    private IEnumerator SendMessage()
    {
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
        AttachHeader(postReqSendMessage, "Authorization", "Bearer " + this.accessToken);
        yield return postReqSendMessage.SendWebRequest();
        if (postReqSendMessage.responseCode == 201)
            print("no error");
        // wenn ergebnis nicht "id: ...." dann fehlerbehandlung -> access token falsch => refresh token
        print("POST DATA1 " + postReqSendMessage.downloadHandler.text);
    }

    private IEnumerator GetToken()
    {
     
        //yield return new WaitForSeconds(100f);
        // while the server doesn't answer with an code
        // retry every 5 seconds to get code
        //while (this.authCode == null || this.authCode.Equals("error no code found for given state"))
        //{
        //    yield return new WaitForSeconds(5f);
        //    var getCode = CreateRequest(redirectUri + "/code/" + this.stateOfAccount, RequestType.GET);
        //    yield return getCode.SendWebRequest();
        //    // when server not accesible
        //    // and ngrok gives an error in html format
        //    // dont set authCode to html output
        //    if(!getCode.downloadHandler.text.Contains("<"))
        //        this.authCode = getCode.downloadHandler.text;
        //}
        while (this.accessToken == null || this.accessToken.Equals("NO_TOKEN"))
        {
            yield return new WaitForSeconds(2f);
            var getCode = CreateRequest(redirectUri + "/token", RequestType.GET);
            yield return getCode.SendWebRequest();
            // when server not accesible
            // and ngrok gives an error in html format
            // dont set authCode to html output
            if (getCode.downloadHandler.text.Equals("NO_TOKEN") && !isUrlOpen){

                this.stateOfAccount = System.Guid.NewGuid().ToString();
                Application.OpenURL("https://zoom.us/oauth/authorize?response_type=code&client_id=" + this.clientID + "&redirect_uri=" + this.redirectUri + "&state=" + this.stateOfAccount);
                this.isUrlOpen = true;
            }
            if (!getCode.downloadHandler.text.Contains("<"))
                this.accessToken = getCode.downloadHandler.text;
        }
        this.isUrlOpen = false;

        // Post get oAuth 2.0 access and refresh token
        //String clientIdAndSecret = EncodeTo64(clientID + ":" + clientSecret);
        //var postToken = CreateRequest("https://zoom.us/oauth/token?code=" + this.authCode + "&grant_type=authorization_code&redirect_uri=" + this.redirectUri, RequestType.POST);
        //AttachHeader(postToken, "Authorization", "Basic " + clientIdAndSecret);
        //yield return postToken.SendWebRequest();
        //var deserializedPostData = JsonUtility.FromJson<PostGetToken>(postToken.downloadHandler.text);
        //this.accessToken = deserializedPostData.access_token;
        //this.refreshToken = deserializedPostData.refresh_token;
        //print("Token Data:  " + postToken.downloadHandler.text);
        print(this.accessToken);
        this.gotToken = true;

    }

    private UnityWebRequest CreateRequest(string path, RequestType type, object data = null)
    {
        var request = new UnityWebRequest(path, type.ToString());

        if (data != null)
        {
            var bodyRaw = Encoding.UTF8.GetBytes(JsonUtility.ToJson(data));
            //print("PostBody"+ bodyRaw);
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

    static public string EncodeTo64(string toEncode)
    {
        byte[] toEncodeAsBytes
              = Encoding.ASCII.GetBytes(toEncode);
        string returnValue
              = Convert.ToBase64String(toEncodeAsBytes);
        return returnValue;
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

public class PostResult
{
    public string success { get; set; }
}
