using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class CustomisationGet : MonoBehaviour
{
    public CharacterStatistics charStats;

    public Renderer characterRenderer;
    public GameObject player;

    [SerializeField]
    Vector3 playerPosition;
    [SerializeField]
    int skinIndex = 0;
    [SerializeField]
    int eyesIndex = 0;
    [SerializeField]
    int mouthIndex = 0;
    [SerializeField]
    int hairIndex = 0;
    [SerializeField]
    int clothesIndex = 0;
    [SerializeField]
    int armourIndex = 0;
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
    string characterClass = "";



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Load();
        //player.AddComponent<CharacterController>();
    }
    /// <summary>
    /// Load the binary save and set the textures and stats according to the save data
    /// </summary>
    void Load()
    {
        string filePath = Application.persistentDataPath + "/save.data";

        if (File.Exists(filePath))
        {
            // File exists  
            FileStream dataStream = new FileStream(filePath, FileMode.Open);

            BinaryFormatter converter = new BinaryFormatter();
            GameData gD = converter.Deserialize(dataStream) as GameData;

            dataStream.Close();
            if (gD.characterPositionX != null ||
                gD.characterPositionY != null ||
                gD.characterPositionZ != null) playerPosition = new Vector3((float)gD.characterPositionX, (float)gD.characterPositionY, (float)gD.characterPositionZ);

            skinIndex = gD.skinIndex;
            eyesIndex = gD.eyesIndex;
            mouthIndex = gD.mouthIndex;
            hairIndex = gD.hairIndex;
            clothesIndex = gD.clothesIndex;
            armourIndex = gD.armourIndex;
            characterName = gD.characterName;
            strengthStat = gD.strengthStat;
            dexterityStat = gD.dexterityStat;
            constitutionStat = gD.constitutionStat;
            intelligenceStat = gD.intelligenceStat;
            wisdomStat = gD.wisdomStat;
            charismaStat = gD.charismaStat;
        }
        else
        {
            // File does not exist
            Debug.LogError("File does not exist at: " + filePath);
        }



        //skinIndex = PlayerPrefs.GetInt("SkinIndex");
        //eyesIndex = PlayerPrefs.GetInt("EyesIndex");
        //mouthIndex = PlayerPrefs.GetInt("MouthIndex");
        //hairIndex = PlayerPrefs.GetInt("HairIndex");
        //clothesIndex = PlayerPrefs.GetInt("ClothesIndex");
        //armourIndex = PlayerPrefs.GetInt("ArmourIndex");
        //characterName = PlayerPrefs.GetString("CharacterName");

        SetTexture("Skin", skinIndex);
        SetTexture("Eyes", eyesIndex);
        SetTexture("Mouth", mouthIndex);
        SetTexture("Hair", hairIndex);
        SetTexture("Clothes", clothesIndex);
        SetTexture("Armour", armourIndex);
        player.name = characterName;

        SetStat();

        if (playerPosition != null) player.transform.position = playerPosition;

    }
    void SetTexture(string type, int index) 
    {
        Texture2D texture = null;
        int matIndex = 0;
        switch(type)
        {
            case "Skin":
                texture = Resources.Load("Character/Skin_" + index) as Texture2D;
                matIndex = 1;
                break;
            case "Eyes":
                texture = Resources.Load("Character/Eyes_" + index) as Texture2D;
                matIndex = 2;
                break;
            case "Mouth":
                texture = Resources.Load("Character/Mouth_" + index) as Texture2D;
                matIndex = 3;
                break;
            case "Hair":
                texture = Resources.Load("Character/Hair_" + index) as Texture2D;
                matIndex = 4;
                break;
            case "Clothes":
                texture = Resources.Load("Character/Clothes_" + index) as Texture2D;
                matIndex = 5;
                break;
            case "Armour":
                texture = Resources.Load("Character/Armour_" + index) as Texture2D;
                matIndex = 6;
                break;
        }

        Material[] mats = characterRenderer.materials;
        mats[matIndex].mainTexture = texture;
        characterRenderer.materials = mats;

        CharacterStatistics charStats = player.GetComponent<CharacterStatistics>();
        charStats.skinIndex = skinIndex;
        charStats.eyesIndex = eyesIndex;
        charStats.mouthIndex = mouthIndex;
        charStats.armourIndex = armourIndex;
        charStats.clothesIndex = clothesIndex;
    }

    void SetStat()
    {
        //strengthStat = PlayerPrefs.GetInt("Strength");
        //dexterityStat = PlayerPrefs.GetInt("Dexterity");
        //constitutionStat = PlayerPrefs.GetInt("Constitution");
        //intelligenceStat = PlayerPrefs.GetInt("Intelligence");
        //wisdomStat = PlayerPrefs.GetInt("Wisdom");
        //charismaStat = PlayerPrefs.GetInt("Charisma");

        charStats.SetCharacterStatistics(characterName, characterClass,strengthStat, dexterityStat, constitutionStat,
                                        intelligenceStat, wisdomStat, charismaStat);
    }
}
