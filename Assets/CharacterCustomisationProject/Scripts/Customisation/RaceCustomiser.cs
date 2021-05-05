using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RaceCustomiser : MonoBehaviour
{
    public struct RaceOptions
    {
        public string className { get; }
        public float hp { get; }
        public float energy { get; }
        public float attack { get; }
        public float defence { get; }
        public float agility { get; }
        public float intelligence { get; }

        public RaceOptions(string _name, float _hp, float _energy, float _attack, float _defence, float _agility, float _intelligence)
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

    //Ui interactable Elements;
    public GameObject racePickerPanel;
    public GameObject prevButtonGObj;
    public Button prevButton;
    public GameObject nextButtonGObj;
    public Button nextButton;
    public GameObject confirmButtonGObj;
    public Button confirmButton;

    // Ui Text Elements
    public TextMeshProUGUI raceNameDisplay;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI energyText;
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI defenceText;
    public TextMeshProUGUI agilityText;
    public TextMeshProUGUI intelligenceText;

    [SerializeField]
    public List<RaceOptions> raceDictionary = new List<RaceOptions>();
    [SerializeField]
    public bool isClassActive { get; set; } = false;
    [SerializeField]
    public bool isClassLoaded { get; set; } = false;

    public int raceCounter = 0;

    private void MakeRace()
    {
        RaceOptions human = new RaceOptions("Human", 100, 200, 5, 10, 10, 50);
        RaceOptions lizardKin = new RaceOptions("LizardKin", 150, 100, 20, 10, 10, 5);
        RaceOptions giant = new RaceOptions("Giant", 300, 150, 5, 40, 10, 5);
        raceDictionary.Add(human);
        raceDictionary.Add(lizardKin);
        raceDictionary.Add(giant);
    }
    private void ChooseRaceNext()
    {
        raceCounter++;
        CheckButtonInteractable();
        UpdateTextUI();
    }
    private void ChooseRaceBefore()
    {
        raceCounter--;
        CheckButtonInteractable();
        UpdateTextUI();
    }
    private void CheckButtonInteractable()
    {
        if (raceCounter == 0)
        {
            prevButton.interactable = false;
        }
        else if (raceCounter == raceDictionary.Count - 1)
        {
            nextButton.interactable = false;
        }
        else
        {
            prevButton.interactable = true;
            nextButton.interactable = true;
        }
    }
    private void UpdateTextUI()
    {
        raceNameDisplay.text = raceDictionary[raceCounter].className;
        healthText.text = "Health: " + raceDictionary[raceCounter].hp.ToString();
        energyText.text = "Energy: " + raceDictionary[raceCounter].energy.ToString();
        attackText.text = "Attack: " + raceDictionary[raceCounter].attack.ToString();
        defenceText.text = "Defence: " + raceDictionary[raceCounter].defence.ToString();
        agilityText.text = "Agility: " + raceDictionary[raceCounter].agility.ToString();
        intelligenceText.text = "Intelligence: " + raceDictionary[raceCounter].intelligence.ToString();

    }

    public void Run()
    {
        isClassActive = true;
        racePickerPanel.SetActive(true);
    }

}
