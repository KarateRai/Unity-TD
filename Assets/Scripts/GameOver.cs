using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public SceneFader sceneFader;
    public string menuScene = "MainMenu";
    
    public void Retry()
    {
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }


    public void Menu()
    {
        sceneFader.FadeTo(menuScene);
        Debug.Log("Go To Menu");
    }
}
