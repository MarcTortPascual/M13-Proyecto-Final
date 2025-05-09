using System;
using System.Collections;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class Login : MonoBehaviour
{
    //user:test pass:0000
    //
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public string username,password;
    public TMP_InputField  user, pass;

    public string server;
    private StreamReader server_env = new StreamReader("Assets/Assets/server.txt");
    private StreamWriter session_token = new StreamWriter("Assets/Assets/token.txt");
    private UnityWebRequest uwr ;
    void Start()
    {
        server = server_env.ReadToEnd();
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
    void login(){
     
        
    }

    private IEnumerator LoginCo(){

        uwr = new UnityWebRequest(server+"api/login","POST");
        UploadHandlerRaw uhr = new(System.Text.Encoding.UTF8.GetBytes("{"+$"\"username\":\"{username}\",\"password\":\"{password}\""+"}"));
        uhr.contentType = "application/json";
        uwr.uploadHandler = uhr;
        uwr.downloadHandler = new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");
        uwr.SetRequestHeader("Accept", "application/json");
        var response = uwr.SendWebRequest();
        yield return response;
        print(uwr.downloadHandler.text);
    }
}
