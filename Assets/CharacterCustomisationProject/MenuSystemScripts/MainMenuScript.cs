using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void CreateNewGame()
    {
        SceneManager.LoadSceneAsync(2);
        PlayerPrefs.SetString("LoadTo", "NewGame");
    }
    public void LoadGame()
    {
        SceneManager.LoadSceneAsync(2);
        PlayerPrefs.SetString("LoadTo", "LoadGame");
    }
    public void GoToOptions()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
