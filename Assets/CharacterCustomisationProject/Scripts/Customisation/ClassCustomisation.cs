using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClassCustomisation : MonoBehaviour
{
    public struct ClassOptions {
        public string className { get; }
        public float hp { get; }
        public float energy { get; }
        public float attack { get; }
        public float defence { get; }
        public float agility { get; }
        public float intelligence { get; }

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

    // Ui interactable Elements
    public GameObject classPickerPanel;
    public GameObject prevButtonGObj;
    public Button prevButton;
    public GameObject nextButtonGObj;
    public Button nextButton;
    public GameObject confirmButtonGObj;
    public Button confirmButton;

    // Ui Text Elements
    public TextMeshProUGUI classNameDisplay;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI energyText;
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI defenceText;
    public TextMeshProUGUI agilityText;
    public TextMeshProUGUI intelligenceText;


    [SerializeField]
    public List<ClassOptions> classDictionary = new List<ClassOptions>();
    [SerializeField]
    public bool isClassActive { get; set; } = false;
    [SerializeField]
    public bool isClassLoaded { get; set; } = false;

    public int classCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        MakeClass();
        prevButton = prevButtonGObj.GetComponent<Button>();
        nextButton = nextButtonGObj.GetComponent<Button>();
        confirmButton = confirmButtonGObj.GetComponent<Button>();

        prevButton.onClick.AddListener(ChooseClassBefore);
        nextButton.onClick.AddListener(ChooseClassNext);
        confirmButton.onClick.AddListener(ConfirmClass);
        UpdateTextUI();
        CheckButtonInteractable();
        isClassLoaded = true;
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

    private void ChooseClassNext()
    {
        classCounter++;
        CheckButtonInteractable();
        UpdateTextUI();
    }
    private void ChooseClassBefore()
    {
        classCounter--;
        CheckButtonInteractable();
        UpdateTextUI();
    }

    private void ConfirmClass()
    {
        player.GetComponent<PlayerController>().SetPlayerClass(classDictionary[classCounter].className,
                                                               classDictionary[classCounter].hp,
                                                               classDictionary[classCounter].energy,
                                                               classDictionary[classCounter].attack,
                                                               classDictionary[classCounter].defence,
                                                               classDictionary[classCounter].agility,
                                                               classDictionary[classCounter].intelligence);
                                                                                                
    }
    
    private void CheckButtonInteractable()
    {
        if (classCounter == 0)
        {
            prevButton.interactable = false;
        }
        else if (classCounter == classDictionary.Count-1)
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
        classNameDisplay.text = classDictionary[classCounter].className;
        healthText.text = "Health: " + classDictionary[classCounter].hp.ToString();
        energyText.text = "Energy: " + classDictionary[classCounter].energy.ToString();
        attackText.text = "Attack: " + classDictionary[classCounter].attack.ToString();
        defenceText.text = "Defence: " + classDictionary[classCounter].defence.ToString();
        agilityText.text = "Agility: " + classDictionary[classCounter].agility.ToString();
        intelligenceText.text = "Intelligence: " + classDictionary[classCounter].intelligence.ToString();

    }

    public void Run()
    {
        isClassActive = true;
        classPickerPanel.SetActive(true);
    }
}
