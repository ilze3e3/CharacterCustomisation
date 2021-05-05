using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public PlayerController player;
    public ClassCustomisation classCustomiser;
    public EquipmentCustomisation equipmentCustomiser;
    public RaceCustomiser raceCustomiser;
    // UI Elements
    public GameObject startButtonGObj;
    public Button startButton;
    public GameObject settingButtonGObj;
    public Button settingButton;
    public GameObject exitButtonGObj;
    public Button exitButton;
    public GameObject mainMenuPanel;

    private void Start()
    {
        startButton = startButtonGObj.GetComponent<Button>();
        settingButton = settingButtonGObj.GetComponent<Button>();
        exitButton = exitButtonGObj.GetComponent<Button>();

        classCustomiser = this.GetComponent<ClassCustomisation>();
        equipmentCustomiser = this.GetComponent<EquipmentCustomisation>();
        raceCustomiser = this.GetComponent<RaceCustomiser>();

        startButton.onClick.AddListener(StartGame);
        
    }

    private void StartGame()
    {
        mainMenuPanel.SetActive(false);
        classCustomiser.Run();
    }   
}
