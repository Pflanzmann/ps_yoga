using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TokenClassName : MonoBehaviour
{
    [Serializable]
    public class TokenName
    {
        public string access_token;
    }

    private static IEnumerator GetAccessToken(Action<string> result)
    {
        Dictionary<string, string> content = new Dictionary<string, string>();
        //Fill key and value
        content.Add("grant_type", "client_credentials");
        content.Add("client_id", "login-secret");
        content.Add("client_secret", "secretpassword");

        UnityWebRequest www = UnityWebRequest.Post("https://someurl.com//oauth/token", content);
        //Send request
        yield return www.Send();

        if (!www.isNetworkError)
        {
            string resultContent = www.downloadHandler.text;
            TokenName json = JsonUtility.FromJson<TokenName>(resultContent);

            //Return result
            result(json.access_token);
        }
        else
        {
            //Return null
            result("");
        }
    }
}
