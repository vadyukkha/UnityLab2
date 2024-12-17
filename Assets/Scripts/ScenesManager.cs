using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    void Start()
    {
        if (PlayerPrefs.GetInt("flag") == 0) 
        {
            PlayerPrefs.SetFloat("sensitivity", 0.5f);
            PlayerPrefs.SetInt("flag", 1);
        }
    }

    public void LoadLevelScene()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadSettingScene()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadStatsScene()
    {
        SceneManager.LoadScene(3);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
