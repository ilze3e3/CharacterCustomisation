using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Stat Points
    public string className { get; set; } // The name of their class
    public float healthPoints { get; set; } // The amount of health pool a player has
    public float staminaPoints { get; set; } // The amount of energy pool a player has (Used up when doing physical actions like running and attacking melee)
    public float manaPoints {get; set;} // The amount of mana pool a player has (Used up when doing magical actions specific to wizard class)
    public float attackPoints { get; set; } // The amount of attack a player has. Affects melee hits
    public float defencePoints { get; set; } // The amount of defence a player has. Affects the amount of damage taken
    public float agilityPoints { get; set; } // the amount of agility a player has. Affets how fast the player goes
    public float intelligencePoints { get; set; } // the amount of intelligence a player has. Affects magic hits


    // Start is called before the first frame update
    void Start()
    {
        healthPoints = 0;
        staminaPoints = 0;
        attackPoints = 0;
        defencePoints = 0;
        agilityPoints = 0;
        intelligencePoints = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayerClass(float _hp, float _energy, float _attack, float _defence, float _agility, float _intelligence)
    {
        healthPoints = _hp;
        staminaPoints = _energy;
        attackPoints = _attack;
        defencePoints = _defence;
        agilityPoints = _agility;
        intelligencePoints = _intelligence;
    }
}
