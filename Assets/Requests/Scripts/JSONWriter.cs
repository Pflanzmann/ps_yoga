using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class JSONWriter : MonoBehaviour
{
    [Serializable]
    public class Tokens
    {
        public string authorizeToken;
        public string refreshToken;
    }

    public Tokens tokens;
    public TextAsset jsonFile;

    public void WriteToken(string oAuth, string refresh)
    {
        tokens.authorizeToken = oAuth;
        tokens.refreshToken = refresh;
        string jsonToWrite = JsonUtility.ToJson(tokens);
        File.WriteAllText(Application.dataPath + "/Requests/tokens.txt", jsonToWrite);
    }
    public string[] ReadTokens()
    {
        //StreamReader inp_stm = new StreamReader("tokenData");

        //while (!inp_stm.EndOfStream)
        //{
        //    string inp_ln = inp_stm.ReadLine();
        //    // Do Something with the input. 
        //}

        //inp_stm.Close();
        print(jsonFile.text);
        tokens = JsonUtility.FromJson<Tokens>(jsonFile.text);
        string[] tokenArray = new string[2];
        tokenArray[0] = tokens.authorizeToken;
        tokenArray[1] = tokens.refreshToken;
        print(tokenArray[0]);
        return tokenArray;
    }

}
