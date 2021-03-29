using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//Script which manages various attributes of player stats, such as changing Spell/Health Points

public struct CharacterStats {//Struct that contains all the Stats for a character
    public string Name;//Character's Name
    //Set of base Stats - both which have a base value, and a change-able value (cannot be decreased below base value)
    public int Strength;
    private int _baseSTR;
    public int BaseStrength {
        get {
            return _baseSTR;
        }
    }
    public int Dexterity;
    private int _baseDEX;
    public int BaseDexterity {
        get {
            return _baseDEX;
        }
    }
    public int Toughness;
    private int _baseTOU;
    public int BaseToughness {
        get {
            return _baseTOU;
        }
    }
    public int Intelligence;
    private int _baseINT;
    public int BaseIntelligence {
        get {
            return _baseINT;
        }
    }
    public int Power;
    private int _basePOW;
    public int BasePower {
        get {
            return _basePOW;
        }
    }
    public int Charm;
    private int _baseCHA;
    public int BaseCharm {
        get {
            return _baseCHA;
        }
    }
    //Stat points are used to increase stat values
    public int StatPoints;
    //Advanced Stats
    public int HP;//Health
    public int SP;//Spell points
    public int Level;//Character's level

    //Inventory
    public List<Item> Inventory;
    
    //A special declaration, where you declare all their base stats
    public CharacterStats(string NAME, int STR, int DEX, int TOU, int INT, int POW, int CHA, int lev) {
        this.Name = NAME;
        this._baseSTR = STR;
        this.Strength = STR;
        this._baseDEX = DEX;
        this.Dexterity = DEX;
        this._baseTOU = TOU;
        this.Toughness = TOU;
        this._baseINT = INT;
        this.Intelligence = INT;
        this._basePOW = POW;
        this.Power = POW;
        this._baseCHA = CHA;
        this.Charm = CHA;
        StatPoints = 3 + lev * 2;
        //These need to be set as a function
        this.HP = 0;
        this.SP = 0;
        this.Level = lev;
        this.Inventory = new List<Item>();
    }
}

public class StatManager : MonoBehaviour
{
    public CharacterStats currentCharacter;
    [SerializeField]
    private ItemManager myItemManager;

    private string baseItemDescriptionText;//What the item description text typically says
    void Start()
    {
        baseItemDescriptionText = ItemDescriptionText.text;
        CreateNewCharacter(1);
        myItemManager.SortItems(ref currentCharacter.Inventory, OrderByType.Alphabet);
        RefreshStatPage();
    }

    
    void Update()
    {
        
    }


    //For both increasing and decreasing statistics, it calls an Integer for the statistic that will be changed
        //0 = Strength, 1 = Dexterity, 2 = Toughness, 3 = Intelligence, 4= Power, 5 = Charm
    public void IncreaseStat(int statToChange) {
        if(currentCharacter.StatPoints > 0) {
            currentCharacter.StatPoints--;
            if(statToChange == 0) {
                currentCharacter.Strength++;
            }
            else if(statToChange == 1) {
                currentCharacter.Dexterity++;
            }
            else if(statToChange == 2) {
                currentCharacter.Toughness++;
            }
            else if(statToChange == 3) {
                currentCharacter.Intelligence++;
            }
            else if(statToChange == 4) {
                currentCharacter.Power++;
            }
            else if(statToChange == 5) {
                currentCharacter.Charm++;
            }
        }
        RefreshStatPage();
    }
    public void DecreaseStat(int statToChange) {
        if(statToChange == 0) {
            if(currentCharacter.Strength > currentCharacter.BaseStrength) {
                currentCharacter.Strength--;
                currentCharacter.StatPoints++;
            }
        }
        else if(statToChange == 1) {
            if(currentCharacter.Dexterity > currentCharacter.BaseDexterity) {
                currentCharacter.Dexterity--;
                currentCharacter.StatPoints++;
            }
        }
        else if(statToChange == 2) {
            if(currentCharacter.Toughness > currentCharacter.BaseToughness) {
                currentCharacter.Toughness--;
                currentCharacter.StatPoints++;
            }
        }
        else if(statToChange == 3) {
            if(currentCharacter.Intelligence > currentCharacter.BaseIntelligence) {
                currentCharacter.Intelligence--;
                currentCharacter.StatPoints++;
            }
        }
        else if(statToChange == 4) {
            if(currentCharacter.Power > currentCharacter.BasePower) {
                currentCharacter.Power--;
                currentCharacter.StatPoints++;
            }
        }
        else if(statToChange == 5) {
            if(currentCharacter.Charm > currentCharacter.BaseCharm) {
                currentCharacter.Charm--;
                currentCharacter.StatPoints++;
            }
        }
        RefreshStatPage();
    }

    //All these variables are objects that need to be called when the Page is refreshed, such as numbers being changed, and buttons deactivating
    [SerializeField]
    private TMP_InputField nameText;
    [SerializeField]
    private TextMeshProUGUI statPointsText;
    [SerializeField]
    private TextMeshProUGUI strText;
        [SerializeField]
        private Button strUpButton;
        [SerializeField]
        private Button strDownButton;
    [SerializeField]
    private TextMeshProUGUI dexText;
        [SerializeField]
        private Button dexUpButton;
        [SerializeField]
        private Button dexDownButton;
    [SerializeField]
    private TextMeshProUGUI touText;
        [SerializeField]
        private Button touUpButton;
        [SerializeField]
        private Button touDownButton;
    [SerializeField]
    private TextMeshProUGUI intText;
        [SerializeField]
        private Button intUpButton;
        [SerializeField]
        private Button intDownButton;
    [SerializeField]
    private TextMeshProUGUI powText;
        [SerializeField]
        private Button powUpButton;
        [SerializeField]
        private Button powDownButton;
    [SerializeField]
    private TextMeshProUGUI chaText;
        [SerializeField]
        private Button chaUpButton;
        [SerializeField]
        private Button chaDownButton;
    //Advanced stats also have these
    [SerializeField]
    private TextMeshProUGUI hpText;
    [SerializeField]
    private TextMeshProUGUI spText;
    [SerializeField]
    private TextMeshProUGUI levelText;
    [SerializeField]
    private Image[] InventoryImage;
    //This function refreshes everything on the character's page, fixing anything that needs to be changed
    public void RefreshStatPage() {
        if(currentCharacter.Name == "") {
            nameText.text = "Name Your Character";
        }
        else {
            nameText.text = currentCharacter.Name;
        }
        statPointsText.text = currentCharacter.StatPoints.ToString();
        //If you have no stat points, all UP buttons become non-interactable
        if(currentCharacter.StatPoints == 0) {
            strUpButton.interactable = false;
            dexUpButton.interactable = false;
            touUpButton.interactable = false;
            intUpButton.interactable = false;
            powUpButton.interactable = false;
            chaUpButton.interactable = false;
        }
        else {
            strUpButton.interactable = true;
            dexUpButton.interactable = true;
            touUpButton.interactable = true;
            intUpButton.interactable = true;
            powUpButton.interactable = true;
            chaUpButton.interactable = true;
        }
        strText.text = currentCharacter.Strength.ToString();
        //Decreases their value might become non-interactable if its already at its base
        if(currentCharacter.Strength == currentCharacter.BaseStrength) {
            strDownButton.interactable = false;
        }
        else {
            strDownButton.interactable = true;
        }
        dexText.text = currentCharacter.Dexterity.ToString();
        if(currentCharacter.Dexterity == currentCharacter.BaseDexterity) {
            dexDownButton.interactable = false;
        }
        else {
            dexDownButton.interactable = true;
        }
        touText.text = currentCharacter.Toughness.ToString();
        if(currentCharacter.Toughness == currentCharacter.BaseToughness) {
            touDownButton.interactable = false;
        }
        else {
            touDownButton.interactable = true;
        }
        intText.text = currentCharacter.Intelligence.ToString();
        if(currentCharacter.Intelligence == currentCharacter.BaseIntelligence) {
            intDownButton.interactable = false;
        }
        else {
            intDownButton.interactable = true;
        }
        powText.text = currentCharacter.Power.ToString();
        if(currentCharacter.Power == currentCharacter.BasePower) {
            powDownButton.interactable = false;
        }
        else {
            powDownButton.interactable = true;
        }
        chaText.text = currentCharacter.Charm.ToString();
        if(currentCharacter.Charm == currentCharacter.BaseCharm) {
            chaDownButton.interactable = false;
        }
        else {
            chaDownButton.interactable = true;
        }
        CalculateHPandSP();
        hpText.text = currentCharacter.HP.ToString();
        spText.text = currentCharacter.SP.ToString();
        levelText.text = currentCharacter.Level.ToString();
        //Places all Items in the Inventory
        for(int i = 0; i < InventoryImage.Length; i++){
            if(i < currentCharacter.Inventory.Count) {
                //each inventory spot either becomes visible or invisible depending if it has a sprite
                InventoryImage[i].sprite = currentCharacter.Inventory[i].Image;
                InventoryImage[i].enabled = true;
            }
            else {
                InventoryImage[i].enabled = false;
            }
        }

    }
    public void ChangeName() {
        currentCharacter.Name = nameText.text;
        RefreshStatPage();
    }
    private const int BASE_HP = 5;
    private const int BASE_SP = 0;
    private const int HP_TOU_BASE = 10;
    private const float HP_MULTIPLIER= 1.5f;
    private const int HP_PER_LEVEL = 4;
    private const int SP_INT_BASE = 12;
    private const int SP_POW_BASE = 10;
    private const int SP_CHA_BASE = 15;
    private const float SP_MULTIPLIER = 2f;
    private const int SP_PER_LEVEL = 5;
    //Calculates the current Character's HP and SP with regards to their current stats
    public void CalculateHPandSP() {
        currentCharacter.HP = BASE_HP + Mathf.FloorToInt(HP_MULTIPLIER * Mathf.Max((currentCharacter.Toughness - HP_TOU_BASE), 0)) + currentCharacter.Level * HP_PER_LEVEL;
        currentCharacter.SP = BASE_SP + Mathf.FloorToInt(SP_MULTIPLIER * (Mathf.Max((currentCharacter.Intelligence - SP_INT_BASE), 0) + Mathf.Max((currentCharacter.Power - SP_POW_BASE), 0) + Mathf.Max((currentCharacter.Charm - SP_CHA_BASE), 0))) + currentCharacter.Level * SP_PER_LEVEL;
    }

    [SerializeField]
    private TMP_Dropdown levelDropDown;
    private const int BASE_STAT_AMT = 10;
    private const int AUTO_STAT_DISTRIBUTION = 24;
    public void CreateNewCharacter(int level = 0) {
        if(level == 0) {
            //When you create a new character, it uses the level-setter dropdown menu provided
            level = levelDropDown.value +1;
        }
        int[] tempStat = {BASE_STAT_AMT, BASE_STAT_AMT, BASE_STAT_AMT, BASE_STAT_AMT, BASE_STAT_AMT, BASE_STAT_AMT};
        for(int i = 0; i < 3; i++) {
            for(int j = 0; j < AUTO_STAT_DISTRIBUTION / 3; j++) {
                tempStat[Random.Range(0, 2) + i * 2]++;
            }
        }
        currentCharacter = new CharacterStats("", tempStat[0], tempStat[1], tempStat[2], tempStat[3], tempStat[4], tempStat[5], level);
        myItemManager.CreateItems(level + 3);
        RefreshStatPage();

        //Item description is reset when you create a new character
        ItemDescriptionText.text = baseItemDescriptionText;
    }

    [SerializeField]
    private TMP_Dropdown InventoryDropdown;
    //This sort is called by the inventory dropdown, which it then inputs into the ItemManager Sort code
    public void SortInventoryOverride() {
        myItemManager.SortItems(ref currentCharacter.Inventory, (OrderByType)InventoryDropdown.value);
        RefreshStatPage();
    }

    [SerializeField]
    private TextMeshProUGUI ItemDescriptionText;
    //Gives the description text for whatever you click on
    public void NewItemDescription(int itemNum) {
        if(itemNum < currentCharacter.Inventory.Count) {
            Item tempItem = currentCharacter.Inventory[itemNum];
            string tempText = tempItem.Name + "\n" +
                            "Level " + tempItem.Level + "\n" +
                            "Weight: " + tempItem.Weight + " lbs \n" +
                            "Cost: " + tempItem.Cost + " gold";
            ItemDescriptionText.text = tempText;
        }
    }
}
