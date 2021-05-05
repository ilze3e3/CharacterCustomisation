using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Stat Points
    [SerializeField]
    private string _className;
    public string className { get { return _className; } set { _className = value; } } // The name of their class
    [SerializeField]
    private float _healthPoints;
    public float healthPoints { get { return _healthPoints; } set { _healthPoints = value; } } // The amount of health pool a player has
    [SerializeField]
    private float _energyPoints;
    public float energyPoints { get { return _energyPoints; } set { _energyPoints = value; } } // The amount of energy pool a player has
    [SerializeField]
    private float _attackPoints;
    public float attackPoints { get { return _energyPoints; } set { _energyPoints = value; } } // The amount of attack a player has. Affects melee hits
    [SerializeField]
    private float _defencePoints;
    public float defencePoints { get { return _defencePoints; } set { _defencePoints = value; } } // The amount of defence a player has. Affects the amount of damage taken
    [SerializeField]
    private float _agilityPoints;
    public float agilityPoints { get { return _agilityPoints; } set { _agilityPoints = value; } } // the amount of agility a player has. Affets how fast the player goes
    [SerializeField]
    private float _intelligencePoints;
    public float intelligencePoints { get { return _intelligencePoints; } set { _intelligencePoints = value; } } // the amount of intelligence a player has. Affects magic hits


    // Start is called before the first frame update
    void Start()
    {
        healthPoints = 0;
        energyPoints = 0;
        attackPoints = 0;
        defencePoints = 0;
        agilityPoints = 0;
        intelligencePoints = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayerClass(string _className, float _hp, float _energy, float _attack, float _defence, float _agility, float _intelligence)
    {
        className = _className;
        healthPoints = _hp;
        energyPoints = _energy;
        attackPoints = _attack;
        defencePoints = _defence;
        agilityPoints = _agility;
        intelligencePoints = _intelligence;
    }
}
