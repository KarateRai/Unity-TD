using UnityEngine;

public class CompleteLevel : MonoBehaviour
{
    public SceneFader sceneFader;

    public string menuSceneName = "MainMenu";

    public string nextLevel = "Level02";
    public int levelToUnLock = 2;
    

    public void NextLevel()
    {
        PlayerPrefs.SetInt("levelReached", levelToUnLock);
        sceneFader.FadeTo(nextLevel);
    }

    public void Menu()
    {
        sceneFader.FadeTo(menuSceneName);
    }
}
