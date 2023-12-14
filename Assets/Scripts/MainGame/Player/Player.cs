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
    private Dictionary<PlayerStats, float> playerStatsDict = new Dictionary<PlayerStats, float>();
    private int playerAge;
    private string playerName;
    private Gender playerGender;
    private CharactersScriptableObj currentCharacter;
    private List<string> contactList = new List<string>();
    public Dictionary<PlayerStats, float> PlayerStatsDict {set{playerStatsDict = value;} get{return playerStatsDict;}}
    public string PlayerName { set{playerName = value;} get{return playerName;}}
    public Gender PlayerGender { set{playerGender = value;} get{return playerGender;}}
    public CharactersScriptableObj CurrentCharacter { set{currentCharacter = value;} get{return currentCharacter;}}
    public List<string> ContactList { set{contactList = value;} get{return contactList;}}

    //Finance
    [SerializeField] private Prompts notEnoughMoneyPrompt;
    private float playerCash;
    private float playerBankSavings;
    private float playerTotalDebt;
    private float playerHospitalOutstandingDebt;
    private bool isPlayerHasBankAcc;
    public float PlayerCash { set{playerCash = value;} get{return playerCash;}}
    public float PlayerBankSavings { set{playerBankSavings = value;} get{return playerBankSavings;}}
    public float PlayerHospitalOutstandingDebt { set{playerHospitalOutstandingDebt = value;} get{return playerHospitalOutstandingDebt;}}
    public float PlayerTotalDebt { set{playerTotalDebt = value;} get{return playerTotalDebt;}}
    public bool IsPlayerHasBankAcc { set{isPlayerHasBankAcc = value;} get{return isPlayerHasBankAcc;}}

    //Budget tracking
    private float playerLvlBillExpenses;
    private float playerLvlSavings;
    private float playerLvlConsumablesExpenses;
    private float playerLvlEmergencyFunds;
    public float PlayerLvlBillExpenses { set{playerLvlBillExpenses = value;} get{return playerLvlBillExpenses;}}
    public float PlayerLvlSavings { set{playerLvlSavings = value;} get{return playerLvlSavings;}}
    public float PlayerLvlConsumablesExpenses { set{playerLvlConsumablesExpenses = value;} get{return playerLvlConsumablesExpenses;}}
    public float PlayerLvlEmergencyFunds { set{playerLvlEmergencyFunds = value;} get{return playerLvlEmergencyFunds;}}

    //Studies
    private UniversityCourses goalCourse;
    private UniversityCourses courseEnrolled;
    private StudyFields studyFieldEnrolled;
    public UniversityCourses PlayerEnrolledCourse { set{courseEnrolled = value;} get{return courseEnrolled;}}
    public StudyFields PlayerEnrolledStudyField { set{studyFieldEnrolled = value;} get{return studyFieldEnrolled;}}
    public UniversityCourses GoalCourse { set{goalCourse = value;} get{return goalCourse;}}
    public float PlayerEnrolledCourseDuration { set; get;}
    public float PlayerStudyHours { set; get;}

    //Possessions
    private List<Items> playerOwnedVehicles = new List<Items>();
    private List<Items> playerOwnedAppliances = new List<Items>();
    private int groceryBarValue;
    public List<Items> PlayerOwnedVehicles { get{return playerOwnedVehicles;}}
    public List<Items> PlayerOwnedAppliances { get{return playerOwnedAppliances;}}
    public int GroceryBarValue { set{groceryBarValue = value;} get{return groceryBarValue;}}

    //Work
    private JobPositions currentPlayerJob;
    private Dictionary<JobFields,float> playerWorkFieldHistory = new Dictionary<JobFields, float>();
    private float currentWorkHours;
    public JobPositions CurrentPlayerJob { set{currentPlayerJob = value;} get{return currentPlayerJob;}}
    public Dictionary<JobFields, float> PlayerWorkFieldHistory {set{playerWorkFieldHistory = value;} get{return playerWorkFieldHistory;}}
    public float CurrentWorkHours { set{currentWorkHours = value;} get{return currentWorkHours;}}

    //Consts
    private const float StudyEnergyCutValue = 10;
    private const float StudyHappinessCutValue = 3;
    private const float StudyHungerCutValue = 5;
    private const float WorkEnergyCutValue = 10;
    private const float WorkHappinessCutValue = 10;
    private const float WorkHungerCutValue = 10;

    //Misc.
    [SerializeField] private PlayerPhone playerPhone;
    private ResBuilding currentPlayerPlace;
    public ResBuilding CurrentPlayerPlace { set{currentPlayerPlace = value;} get{return currentPlayerPlace;}}
    public static Player Instance { get; private set; }


    private void Awake() 
    { 
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    private void Start()
    {
        playerAge = 19;
        currentPlayerJob = null;
        courseEnrolled = UniversityCourses.NONE;
        playerCash = 5000f;
        playerBankSavings = 0f;
        groceryBarValue = 0;
        playerStatsDict.Add(PlayerStats.HAPPINESS, 100f);
        playerStatsDict.Add(PlayerStats.HUNGER, 100f);
        playerStatsDict.Add(PlayerStats.ENERGY, 100f);
        playerStatsDict.Add(PlayerStats.MONEY, PlayerCash);
        currentPlayerPlace = null;
        isPlayerHasBankAcc = false;
        PlayerStatsObserver.onPlayerStatChanged += StatsChecker;
        PlayerStatsObserver.onPlayerStatChanged(PlayerStats.ALL, playerStatsDict);
    }


    private void OnDestroy()
    {
        PlayerStatsObserver.onPlayerStatChanged -= StatsChecker;
    }

    private void OnEnable()
    {
        PlayerStatsObserver.onPlayerStatChanged += StatsChecker;
    }
    private void OnDisable()
    {
        PlayerStatsObserver.onPlayerStatChanged -= StatsChecker;
    }

    public void EatDrink(Items foodToConsume)
    {
        StartCoroutine(DoAnim(ActionAnimations.EAT, 2f));
        TimeManager.Instance.AddClockTime(foodToConsume.eatingTime);
        playerStatsDict[PlayerStats.HAPPINESS] += foodToConsume.happinessBarValue;
        playerStatsDict[PlayerStats.ENERGY] += foodToConsume.energyBarValue;
        playerStatsDict[PlayerStats.HUNGER] += foodToConsume.hungerBarValue;

        PlayerStatsObserver.onPlayerStatChanged(PlayerStats.ALL, playerStatsDict);
    }


    public void ConsumeGrocery()
    {
        playerPhone.ConsumeGrocery();
    }


    public void Work(bool isGettingSalary, float workHrsDone, float totalWorkHrs)
    {
        currentWorkHours += workHrsDone;
        
        if (isGettingSalary)
        {
            playerCash += totalWorkHrs * currentPlayerJob.salaryPerHr;
            playerStatsDict[PlayerStats.MONEY] = playerCash;
        }

        playerStatsDict[PlayerStats.ENERGY] -= workHrsDone * WorkEnergyCutValue;
        playerStatsDict[PlayerStats.HAPPINESS] -= workHrsDone * WorkHappinessCutValue;
        playerStatsDict[PlayerStats.HUNGER] -= workHrsDone * WorkHungerCutValue;
        PlayerStatsObserver.onPlayerStatChanged(PlayerStats.ALL, playerStatsDict);
        //save game
    }


    public void Study(int studyDurationValue)
    {
        TimeManager.Instance.AddClockTime(studyDurationValue);
        playerStatsDict[PlayerStats.ENERGY] -= studyDurationValue * StudyEnergyCutValue;
        playerStatsDict[PlayerStats.HAPPINESS] -= studyDurationValue * StudyHappinessCutValue;
        playerStatsDict[PlayerStats.HUNGER] -= studyDurationValue * StudyHungerCutValue;
        PlayerStatsObserver.onPlayerStatChanged(PlayerStats.ALL, playerStatsDict);

        UniversityManager.Instance.UpdateStudyHours(studyDurationValue);
        LevelManager.onFinishedPlayerAction(MissionType.STUDYHR, studyDurationValue);
    }


    public void Purchase(bool toConsume, Items item, float timeAdded, float energyLevelCutValue = 10f)
    {
        if (item.itemPrice > playerCash)
        {
            PromptManager.Instance.ShowPrompt(notEnoughMoneyPrompt);
            return;
        }

        StartCoroutine(DoAnim(ActionAnimations.BUY, 2f));

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
        playerCash -= (item.itemPrice + ((GameManager.Instance.InflationRate / 100) * item.itemPrice));
        playerLvlConsumablesExpenses += (item.itemPrice + ((GameManager.Instance.InflationRate / 100) * item.itemPrice));
        playerStatsDict[PlayerStats.MONEY] -= (item.itemPrice + ((GameManager.Instance.InflationRate / 100) * item.itemPrice));
        PlayerStatsObserver.onPlayerStatChanged(PlayerStats.ALL, playerStatsDict);

        if (toConsume)
        {
            EatDrink(item);
            LevelManager.onFinishedPlayerAction(MissionType.EAT, interactedBuilding:BuildingManager.Instance.CurrentSelectedBuilding.buildingEnumName);
            LevelManager.onFinishedPlayerAction(MissionType.BUY, interactedItemType:ItemType.CONSUMABLE);
        }
    }


    //for non-consumable
    public bool Pay(bool isBill, float price, float timeAdded, float happinessAdded, float hungerLevelCutValue, Prompts errorMoneyPrompt, float energyLevelCutValue = 10f)
    {
        if (price > playerCash)
        {
            PromptManager.Instance.ShowPrompt(errorMoneyPrompt);
            return false;
        }

        if (isBill)
        {
            playerLvlBillExpenses += price;
        }
        else
        {
            playerLvlConsumablesExpenses += price;
        }

        TimeManager.Instance.AddClockTime(timeAdded);
        playerCash -= price;
        playerStatsDict[PlayerStats.ENERGY] -= energyLevelCutValue;
        playerStatsDict[PlayerStats.HAPPINESS] += happinessAdded;
        playerStatsDict[PlayerStats.HUNGER] -= hungerLevelCutValue;
        playerStatsDict[PlayerStats.MONEY] -= price;
        PlayerStatsObserver.onPlayerStatChanged(PlayerStats.ALL, playerStatsDict);
        return true;
    }


    public void Walk(float energyLevelCutValue, float hungerLevelCutValue)
    {
        TimeManager.Instance.AddClockTime(1f);
        playerStatsDict[PlayerStats.ENERGY] -= energyLevelCutValue;
        playerStatsDict[PlayerStats.HUNGER] -= hungerLevelCutValue;
        PlayerStatsObserver.onPlayerStatChanged(PlayerStats.ALL, playerStatsDict);
    }


    // public void Ride(float energyLevelCutValue, float hungerLevelCutValue)
    // {
    //     playerStatsDict[PlayerStats.ENERGY] -= energyLevelCutValue;
    //     playerStatsDict[PlayerStats.HUNGER] -= hungerLevelCutValue;
    //     PlayerStatsObserver.onPlayerStatChanged(PlayerStats.ALL, playerStatsDict);
    // }


    public float GetLvlTotalExpenses()
    {
        return playerLvlBillExpenses + playerLvlSavings + playerLvlConsumablesExpenses + playerLvlEmergencyFunds;
    }


    public void ResetLvlExpenses()
    {
        playerLvlBillExpenses = 0f;
        playerLvlSavings = 0f;
        playerLvlConsumablesExpenses = 0f;
        playerLvlEmergencyFunds = 0f;
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


    public void IncrementAge()
    {
        playerAge++;
    }


    private void StatsChecker(PlayerStats statName, Dictionary<PlayerStats, float> playerStatsDict)
    {
        if (playerStatsDict[PlayerStats.ENERGY] <= 10 || playerStatsDict[PlayerStats.HAPPINESS] <= 10 || playerStatsDict[PlayerStats.HUNGER] <= 10)//checks if any of the stats reaches lower limit
        {
            float sumOfStats = playerStatsDict[PlayerStats.ENERGY] + playerStatsDict[PlayerStats.HAPPINESS] + playerStatsDict[PlayerStats.HUNGER];

            if ((sumOfStats * 100) / 300 <= 10)
            {
                HospitalManager.Instance.Hospitalized(5);
            }
            else if ((sumOfStats*100) / 300 <= 30)
            {
                HospitalManager.Instance.Hospitalized(4);
            }
            else if ((sumOfStats*100) / 300 <= 50)
            {
                HospitalManager.Instance.Hospitalized(3);
            }
            else if ((sumOfStats*100) / 300 <= 70)
            {
                HospitalManager.Instance.Hospitalized(2);
            }
            else
            {
                HospitalManager.Instance.Hospitalized(1);
            }
        }

        //Checks if stats reaches the upper limit
        if (playerStatsDict[PlayerStats.ENERGY] >= 100)
        {
            playerStatsDict[PlayerStats.ENERGY] = 100;

            try
            {
                LevelManager.onFinishedPlayerAction(MissionType.MAXSTATS, interactedPlayerStats:PlayerStats.ENERGY);
            }
            catch (System.Exception e)
            {
                Debug.Log((e));
            }
        }
        if (playerStatsDict[PlayerStats.HAPPINESS] >= 100)
        {
            playerStatsDict[PlayerStats.HAPPINESS] = 100;

            try
            {
                LevelManager.onFinishedPlayerAction(MissionType.MAXSTATS, interactedPlayerStats:PlayerStats.HAPPINESS);
            }
            catch (System.Exception e)
            {
                Debug.Log((e));
            }
        }
        if (playerStatsDict[PlayerStats.HUNGER] >= 100)
        {
            playerStatsDict[PlayerStats.HUNGER] = 100;

            try
            {
                LevelManager.onFinishedPlayerAction(MissionType.MAXSTATS, interactedPlayerStats:PlayerStats.HUNGER);
            }
            catch (System.Exception e)
            {
                Debug.Log((e));
            }
        }
    }


    private IEnumerator DoAnim(ActionAnimations actionAnimation, float animLength)
    {
        AnimOverlayManager.Instance.StartAnim(actionAnimation);
        yield return new WaitForSeconds(animLength);
        AnimOverlayManager.Instance.StopAnim();
        yield return null;
    }
}
