using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public static void LoadGame(){
        SceneManager.LoadScene(1);
    }

    public static void LoadMainMenu(){
        SceneManager.LoadScene(0);
    }

    public static void LoadScoreBoard(){
        SceneManager.LoadScene(2);
    }

    public static void LoadExitQ(){
        SceneManager.LoadScene(3);
    }

    public static void CloseApp(){
        Application.Quit();
    }
}
