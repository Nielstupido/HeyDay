using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public enum PlayerStats
{
    NONE,
    ALL,
    HAPPINESS,
    HUNGER,
    ENERGY,
    MONEY
}

public enum Gender
{
    MALE,
    FEMALE
}


public class Player : MonoBehaviour
{
    //Player Basic Data
    private IDictionary<PlayerStats, float> playerStatsDict = new Dictionary<PlayerStats, float>();
    private string playerName;
    private Gender playerGender;
    public IDictionary<PlayerStats, float> PlayerStatsDict {set{playerStatsDict = value;} get{return playerStatsDict;}}
    public string PlayerName { set{playerName = value;} get{return playerName;}}
    public Gender PlayerGender { set{playerGender = value;} get{return playerGender;}}

    //Finance
    [SerializeField] private Prompts notEnoughMoneyPrompt;
    private float playerCash;
    private float playerBankSavings;
    private bool isPlayerHasBankAcc;
    public float PlayerCash { set{playerCash = value;} get{return playerCash;}}
    public float PlayerBankSavings { set{playerBankSavings = value;} get{return playerBankSavings;}}
    public bool IsPlayerHasBankAcc { set{isPlayerHasBankAcc = value;} get{return isPlayerHasBankAcc;}}

    //Studies
    private UniversityCourses courseEnrolled;
    private StudyFields studyFieldEnrolled;
    public UniversityCourses PlayerEnrolledCourse { set{courseEnrolled = value;} get{return courseEnrolled;}}
    public StudyFields PlayerEnrolledStudyField { set{studyFieldEnrolled = value;} get{return studyFieldEnrolled;}}
    public float PlayerEnrolledCourseDuration { set; get;}
    public float PlayerStudyHours { set; get;}

    //Possessions
    private List<Items> playerOwnedVehicles = new List<Items>();
    private List<Items> playerOwnedAppliances = new List<Items>();
    private List<Items> playerOwnedGroceries = new List<Items>();
    public List<Items> PlayerOwnedVehicles { get{return playerOwnedVehicles;}}
    public List<Items> PlayerOwnedAppliances { get{return playerOwnedAppliances;}}
    public List<Items> PlayerOwnedGroceries { get{return playerOwnedGroceries;}}

    //Work
    private JobPositions currentPlayerJob;
    private IDictionary<JobFields,float> playerWorkFieldHistory = new Dictionary<JobFields, float>();
    private float currentWorkHours;
    public JobPositions CurrentPlayerJob { set{currentPlayerJob = value;} get{return currentPlayerJob;}}
    public IDictionary<JobFields, float> PlayerWorkFieldHistory {set{playerWorkFieldHistory = value;} get{return playerWorkFieldHistory;}}
    public float CurrentWorkHours { set{currentWorkHours = value;} get{return currentWorkHours;}}

    //Consts
    private const float STUDY_ENERGY_CUT_VALUE = 10;
    private const float STUDY_HAPPINESS_CUT_VALUE = 3;
    private const float STUDY_HUNGER_CUT_VALUE = 5;
    private const float WORK_ENERGY_CUT_VALUE = 12;
    private const float WORK_HAPPINESS_CUT_VALUE = 3;
    private const float WORK_HUNGER_CUT_VALUE = 7;

    //Misc.
    private ResBuilding currentPlayerPlace;
    public ResBuilding CurrentPlayerPlace { set{currentPlayerPlace = value;} get{return currentPlayerPlace;}}

    public static Player Instance { get; private set; }


    private void Awake() 
    { 
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }


    private void Start()
    {
        currentPlayerJob = null;
        courseEnrolled = UniversityCourses.NONE;
        playerCash = 5000f;
        playerBankSavings = 0f;
        playerStatsDict.Add(PlayerStats.HAPPINESS, 100f);
        playerStatsDict.Add(PlayerStats.HUNGER, 100f);
        playerStatsDict.Add(PlayerStats.ENERGY, 100f);
        playerStatsDict.Add(PlayerStats.MONEY, PlayerCash);
        currentPlayerPlace = null;
        isPlayerHasBankAcc = false;
        PlayerStatsObserver.onPlayerStatChanged += StatsChecker;
        PlayerStatsObserver.onPlayerStatChanged(PlayerStats.ALL, playerStatsDict);
    }


    public void EatDrink(Items foodToConsume)
    {
        TimeManager.Instance.AddClockTime(foodToConsume.eatingTime);
        playerStatsDict[PlayerStats.HAPPINESS] += foodToConsume.happinessBarValue;
        playerStatsDict[PlayerStats.ENERGY] += foodToConsume.energyBarValue;
        playerStatsDict[PlayerStats.HUNGER] += foodToConsume.hungerBarValue;

        PlayerStatsObserver.onPlayerStatChanged(PlayerStats.ALL, playerStatsDict);
        playerOwnedGroceries.Remove(foodToConsume);
    }


    public void Work(bool isGettingSalary, float workHrsDone, float totalWorkHrs)
    {
        if (isGettingSalary)
        {
            playerCash += totalWorkHrs * currentPlayerJob.salaryPerHr;
            playerStatsDict[PlayerStats.MONEY] = playerCash;
        }

        playerStatsDict[PlayerStats.ENERGY] -= workHrsDone * WORK_ENERGY_CUT_VALUE;
        playerStatsDict[PlayerStats.HAPPINESS] -= workHrsDone * WORK_HAPPINESS_CUT_VALUE;
        playerStatsDict[PlayerStats.HUNGER] -= workHrsDone * WORK_HUNGER_CUT_VALUE;
        PlayerStatsObserver.onPlayerStatChanged(PlayerStats.ALL, playerStatsDict);
        //save game
    }


    public void Study(float studyDurationValue)
    {
        TimeManager.Instance.AddClockTime(studyDurationValue);
        playerStatsDict[PlayerStats.ENERGY] -= studyDurationValue * STUDY_ENERGY_CUT_VALUE;
        playerStatsDict[PlayerStats.HAPPINESS] -= studyDurationValue * STUDY_HAPPINESS_CUT_VALUE;
        playerStatsDict[PlayerStats.HUNGER] -= studyDurationValue * STUDY_HUNGER_CUT_VALUE;
        PlayerStatsObserver.onPlayerStatChanged(PlayerStats.ALL, playerStatsDict);

        UniversityManager.Instance.UpdateStudyHours(studyDurationValue);
    }


    public void Purchase(bool toConsume, Items item, float timeAdded, float energyLevelCutValue = 10f)
    {
        if (item.itemPrice > playerCash)
        {
            PromptManager.Instance.ShowPrompt(notEnoughMoneyPrompt);
            return;
        }

        switch (item.itemType)
        {
            case ItemType.VEHICLE:
                playerOwnedVehicles.Add(item);
                playerStatsDict[PlayerStats.ENERGY] -= energyLevelCutValue;
                playerStatsDict[PlayerStats.HAPPINESS] += item.happinessBarValue;
                break;
            case ItemType.APPLIANCE:
                playerOwnedAppliances.Add(item);
                playerStatsDict[PlayerStats.ENERGY] -= energyLevelCutValue;
                playerStatsDict[PlayerStats.HAPPINESS] += item.happinessBarValue;
                LevelManager.onFinishedPlayerAction(MissionType.BUY, interactedItemType:ItemType.APPLIANCE);
                break;
            case ItemType.CONSUMABLE:
                playerOwnedGroceries.Add(item);
                break;
            case ItemType.SERVICE:
                if (item.itemName == "Spa Service")
                {
                    LevelManager.onFinishedPlayerAction(MissionType.MASSAGE);
                }
                else if (item.itemName == "Hair Salon Service")
                {
                    LevelManager.onFinishedPlayerAction(MissionType.HAIRSERVICE);
                }
                break;
        }
        
        TimeManager.Instance.AddClockTime(timeAdded);
        playerCash -= item.itemPrice;
        playerStatsDict[PlayerStats.MONEY] -= item.itemPrice;
        PlayerStatsObserver.onPlayerStatChanged(PlayerStats.ALL, playerStatsDict);

        if (toConsume)
        {
            EatDrink(item);
            LevelManager.onFinishedPlayerAction(MissionType.EAT, interactedBuilding:BuildingManager.Instance.CurrentSelectedBuilding.buildingEnumName);
        }
    }


    public void Walk(float energyLevelCutValue)
    {
        TimeManager.Instance.AddClockTime(2);
        playerStatsDict[PlayerStats.ENERGY] -= energyLevelCutValue;
        PlayerStatsObserver.onPlayerStatChanged(PlayerStats.ENERGY, playerStatsDict);
    }


    public void Ride(float energyLevelCutValue)
    {
        TimeManager.Instance.AddClockTime(0.5f);
        playerStatsDict[PlayerStats.ENERGY] -= energyLevelCutValue;
        PlayerStatsObserver.onPlayerStatChanged(PlayerStats.ENERGY, playerStatsDict);
    }


    public void BuyGrocery(float amount)
    {
        playerStatsDict[PlayerStats.MONEY] -= amount;
        PlayerStatsObserver.onPlayerStatChanged(PlayerStats.MONEY, playerStatsDict);
    }


    public float GetTotalWorkHours()
    {
        float totalWorkHours = 0;

        if (playerWorkFieldHistory.Count > 0)
        {
            foreach (KeyValuePair<JobFields, float> workHistory in playerWorkFieldHistory)
            {
                if (workHistory.Key != currentPlayerJob.workField)
                {
                    totalWorkHours += workHistory.Value;
                }
            }
            
            totalWorkHours += currentWorkHours;

            return totalWorkHours;
        }
        else
        {
            return currentWorkHours;
        }
    }


    private void StatsChecker(PlayerStats statName, IDictionary<PlayerStats, float> playerStatsDict)
    {
        if (playerStatsDict[PlayerStats.ENERGY] <= 10 | playerStatsDict[PlayerStats.HAPPINESS] <= 10 | playerStatsDict[PlayerStats.HUNGER] <= 10)//checks if any of the stats reaches lower limit
        {
            float sumOfStats = playerStatsDict[PlayerStats.ENERGY] + playerStatsDict[PlayerStats.HAPPINESS] + playerStatsDict[PlayerStats.HUNGER];

            if ((sumOfStats*100) / 300 <= 10)
            {
                GameManager.Instance.Hospitalized(5);
            }
            else if ((sumOfStats*100) / 300 <= 30)
            {
                GameManager.Instance.Hospitalized(4);
            }
            else if ((sumOfStats*100) / 300 <= 50)
            {
                GameManager.Instance.Hospitalized(3);
            }
            else if ((sumOfStats*100) / 300 <= 70)
            {
                GameManager.Instance.Hospitalized(2);
            }
            else
            {
                GameManager.Instance.Hospitalized(1);
            }
        }

        //Checks if stats reaches the upper limit
        if (playerStatsDict[PlayerStats.ENERGY] >= 100)
        {
            playerStatsDict[PlayerStats.ENERGY] = 100;
            LevelManager.onFinishedPlayerAction(MissionType.MAXSTATS, interactedPlayerStats:PlayerStats.ENERGY);
        }
        if (playerStatsDict[PlayerStats.HAPPINESS] >= 100)
        {
            playerStatsDict[PlayerStats.HAPPINESS] = 100;
            LevelManager.onFinishedPlayerAction(MissionType.MAXSTATS, interactedPlayerStats:PlayerStats.HAPPINESS);
        }
        if (playerStatsDict[PlayerStats.HUNGER] >= 100)
        {
            playerStatsDict[PlayerStats.HUNGER] = 100;
            LevelManager.onFinishedPlayerAction(MissionType.MAXSTATS, interactedPlayerStats:PlayerStats.HUNGER);
        }
    }
}
