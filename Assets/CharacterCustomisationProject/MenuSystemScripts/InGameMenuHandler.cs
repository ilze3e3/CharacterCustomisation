using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class InGameMenuHandler : MonoBehaviour
{
    CharacterStatistics playerStats; 
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject pressAnyKeyPanel;
    [SerializeField] AudioSource bgMusic;
    CanvasGroup pauseMenuCanvasGroup;
    [SerializeField]bool pauseMenuStatus = false;
    [SerializeField] CanvasGroup HUDCanvasGroup;
    [SerializeField] bool isHUDOn = true;
    [SerializeField] GameObject instructionPanel;
    
    IEnumerator PauseFadeOut()
    {
        while (pauseMenuCanvasGroup.alpha > 0.1)
        {
            pauseMenuCanvasGroup.alpha -= 0.9f * Time.deltaTime;
            yield return null;
        }
        
    }
    IEnumerator PauseFadeIn()
    { 
        while (pauseMenuCanvasGroup.alpha < 1.0f)
        {
            pauseMenuCanvasGroup.alpha += 0.9f * Time.deltaTime;
            yield return null;
        }

    }

    IEnumerator HUDFadeOut()
    {
        while (HUDCanvasGroup.alpha > 0)
        {
            HUDCanvasGroup.alpha -= 0.9f * Time.deltaTime;
            yield return null;
        }

    }
    IEnumerator HUDFadeIn()
    {
        while (HUDCanvasGroup.alpha < 1.0f)
        {
            HUDCanvasGroup.alpha += 0.9f * Time.deltaTime;
            yield return null;
        }

    }

    private void Start()
    {
        playerStats = FindObjectOfType<CharacterStatistics>();
    }
    void Update()
    {
        
        if(pressAnyKeyPanel.activeSelf && Input.anyKeyDown)
        {
            pressAnyKeyPanel.SetActive(false);
            bgMusic.Play();

        }
        
        if(Input.GetKeyDown(KeyCode.Escape) && !pauseMenuStatus)
        {
            pauseMenu.SetActive(true);
            pauseMenuCanvasGroup = pauseMenu.GetComponent<CanvasGroup>();
            StartCoroutine("PauseFadeIn");
            pauseMenuStatus = true;
            if(pauseMenuCanvasGroup.alpha >= 1) Time.timeScale = 0;
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && pauseMenuStatus)
        {
            pauseMenuCanvasGroup = pauseMenu.GetComponent<CanvasGroup>();
            Time.timeScale = 1;
            StartCoroutine("PauseFadeOut");
            pauseMenu.SetActive(false);
            pauseMenuStatus = false;
        }
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1;
        PlayerPrefs.SetString("LoadTo", "BackToMenu");
        SceneManager.LoadSceneAsync(2);
        
    }
    public void GoToOptions()
    {
        Save();
        PlayerPrefs.SetString("LoadTo", "Options");
        PlayerPrefs.SetString("LoadFrom", "GameScene");
        PlayerPrefs.Save();
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(2);
    }
    public void Save()
    {
        GameData gD = new GameData();

        gD.characterPositionX = playerStats.transform.position.x;
        gD.characterPositionY = playerStats.transform.position.y;
        gD.characterPositionZ = playerStats.transform.position.z;

        gD.skinIndex = playerStats.skinIndex;
        gD.hairIndex = playerStats.hairIndex;
        gD.eyesIndex = playerStats.eyesIndex;
        gD.mouthIndex = playerStats.mouthIndex;
        gD.clothesIndex = playerStats.clothesIndex;
        gD.armourIndex = playerStats.armourIndex;
        gD.characterName = playerStats.characterName;
        gD.characterClass = playerStats.characterClass;

        gD.strengthStat = playerStats.strengthStat;
        gD.dexterityStat = playerStats.dexterityStat;
        gD.constitutionStat = playerStats.constitutionStat;
        gD.intelligenceStat = playerStats.intelligenceStat;
        gD.wisdomStat = playerStats.wisdomStat;
        gD.charismaStat = playerStats.charismaStat;

        gD.maxHealth = playerStats.maxHealth;
        gD.currentHealth = playerStats.currentHealth;
        gD.healthRegen = playerStats.healthRegen;
        gD.healthPerLevelUp = playerStats.healthPerLevelUp;

        gD.maxStamina = playerStats.maxStamina;
        gD.currentStamina = playerStats.currentStamina;
        gD.staminaRegen = playerStats.staminaRegen;
        gD.staminaPerLevelUp = playerStats.staminaPerLevelUp;

        gD.maxMana = playerStats.maxMana;
        gD.currentMana = playerStats.currentMana;
        gD.manaRegen = playerStats.manaRegen;
        gD.manaPerLevelUp = playerStats.manaPerLevelUp;

        string filePath = Application.persistentDataPath + "/save.data";

        FileStream dataStream = new FileStream(filePath, FileMode.Create);

        BinaryFormatter converter = new BinaryFormatter();
        converter.Serialize(dataStream, gD);

        dataStream.Close();
    }

    public void Load()
    {
        SceneManager.LoadSceneAsync(2);
        PlayerPrefs.SetString("LoadTo", "PlayGame");
    }

    public void ExitToDesktop()
    {
        Application.Quit();
    }

    public void ShowHideHUD()
    {
        if(isHUDOn)
        {
            StartCoroutine("HUDFadeOut");
            isHUDOn = false;
        }
        else if (!isHUDOn)
        {
            StartCoroutine("HUDFadeIn");
            isHUDOn = true;
        }
    }

    public void ShowHideInstruction()
    {
       if(instructionPanel.activeSelf)
        {
            instructionPanel.SetActive(false);
        }
       else
        {
            instructionPanel.SetActive(true);
        }
    }
}
