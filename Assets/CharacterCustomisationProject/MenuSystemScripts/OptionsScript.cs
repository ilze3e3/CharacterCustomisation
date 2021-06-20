using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
public class OptionsScript : MonoBehaviour
{
    bool fullscreen = true;
    Vector2Int resolution = new Vector2Int(1920,1080);
    int graphicLevelIndex = 0;
    private int[] values;
    private bool[] keys;

    KeyCode forward;
    KeyCode backward;
    KeyCode left;
    KeyCode right;

    public AudioMixer masterAudioMixer;

    HotkeyRebinder hotkeyRebinder;
    [SerializeField] bool loadKeyStat = false;

    private void Start()
    {
        hotkeyRebinder = FindObjectOfType<HotkeyRebinder>();
        
    }
    private void Update()
    {
        if(!loadKeyStat)
        {
            if(hotkeyRebinder.rebinderStatus)
            {
                hotkeyRebinder.LoadKeys();
                loadKeyStat = true;
            }
        }
    }

    public void ChangeResolution(int val)
    {
        switch(val)
        {
            case 0:
                Debug.Log("Changing Res to 1920x1080");
                resolution = new Vector2Int(1920, 1080);
                break;
            case 1:
                Debug.Log("Changing Res to 640x480");
                resolution = new Vector2Int(640, 480);
                break;
            case 2:
                Debug.Log("Changing Res to 800x600");
                resolution = new Vector2Int(800,600);
                break;
        }
    }

    public void ChangeWindowedFullscreen(int val)
    {
        switch (val)
        {
            case 0:
                Debug.Log("Changing to Full Screen");
                fullscreen = true;
                break;
            case 1:
                Debug.Log("Changing to Windowed Mode");
                fullscreen = false;
                break;
        }
    } 

    public void ChangeGraphicsSettings(int val)
    {
        switch (val)
        {
            case 0:
                Debug.Log("Changing to Low Graphics Setting");
                graphicLevelIndex = 1;
                break;
            case 1:
                Debug.Log("Changing to Medium Graphics Setting");
                graphicLevelIndex = 2;
                break;
            case 2:
                Debug.Log("Changing to High Graphics Setting");
                graphicLevelIndex = 3;
                break;
        }

    }

    public void ConfirmChanges()
    {
        Screen.SetResolution(resolution.x, resolution.y, fullscreen);
        QualitySettings.SetQualityLevel(graphicLevelIndex);

        hotkeyRebinder.SaveKeys();

        switch(PlayerPrefs.GetString("LoadFrom"))
        {
            case "GameScene":
                PlayerPrefs.SetString("LoadTo", "PlayGame");
                PlayerPrefs.SetString("LoadFrom", "Options");
                break;
            case "MainMenu":
                PlayerPrefs.SetString("LoadTo", "MainMenu");
                PlayerPrefs.SetString("LoadFrom", "Options");
                break;
        }

        SceneManager.LoadSceneAsync(2);
    }

    public void CancelChanges()
    {
        switch (PlayerPrefs.GetString("LoadFrom"))
        {
            case "GameScene":
                PlayerPrefs.SetString("LoadTo", "PlayGame");
                PlayerPrefs.SetString("LoadFrom", "Options");
                break;
            case "MainMenu":
                PlayerPrefs.SetString("LoadTo", "MainMenu");
                PlayerPrefs.SetString("LoadFrom", "Options");
                break;
        }
        SceneManager.LoadSceneAsync(2);
    }

    public void ChangeMusicVolume(float volume)
    {
        masterAudioMixer.SetFloat("BGVolume", volume);
    }
    public void ChangeSFXVolume(float volume)
    {
        masterAudioMixer.SetFloat("SFXVolume", volume);
    }
}
