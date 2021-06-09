using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatistics : MonoBehaviour
{
    [SerializeField]
    int characterLevel = 1;
    [SerializeField]
    string characterName = "";
    [SerializeField]
    int strengthStat = 0;
    [SerializeField]
    int dexterityStat = 0;
    [SerializeField]
    int constitutionStat = 0;
    [SerializeField]
    int wisdomStat = 0;
    [SerializeField]
    int intelligenceStat = 0;
    [SerializeField]
    int charismaStat = 0;

    [SerializeField]
    int maxHealth = 20;
    [SerializeField]
    int currentHealth = 0;
    [SerializeField]
    int healthRegen = 0;

    [SerializeField]
    int maxStamina = 20;
    [SerializeField]
    int currentStamina = 0;
    [SerializeField]
    int staminaRegen = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth < maxHealth)
        {
            currentHealth += (int)(healthRegen * Time.deltaTime);
        }
        if(currentStamina < maxStamina)
        {
            currentStamina += (int)(staminaRegen * Time.deltaTime);
        }
    }

    public void SetCharacterStatistics(string _name, int _str, int _dex, int _con, int _wis, int _int, int _chr)
    {
        characterName = _name;

        strengthStat = _str;
        dexterityStat = _dex;
        constitutionStat = _con;
        wisdomStat = _wis;
        intelligenceStat = _int;
        charismaStat = _chr;

        maxHealth += GCD(constitutionStat, 5) * 5;
        maxStamina += GCD(strengthStat, 5) * 5;

        healthRegen += GCD(constitutionStat, 5);
        staminaRegen += GCD(strengthStat, 5);

        currentHealth = maxHealth;
        currentStamina = maxStamina;
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
}
