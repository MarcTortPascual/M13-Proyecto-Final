
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    private UnityWebRequest uwr ;
    public string server,bearer_token;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        using (StreamReader server_env = new ("Assets/Resources/server.txt"))
        {
            server = server_env.ReadToEnd();
        }
        using (StreamReader token_env = new ("Assets/Resources/token.txt"))
        {
            bearer_token = token_env.ReadToEnd();
        }
    }
    private IEnumerator Send_Levl(float time){
        uwr = new UnityWebRequest(server+"api/time","POST");
        UploadHandlerRaw uhr = new(System.Text.Encoding.UTF8.GetBytes("{"+$"\"level_id\":\"{SceneManager.GetActiveScene().name.Substring(5)}\",\"time\":\"{Mathf.Floor((time*1000)).ToString()}\",\"completed\":\"{1}\""+"}"));
        print("Bearer "+bearer_token);
        print("{"+$"\"level_id\":\"{SceneManager.GetActiveScene().name.Substring(5)}\",\"time\":\"{Mathf.Floor((time*1000)).ToString()}\",\"completed\":\"true\""+"}");
        uhr.contentType = "application/json";
        uwr.uploadHandler = uhr;
        uwr.downloadHandler = new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");
        uwr.SetRequestHeader("Accept", "application/json");
        uwr.SetRequestHeader("x-api-key","nZaC0OhAm512rgr1BkTEhI8c29Iju0sQNx9IK6eAYs098DJDbI");
        uwr.SetRequestHeader("Authorization","Bearer "+bearer_token);
        var response = uwr.SendWebRequest();
        yield return response;
        print(response.webRequest.downloadHandler.text);
    }
    void OnTriggerEnter2D(Collider2D col){
        if (col.CompareTag("Player")){
            StartCoroutine(Send_Levl(col.GetComponent<Player>().Level_time));
            SceneManager.LoadScene("LevelSelection");
        }
    }
}
