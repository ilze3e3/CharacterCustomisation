using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void CreateNewGame()
    {
        PlayerPrefs.SetString("LoadTo", "NewGame");
        SceneManager.LoadSceneAsync(2);
    }
    public void LoadGame()
    {
        PlayerPrefs.SetString("LoadTo", "LoadGame");
        SceneManager.LoadSceneAsync(2);
    }
    public void GoToOptions()
    {
        PlayerPrefs.SetString("LoadTo", "Options");
        PlayerPrefs.SetString("LoadFrom", "MainMenu");
        SceneManager.LoadSceneAsync(2);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
