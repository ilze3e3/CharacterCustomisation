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

    // Ui Text Elements
    public TextMeshProUGUI classNameDisplay;
    public TextMeshProUGUI HealthText;
    public TextMeshProUGUI EnergyText;
    public TextMeshProUGUI AttackText;
    public TextMeshProUGUI DefenceText;
    public TextMeshProUGUI AgilityText;
    public TextMeshProUGUI IntelligenceText;



    public List<ClassOptions> classDictionary = new List<ClassOptions>();
    public bool isClassActive { get; set; } = false;
    public bool isClassLoaded { get; set; } = false;

    public int classCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        MakeClass();
        prevButton = prevButtonGObj.GetComponent<Button>();
        nextButton = nextButtonGObj.GetComponent<Button>();

        prevButton.onClick.AddListener(ChooseClassBefore);
        prevButton.onClick.AddListener(ChooseClassNext);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (isClassLoaded && isClassActive)
        {
            // Activate the panel for the Class Customisation
            classPickerPanel.SetActive(true);

        }
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
    
    private void CheckButtonInteractable()
    {
        if (classCounter == 0)
        {
            prevButton.interactable = false;
        }
        if (classCounter == classDictionary.Count-1)
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
        HealthText.text = classDictionary[classCounter].hp.ToString();
        EnergyText.text = classDictionary[classCounter].energy.ToString();
        AttackText.text = classDictionary[classCounter].attack.ToString();
        DefenceText.text = classDictionary[classCounter].defence.ToString();
        AgilityText.text = classDictionary[classCounter].agility.ToString();
        IntelligenceText.text = classDictionary[classCounter].intelligence.ToString();

    }
}
