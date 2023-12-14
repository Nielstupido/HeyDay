using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GameStateData
{
    //Player Data
    public string playerName;
    public Gender playerGender;
    public float playerCashMoney = 5000f;
    public float playerBankSavingsMoney = 0f;
    public CharactersScriptableObj currentCharacter;
    public List<string> contactList = new List<string>();
    public Dictionary<PlayerStats, float> PlayerStatsDict {set{playerStatsDict = value;} get{return playerStatsDict;}}

    //Player Data Finance
    public float playerCash;
    public float playerBankSavings;
    public float playerTotalDebt;
    public float playerHospitalOutstandingDebt;
    public bool isPlayerHasBankAcc;
    public float playerLvlBillExpenses;
    public float playerLvlSavings;
    public float playerLvlConsumablesExpenses;
    public float playerLvlEmergencyFunds;
    
    //Player Data University
    public UniversityCourses goalCourse;
    public UniversityCourses courseEnrolled;
    public StudyFields studyFieldEnrolled;
    
    //Player Data Owned Things
    public List<Items> playerOwnedVehicles = new List<Items>();
    public List<Items> playerOwnedAppliances = new List<Items>();
    public int groceryBarValue;

    //Game World Data
    public float time = 7f;
    public float day = 1f;
    public int gameLevel = 1;


    //Budget values
    public float currentBillsBudget;
    public float currentSavingsBudget;
    public float currentConsumablesBudget;
    public float currentEmergencyBudget;

    //LeaderBoards
    public Text playerRank;
    public Text playerNameText;
    public Text playerScoreText;

    //LevelManager Data
    public MissionsHolder missionsHolder;
    public Transform missionPrefabsHolder;
    public GameObject missionPrefab;
    public GameObject missionOverlay;
    public GameObject nextLevelBtn;
    public TextMeshProUGUI missionOverlayLevelText;

    //Contact
    public PlayerPhone playerPhone;
    
    //ResBuilding
    public int stayCount;
    public float unpaidBill;
    public int daysUnpaid;
    
    //GAme Items
    public string itemName;
    public ItemType itemType;
    public Sprite itemImage;
    public float itemPrice;
    public ItemCondition itemCondition;
    public VehicleType vehicleType;
    public VehicleColor vehicleColor;
    public float energyBarValue;
    public float hungerBarValue;
    public float happinessBarValue;
    public float eatingTime;
    public float electricBillValue;

    //Job Positions
    public string jobPosName;
    public string jobPosReqs;
    public Buildings establishment;
    public JobFields workField;
    public StudyFields reqStudyField;
    public List<UniversityCourses> reqCourse;
    public JobFields reqWorkField;
    public float reqWorkHrs;
    public float salaryPerHr;

    //Missions
    public string id;
    public MissionStatus missionStatus;
    public MissionType missionType;
    public string missionDets;
    public float currentNumberForMission;
    public float requiredNumberForMission;
    public Buildings targetBuilding;
    public ItemType targetIitemType;
    public APPS targetApp;
    public PlayerStats targetPlayerStats;
}
