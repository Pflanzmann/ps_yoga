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
    private readonly String clientID = "ni4yBfTfQiq3rV5jBLYuDQ";
    private readonly String clientSecret = "GDNIKhxdPC90qt9Zy8Bj3AMH1dBs8j1p";
    private String stateOfAccount;
    // ngrok server uri to forward to localhost
    private readonly String redirectUri = "https://2551-84-170-79-12.eu.ngrok.io";
    // send message body data
    public String message = "test";
    public String to_contact = "felix.kunath@web.de";
    // for getting code to get token
    private String authCode = null;
    private bool gotToken = false;

    public void Start()
    {
        
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
        if (this.accessToken == null) {
            if (this.refreshToken == null)
                StartCoroutine(GetToken());
            else
                StartCoroutine(RefreshToken());
        }
        else
            this.gotToken = true;

        if (this.to_contact == null)
            StartCoroutine(GetHost());
        StartCoroutine(SendMessage());
    }

    private string GetHost()
    {
        throw new NotImplementedException();
    }

    private IEnumerator RefreshToken()
    {
        String clientIdAndSecret = EncodeTo64(clientID + ":" + clientSecret);
        var postRefreshToken = CreateRequest("https://zoom.us/oauth/token?refresh_token=" + this.refreshToken + "&grant_type=refresh_token", RequestType.POST);
        AttachHeader(postRefreshToken, "Authorization", "Basic " + clientIdAndSecret);
        yield return postRefreshToken.SendWebRequest();
        var deserializedPostData = JsonUtility.FromJson<PostGetToken>(postRefreshToken.downloadHandler.text);
        this.accessToken = deserializedPostData.access_token;
        this.refreshToken = deserializedPostData.refresh_token;
        print("Token Data:  " + postRefreshToken.downloadHandler.text);
    }

    private IEnumerator SendMessage()
    {
        while (!this.gotToken)
        {
            yield return new WaitForSeconds(2);
        }
        // POST send message after Bearer enter your OAuthToken
        // to_contact email has to be in your contacts
        var postSendMesData = new SendMessageData() { message = this.message, to_contact = this.to_contact };
        var postReqSendMessage = CreateRequest("https://api.zoom.us/v2/chat/users/me/messages", RequestType.POST, postSendMesData);
        AttachHeader(postReqSendMessage, "Authorization", "Bearer " + this.accessToken);
        yield return postReqSendMessage.SendWebRequest();

        // wenn ergebnis nicht "id: ...." dann fehlerbehandlung -> access token falsch => refresh token
        print("POST DATA1 " + postReqSendMessage.downloadHandler.text);
    }

    private IEnumerator GetToken()
    {
        this.stateOfAccount = System.Guid.NewGuid().ToString();
        Application.OpenURL("https://zoom.us/oauth/authorize?response_type=code&client_id=" + this.clientID + "&redirect_uri=" + this.redirectUri + "&state=" + this.stateOfAccount);

        while (this.authCode == null || this.authCode.Equals("error no code found for given state"))
        {
            yield return new WaitForSeconds(5f);
            var getCode = CreateRequest(redirectUri + "/code/" + this.stateOfAccount, RequestType.GET);
            yield return getCode.SendWebRequest();
            this.authCode = getCode.downloadHandler.text;
            print(authCode);
        }
        
        // Post get oAuth 2.0 access and refresh token
        String clientIdAndSecret = EncodeTo64(clientID + ":" + clientSecret);
        var postToken = CreateRequest("https://zoom.us/oauth/token?code=" + this.authCode + "&grant_type=authorization_code&redirect_uri=" + this.redirectUri, RequestType.POST);
        AttachHeader(postToken, "Authorization", "Basic " + clientIdAndSecret);
        yield return postToken.SendWebRequest();
        var deserializedPostData = JsonUtility.FromJson<PostGetToken>(postToken.downloadHandler.text);
        this.accessToken = deserializedPostData.access_token;
        this.refreshToken = deserializedPostData.refresh_token;
        print("Token Data:  " + postToken.downloadHandler.text);

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
