using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public void selectLevel(string levelNumber){
        SceneManager.LoadScene("Level"+levelNumber);
    }
}
