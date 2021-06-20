using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public PlayerController player;
    public ClassCustomisation classCustomiser;
    public EquipmentCustomisation equipmentCustomiser;

    // UI Elements
    public GameObject startButtonGObj;
    public Button startButton;
    public GameObject settingButtonGObj;
    public Button settingButton;

    private void Start()
    {
        classCustomiser = this.GetComponent<ClassCustomisation>();
        equipmentCustomiser = this.GetComponent<EquipmentCustomisation>();
        startButton.onClick.AddListener(StartGame);
        
    }

    private void StartGame()
    {
        classCustomiser.isClassActive = true;
    }

    

}
