using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class CharacterStatistics : MonoBehaviour
{
    #region Character Stats
    [SerializeField]
    public int characterLevel = 1;
    [SerializeField]
    public string characterName = "";
    [SerializeField]
    public string characterClass = "";
    [SerializeField]
    public int strengthStat = 0;
    [SerializeField]
    public int dexterityStat = 0;
    [SerializeField]
    public int constitutionStat = 0;
    [SerializeField]
    public int wisdomStat = 0;
    [SerializeField]
    public int intelligenceStat = 0;
    [SerializeField]
    public int charismaStat = 0;

    [SerializeField]
    public float maxHealth = 20;
    [SerializeField]
    public float currentHealth = 0;
    [SerializeField]
    public float healthRegen = 0;
    [SerializeField]
    public float healthPerLevelUp = 0;

    [SerializeField]
    public float maxStamina = 20;
    [SerializeField]
    public float currentStamina = 0;
    [SerializeField]
    public float staminaRegen = 0;
    [SerializeField]
    public float staminaPerLevelUp = 0;

    [SerializeField]
    public float maxMana = 0;
    [SerializeField]
    public float currentMana = 0;
    [SerializeField]
    public float manaRegen = 0;
    [SerializeField]
    public float manaPerLevelUp = 0;

    [SerializeField]
    public float runStaminaCost = 15;
    public bool isPlayerRunning;
    #endregion

    public int skinIndex;
    public int eyesIndex, mouthIndex, hairIndex, armourIndex, clothesIndex;
    bool isDead = false;
    [SerializeField] Vector3 spawnPoint;
    [SerializeField] AudioSource deathSFX;
    [SerializeField] AudioSource respawnSFX;
    [SerializeField] AudioSource bgMusic;
    bool playedOnce = false;
    bool bgMusicPlayedOnce = false;
    
    #region StatHUDComponents
    [SerializeField]
    TextMeshProUGUI levelTxt;
    [SerializeField]
    Image hpBar;
    [SerializeField]
    TextMeshProUGUI hpText;

    [SerializeField]
    Image staminaBar;
    [SerializeField]
    TextMeshProUGUI staminaText;

    [SerializeField]
    Image manaBar;
    [SerializeField]
    TextMeshProUGUI manaText;

    [SerializeField] GameObject deathPanel;
    [SerializeField] GameObject crosshair;

    #endregion
    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(bgMusic != null && !bgMusicPlayedOnce)
        {
            bgMusic.Play();
            bgMusicPlayedOnce = true;
        }
        if (isPlayerRunning)
        {
            currentStamina -= runStaminaCost * Time.deltaTime;
        }
        if (deathPanel != null && currentHealth <= 0)
        {
            isDead = true;
            deathPanel.SetActive(true);
            crosshair.SetActive(false);
            if (!playedOnce)
            {
                playedOnce = true;
                deathSFX.Play();
                bgMusic.Stop();
            }
        }
    }

    public void SetCharacterStatistics(string _name, string _className, int _str, int _dex, int _con, int _wis, int _int, int _chr)
    {
        characterName = _name;
        characterClass = _className;
        strengthStat = _str;
        dexterityStat = _dex;
        constitutionStat = _con;
        wisdomStat = _wis;
        intelligenceStat = _int;
        charismaStat = _chr;

        maxHealth += GCD(constitutionStat, 5) * 5;
        maxStamina += GCD(strengthStat, 5) * 5;
        maxMana += GCD(intelligenceStat, 2) * 5;

        healthRegen += GCD(constitutionStat, 5);
        staminaRegen += GCD(strengthStat, 5);
        manaRegen += GCD(intelligenceStat, 2);

        healthPerLevelUp += GCD(constitutionStat, 5) * 3;

        staminaPerLevelUp += GCD(strengthStat, 5) * 3;

        manaPerLevelUp += GCD(intelligenceStat, 2) * 3;

        currentHealth = maxHealth;
        currentStamina = maxStamina;
        currentMana = maxMana;

       

    }

    private static int GCD(int a, int b)
    {
        while (a != 0 && b != 0)
        {
            if (a > b)
                a %= b;
            else
                b %= a;
        }

        return a | b;
    }

    public void TakeDamage(int _damage)
    {
        currentHealth -= _damage;
    }

    public void LevelUpCharacter()
    {
        characterLevel++;
        maxHealth += healthPerLevelUp;
        currentHealth += healthPerLevelUp;

        maxStamina += staminaPerLevelUp;
        currentStamina += staminaPerLevelUp;

        maxMana += manaPerLevelUp;
        currentMana += manaPerLevelUp;
    }

    public void RefreshStat()
    {
        Debug.Log("refreshing stat");
        if (!isDead)
        {
            if (currentHealth < maxHealth)
            {
                currentHealth += (healthRegen * Time.deltaTime);
                if (currentHealth > maxHealth) currentHealth = maxHealth;
            }
            if (currentStamina < maxStamina)
            {
                Debug.Log("regen stamina");
                currentStamina += (staminaRegen * Time.deltaTime);
                if (currentStamina > maxStamina) currentStamina = maxStamina;
            }
            if (currentMana < maxMana)
            {
                currentMana += (manaRegen * Time.deltaTime);
                if (currentMana > maxMana) currentMana = maxMana;
            }
        }
        RefreshHUD();
    }

    public void RefreshHUD()
    {
        levelTxt.text = "Character Level: " + characterLevel;
        hpBar.fillAmount = currentHealth / maxHealth;
        hpText.text = currentHealth.ToString("0.00") + "/" + maxHealth.ToString();

        staminaBar.fillAmount = currentStamina / maxStamina;
        staminaText.text = currentStamina.ToString("0.00") + "/" + maxStamina.ToString();

        manaBar.fillAmount = currentMana / maxMana;
        manaText.text = currentMana.ToString("0.00") + "/" + maxMana.ToString();
    }

    public void Respawn()
    {
        this.GetComponentInParent<Transform>().position = spawnPoint;
        currentHealth = maxHealth;
        currentStamina = maxStamina;
        currentMana = maxMana;
        isDead = false;
        crosshair.SetActive(true);
        respawnSFX.Play();
        playedOnce = false;
        bgMusic.Play();
    }
}
