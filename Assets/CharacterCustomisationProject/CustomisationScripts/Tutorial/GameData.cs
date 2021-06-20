using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    
    public string characterName = "";

    public string characterClass = "";

    public float? characterPositionX;
    public float? characterPositionY;
    public float? characterPositionZ;

    public int skinIndex = 0;
     
    public int eyesIndex = 0;
     
    public int mouthIndex = 0;
     
    public int hairIndex = 0;
     
    public int clothesIndex = 0;
     
    public int armourIndex = 0;
     
    public int strengthStat = 0;
     
    public int dexterityStat = 0;
     
    public int constitutionStat = 0;
     
    public int wisdomStat = 0;
     
    public int intelligenceStat = 0;
     
    public int charismaStat = 0;

    public float maxHealth = 0;
    public float currentHealth = 0;
    public float healthRegen = 0;
    public float healthPerLevelUp = 0;

    public float maxStamina = 0;
    public float currentStamina = 0;
    public float staminaRegen = 0;
    public float staminaPerLevelUp = 0;

    public float maxMana = 0;
    public float currentMana = 0;
    public float manaRegen = 0;
    public float manaPerLevelUp = 0;

    public float runStaminaCost = 0;
  

    public GameData()
    {

    }
}
