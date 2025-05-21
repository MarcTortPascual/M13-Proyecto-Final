using System;
using System.Text.Json;
using System.Collections;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using System.Text.Json.Serialization;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class TokenResponse{
    [JsonPropertyName("token")]
    public string Token {get; set;}
}
public class Login : MonoBehaviour
{
    //user:test pass:0000
    //
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public string username,password;
    public TMP_InputField  user, pass;
    private TokenResponse tk = new();

    public string server;
    
    private UnityWebRequest uwr ;
    void Start()
    {
        print(Application.streamingAssetsPath);
        using (StreamReader server_env = new(Path.Combine(Application.streamingAssetsPath,"server.txt")))
        {
            server = server_env.ReadToEnd();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void get_credentials(){
        username = user.text;
        password = pass.text;
        StartCoroutine(LoginCo());
    }

    private IEnumerator LoginCo(){
        
        uwr = new UnityWebRequest(server+"api/login","POST");
        UploadHandlerRaw uhr = new(System.Text.Encoding.UTF8.GetBytes("{"+$"\"username\":\"{username}\",\"password\":\"{password}\""+"}"));
        uhr.contentType = "application/json";
        uwr.uploadHandler = uhr;
        uwr.downloadHandler = new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");
        uwr.SetRequestHeader("Accept", "application/json");
        uwr.SetRequestHeader("x-api-key","nZaC0OhAm512rgr1BkTEhI8c29Iju0sQNx9IK6eAYs098DJDbI");
        var response = uwr.SendWebRequest();
        yield return response;
        print(response.webRequest.downloadHandler.text);
        tk = JsonSerializer.Deserialize<TokenResponse>(response.webRequest.downloadHandler.text);
        print(tk.Token);
        using (StreamWriter session_token = new(Path.Combine(Application.streamingAssetsPath,"token.txt"))) {
            session_token.Write(tk.Token);
        }
        
        if (!string.IsNullOrEmpty(tk.Token))
        {
            SceneManager.LoadScene("LevelSelection");
        }
        
    }
}
