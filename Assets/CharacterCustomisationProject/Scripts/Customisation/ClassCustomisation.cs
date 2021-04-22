using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassCustomisation : MonoBehaviour
{
    public struct ClassOptions {
        string className;
        float hp;
        float energy;
        float attack;
        float defence;
        float agility;
        float intelligence;

        public ClassOptions(string _name, float _hp, float _energy, float _attack, float _defence, float _agility, float _intelligence)
        {
            className = _name;
            hp = _hp;
            energy = _energy;
            attack = _attack;
            defence = _defence;
            agility = _agility;
            intelligence = _intelligence;
        }

    }

    public GameObject player;
    public List<ClassOptions> classDictionary = new List<ClassOptions>();

    public int classCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        MakeClass();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void MakeClass()
    {
        ClassOptions berserker = new ClassOptions("Berserker", 150, 100, 20, 10, 10, 5);
        ClassOptions shieldBearer = new ClassOptions("ShieldBearer", 300, 150, 5, 40, 10, 5);
        ClassOptions wizard = new ClassOptions("Wizard", 100, 200, 5, 10, 10, 50);
        classDictionary.Add(berserker);
        classDictionary.Add(shieldBearer);
        classDictionary.Add(wizard);
    }

    public void ChooseClassNext()
    {
        classCounter++;
        if(classCounter > classDictionary.Count-1)
        {
            classCounter = classDictionary.Count-1; 
        }
    }
    public void ChooseClassBefore()
    {
        classCounter--;
        if (classCounter < 0)
        {
            classCounter = 0;
        }
    }
}
