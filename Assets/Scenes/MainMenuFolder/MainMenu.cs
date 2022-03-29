using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public string[] levelToLoad;
    public SceneFader sceneFader;
    public bool ballMode = false;

    public void ActivateBallMode()
    {
        if (!ballMode)
        {
            ballMode = true;
        }
        else
        {
            ballMode = false;
        }
    }

    public void Play()
    {
        if (ballMode)
        {
            sceneFader.FadeTo(levelToLoad[0]);
        }
        else
        {
            sceneFader.FadeTo(levelToLoad[1]);
        }
        
    }

    public void Quit()
    {
        Debug.Log("I'm out!");
        Application.Quit();
    }
}
