using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class OptionsScript : MonoBehaviour
{
    bool fullscreen = true;
    Vector2Int resolution = new Vector2Int(1920,1080);
    int graphicLevelIndex = 0;
    int fullscreenWindowedIndex = 0;
    int resolutionIndex = 0;
    float musicVolume = 0;
    float sfxVolume = 0;

    public AudioMixer masterAudioMixer;

    [SerializeField] TMP_Dropdown resolutionDropdown;
    [SerializeField] TMP_Dropdown fullscreenWindowedDropdown;
    [SerializeField] TMP_Dropdown graphicsQualityDropdown;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;

    HotkeyRebinder hotkeyRebinder;
    [SerializeField] bool loadKeyStat = false;

    private void Start()
    {
        hotkeyRebinder = FindObjectOfType<HotkeyRebinder>();
        resolutionDropdown.value = PlayerPrefs.GetInt("ResolutionSetting", 0);
        fullscreenWindowedDropdown.value = PlayerPrefs.GetInt("FullScreenWindowed", 0);
        graphicsQualityDropdown.value = PlayerPrefs.GetInt("GraphicsQuality", 0);

        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0);
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
                resolutionIndex = 0;
                break;
            case 1:
                Debug.Log("Changing Res to 640x480");
                resolution = new Vector2Int(640, 480);
                resolutionIndex = 1;
                break;
            case 2:
                Debug.Log("Changing Res to 800x600");
                resolution = new Vector2Int(800,600);
                resolutionIndex = 2;
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
                fullscreenWindowedIndex = 0;
                break;
            case 1:
                Debug.Log("Changing to Windowed Mode");
                fullscreen = false;
                fullscreenWindowedIndex = 1;
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

        PlayerPrefs.SetInt("ResolutionSetting", resolutionIndex);
        PlayerPrefs.SetInt("FullScreenWindowed", fullscreenWindowedIndex);
        PlayerPrefs.SetInt("GraphicsQuality", graphicLevelIndex);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);

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
        musicVolume = volume;
    }
    public void ChangeSFXVolume(float volume)
    {
        masterAudioMixer.SetFloat("SFXVolume", volume);
        sfxVolume = volume;
    }
}
