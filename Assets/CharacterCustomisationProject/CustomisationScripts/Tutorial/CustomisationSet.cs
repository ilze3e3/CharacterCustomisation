using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;
using TMPro;


public class CustomisationSet : MonoBehaviour
{
    [Header("Character Name")]
    public string characterName;

    [Header("Character Class")]
    public CharacterTutorialClass characterClass = CharacterTutorialClass.Barbarian;
    public string[] selectedClass = new string[3];
    public int selectedClassIndex = 0;
    [System.Serializable]
    public struct Stats
    {
        public string baseStatsName;
        public int baseStats;
        public int tempStats;
    }
    public Stats[] characterStats;

    [Header("Dropdown Menu")]
    public bool showDropdown;
    public Vector2 scrollPos;
    public string classButton = "";
    public int statPoints = 10;

    [Header("Texture Lists")]
    public List<Texture2D> skin = new List<Texture2D>();
    public List<Texture2D> eyes = new List<Texture2D>();
    public List<Texture2D> hair = new List<Texture2D>();
    public List<Texture2D> mouth = new List<Texture2D>();
    public List<Texture2D> armour = new List<Texture2D>();
    public List<Texture2D> clothes = new List<Texture2D>();

    [Header("Index")]
    public int skinIndex;    
    public int eyesIndex, mouthIndex, hairIndex, armourIndex, clothesIndex;

    [Header("Renderer")]
    public Renderer characterRenderer;

    [Header("Max amount of textures per type")]
    public int skinMax;
    public int eyesMax, mouthMax, hairMax, armourMax, clothesMax;

    [Header("Material Name")]
    public string[] matName = new string[6];

    [SerializeField] GameObject leftRightPrefab;
    [SerializeField] GameObject headingPrefab;
    [SerializeField] GameObject customisationPanelParent;
    [SerializeField] TextMeshProUGUI pointText;
    [SerializeField] TextMeshProUGUI strText;
    [SerializeField] TextMeshProUGUI dexText;
    [SerializeField] TextMeshProUGUI conText;
    [SerializeField] TextMeshProUGUI intText;
    [SerializeField] TextMeshProUGUI wisText;
    [SerializeField] TextMeshProUGUI chrText;

    private void Awake()
    {
        matName = new string[6] { "Skin", "Eyes", "Mouth", "Hair", "Clothes", "Armour" };
        selectedClass = new string[] { "Barbarian", "Bard", "Druid" };

        for (int i = 0; i < skinMax; i++)
        {
            Texture2D tempTexture = Resources.Load("Character/Skin_" + i) as Texture2D;
            skin.Add(tempTexture);
        }
        for (int i = 0; i < eyesMax; i++)
        {
            Texture2D tempTexture = Resources.Load("Character/Eyes_" + i) as Texture2D;
            eyes.Add(tempTexture);
        }
        for (int i = 0; i < mouthMax; i++)
        {
            Texture2D tempTexture = Resources.Load("Character/Mouth_" + i) as Texture2D;
            mouth.Add(tempTexture);
        }
        for (int i = 0; i < hairMax; i++)
        {
            Texture2D tempTexture = Resources.Load("Character/Hair_" + i) as Texture2D;
            hair.Add(tempTexture);
        }
        for (int i = 0; i < clothesMax; i++)
        {
            Texture2D tempTexture = Resources.Load("Character/Clothes_" + i) as Texture2D;
            clothes.Add(tempTexture);
        }
        for (int i = 0; i < armourMax; i++)
        {
            Texture2D tempTexture = Resources.Load("Character/Armour_" + i) as Texture2D;
            armour.Add(tempTexture);
        }

    }

    /// <summary>
    /// Define all the different possible materials. 
    /// then for each material, grab all associated textures from the resources folder then store them to the corresponding list
    /// </summary>
    private void Start()
    {
        pointText.text = "Points: " + statPoints.ToString();
        RefreshText();
    }
    
    public void SetTextureNext(string type)
    {
        SetTexture(type, 1);
    }
    public void SetTexturePrev(string type)
    {
        SetTexture(type, -1);
    }

    

    /// <summary>
    /// Set the texture to the material
    /// </summary>
    /// <param name="type"> type of material </param>
    /// <param name="dir"> the index of the texture </param>
    public void SetTexture(string type, int dir)
        {
        Debug.Log("In Settexture");
            int index = 0, max = 0, matIndex = 0;
            Texture2D[] textures = new Texture2D[0];

            switch (type)
            {
                case "Skin":
                    index = skinIndex;
                    max = skinMax;
                    textures = skin.ToArray();
                    matIndex = 1;
                    break;
                case "Eyes":
                    index = eyesIndex;
                    max = eyesMax;
                    textures = eyes.ToArray();
                    matIndex = 2;
                    break;
                case "Mouth":
                    index = mouthIndex;
                    max = mouthMax;
                    textures = mouth.ToArray();
                    matIndex = 3;
                    break;
                case "Hair":
                    index = hairIndex;
                    max = hairMax;
                    textures = hair.ToArray();
                    matIndex = 4;
                    break;
                case "Clothes":
                    index = clothesIndex;
                    max = clothesMax;
                    textures = clothes.ToArray();
                    matIndex = 5;
                    break;
                case "Armour":
                    index = armourIndex;
                    max = armourMax;
                    textures = armour.ToArray();
                    matIndex = 6;
                    break;
                
            }
            index += dir;
            if (index < 0) index = max - 1;
            if (index > (max - 1)) index = 0;
            Material[] mat = characterRenderer.materials;
            mat[matIndex].mainTexture = textures[index];
            characterRenderer.materials = mat;

            switch (type)
            {
                case "Skin":
                    skinIndex = index;
                    break;
                case "Eyes":
                    eyesIndex = index;
                    break;
                case "Mouth":
                    mouthIndex = index;
                    break;
                case "Hair":
                    hairIndex = index;
                    break;
                case "Clothes":
                    clothesIndex = index;
                    break;
                case "Armour":
                    armourIndex = index;
                    break;
            }
        }
    /// <summary>
    /// Choose from one of the predefined classes
    /// </summary>
    /// <param name="_classIndex"> index of class chosen </param>
    public void ChooseClass(int _classIndex)
    {
        switch (_classIndex)
        {
            case 0:
                characterClass = CharacterTutorialClass.Barbarian;
                characterStats[0].baseStats = 18;
                characterStats[1].baseStats = 8;
                characterStats[2].baseStats = 15;
                characterStats[3].baseStats = 7;
                characterStats[4].baseStats = 7;
                characterStats[5].baseStats = 10;

                break;
            case 1:
                characterClass = CharacterTutorialClass.Bard;
                characterStats[0].baseStats = 10;
                characterStats[1].baseStats = 10;
                characterStats[2].baseStats = 12;
                characterStats[3].baseStats = 12;
                characterStats[4].baseStats = 15;
                characterStats[5].baseStats = 15;
                break;
            case 2:
                characterClass = CharacterTutorialClass.Druid;
                characterStats[0].baseStats = 8;
                characterStats[1].baseStats = 15;
                characterStats[2].baseStats = 9;
                characterStats[3].baseStats = 12;
                characterStats[4].baseStats = 12;
                characterStats[5].baseStats = 8;
                break;
        }

        RefreshText();
    }

    public void IncreaseStat(int i)
    {
        if(statPoints > 0)
        {
            statPoints--;
            pointText.text = "Points: " + statPoints;
            characterStats[i].tempStats++;
        }
        RefreshText();
    }
    public void ReduceStat(int i)
    {
        if(characterStats[i].tempStats > 0)
        {
            statPoints++;
            pointText.text = "Points: " + statPoints;
            characterStats[i].tempStats--;
        }
        RefreshText();
    }

    /// <summary>
    /// Save Character data and stats to a binary save
    /// </summary>
    /// 
    public void SaveCharacter()
    {
        GameData gD = new GameData();

        gD.skinIndex = skinIndex;
        gD.hairIndex = hairIndex;
        gD.eyesIndex = eyesIndex;
        gD.mouthIndex = mouthIndex;
        gD.clothesIndex = clothesIndex;
        gD.armourIndex = armourIndex;
        gD.characterName = characterName;
        gD.characterClass = selectedClass[selectedClassIndex];

        foreach(Stats s in characterStats)
        {
            switch(s.baseStatsName)
            {
                case "Strength":
                    gD.strengthStat = s.baseStats + s.tempStats;
                    break;
                case "Dexterity":
                    gD.dexterityStat = s.baseStats + s.tempStats;
                    break;
                case "Constitution":
                    gD.constitutionStat = s.baseStats + s.tempStats;
                    break;
                case "Intelligence":
                    gD.intelligenceStat = s.baseStats + s.tempStats;
                    break;
                case "Wisdom":
                    gD.wisdomStat = s.baseStats + s.tempStats;
                    break;
                case "Charisma":
                    gD.charismaStat = s.baseStats + s.tempStats;
                    break;
            }
        }

        string filePath = Application.persistentDataPath + "/save.data";

        FileStream dataStream = new FileStream(filePath, FileMode.Create);

        BinaryFormatter converter = new BinaryFormatter();
        converter.Serialize(dataStream, gD);

        dataStream.Close();

        SceneManager.LoadScene(2);
        PlayerPrefs.SetString("LoadTo", "PlayGame");
        PlayerPrefs.SetString("LoadFrom", "CustomisationScene");

        //PlayerPrefs.SetInt("SkinIndex", skinIndex);
        //PlayerPrefs.SetInt("HairIndex", hairIndex);
        //PlayerPrefs.SetInt("EyesIndex", eyesIndex);
        //PlayerPrefs.SetInt("MouthIndex", mouthIndex);
        //PlayerPrefs.SetInt("ClothesIndex", clothesIndex);
        //PlayerPrefs.SetInt("ArmourIndex", armourIndex);

        //PlayerPrefs.SetString("CharacterName", characterName);

        //for (int i = 0; i < characterStats.Length; i++)
        //{
        //    PlayerPrefs.SetInt(characterStats[i].baseStatsName, characterStats[i].baseStats + 
        //        characterStats[i].tempStats);
        //}

        //PlayerPrefs.SetString("CharacterClass", selectedClass[selectedClassIndex]);

    }

    private void RefreshText()
    {
        strText.text = "Strength: " + (characterStats[0].baseStats + characterStats[0].tempStats).ToString();
        dexText.text = "Dexterity: " + (characterStats[1].baseStats + characterStats[1].tempStats).ToString();
        conText.text = "Constitution: " + (characterStats[2].baseStats + characterStats[2].tempStats).ToString();
        wisText.text = "Wisdom: " + (characterStats[3].baseStats + characterStats[3].tempStats).ToString();
        intText.text = "Intelligence: " + (characterStats[4].baseStats + characterStats[4].tempStats).ToString();
        chrText.text = "Charisma: " + (characterStats[5].baseStats+ characterStats[5].tempStats).ToString();
    }

    public void SetName(string name)
    {
        characterName = name;
    }
    private void OnGUI()
    {
        #region GUI value setup
        // 16:9 resolution
        Vector2 scr = new Vector2(Screen.width / 16, Screen.height / 9);
        // start position
        float left = 0.25f * scr.x;
        float mid = 0.75f * scr.x;
        float right = 2.25f * scr.x;
        //sizes
        float size = 0.5f * scr.x;
        float x = 0.5f * scr.x;
        float y = 0.5f * scr.y;
        float label = 1.5f * scr.x;
        #endregion
        #region Customisation Textures
        /*
        for (int i = 0; i < matName.Length; i++)
        {
            if (GUI.Button(new Rect(left, y + i * y, x, y), "<"))
            {
                SetTexture(matName[i], -1);
            }
            GUI.Box(new Rect(mid, y + i * y, label, y), matName[i]);

            if (GUI.Button(new Rect(right, y + i * y, x, y), ">"))
            {
                SetTexture(matName[i], 1);
            }
        }
        */
        #endregion
        #region Choose Class
        /*
        float classX = 12.75f * scr.x;
        float h = 0;
        if (GUI.Button(new Rect(classX, y + h * y, 4 * x, y),classButton)) 
        {
            showDropdown = !showDropdown;
        }

        h++;
        
        if (showDropdown)
        {
            scrollPos = GUI.BeginScrollView(
                new Rect(classX, y + h * y, 4 * x, 4 * y), 
                scrollPos,
                new Rect(0, 0, 0, selectedClass.Length * y), 
                false, 
                true);

            for (int i = 0; i < selectedClass.Length; i++)
            {
                if(GUI.Button(new Rect(0, i*y, 3*x, y), selectedClass[i]))
                {
                    ChooseClass(i);
                    classButton = selectedClass[i];
                    showDropdown = false;
                }
            }

            GUI.EndScrollView();
        }
        */ 
        #endregion
        #region Set Stats
        /*
        GUI.Box(new Rect(classX, 5*y, 4*x, y), "Points: " + statPoints);
        for(int i = 0; i < characterStats.Length; i++)
        {
            if(statPoints > 0)
            {
                //+
                if(GUI.Button(new Rect(classX + 4*x, 7 * y + i * y, x, y),"+"))
                {
                    statPoints--;
                    characterStats[i].tempStats++;
                }
            }
            GUI.Box(new Rect(classX, 7*y+i*y,4*x,y), characterStats[i].baseStatsName + " : "
                + (characterStats[i].baseStats + characterStats[i].tempStats));
            if(statPoints < 10 && characterStats[i].tempStats > 0)
            {
                //-
                if (GUI.Button(new Rect(classX - x, 7 * y + i * y, x, y), "-"))
                {
                    statPoints++;
                    characterStats[i].tempStats--;
                }
            }
        }
        */
        #endregion
        /*
        characterName = GUI.TextField(new Rect(left, 7 * y, 5 * x, y), characterName, 32);
        if(GUI.Button(new Rect(left, 8 * y, 5 * x, y), "Save and Play"))
        {
            SaveCharacter();
            SceneManager.LoadScene(2);
            PlayerPrefs.SetString("LoadTo", "PlayGame");
            PlayerPrefs.SetString("LoadFrom", "CustomisationScene");
        }
        */
    }
}

public enum CharacterTutorialClass
{
    Barbarian,
    Bard,
    Druid
}
