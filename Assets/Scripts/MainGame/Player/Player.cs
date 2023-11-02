using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public enum PlayerStats
{
    ALL,
    HAPPINESS,
    HUNGER,
    ENERGY,
    MONEY
}

public enum ItemCondition
{
    NA,
    HEAVILYUSED,
    WELLUSED,
    BRANDNEW
}

public enum VehicleType
{
    NA,
    SCOOTER,
    SEDAN,
    SUV,
    COUPE,
    PICKUP
}

public enum VehicleColor
{
    NA,
    BLUE,
    RED,
    BLACK,
    CREAM,
    YELLOW,
    GREY,
    BROWN
}

public enum ItemType
{
    VEHICLE,
    APPLIANCE,
    CONSUMABLE
}

public enum Gender
{
    MALE,
    FEMALE
}


public class Player : MonoBehaviour
{
    private IDictionary<PlayerStats, float> playerStatsDict = new Dictionary<PlayerStats, float>();
    public List<int> itemsBought = new List<int>(); //to be refactored
    private string playerName;
    private string courseEnrolled;
    private float playerCash;
    private float playerBankSavings;
    private bool isPlayerHasBankAcc;
    private Gender playerGender;
    private ResBuilding currentPlayerPlace;
    private List<Items> playerOwnedVehicles = new List<Items>();
    private List<Items> playerOwnedAppliances = new List<Items>();
    private List<Items> playerOwnedGroceries = new List<Items>();

    public string PlayerName { set{playerName = value;} get{return playerName;}}
    public float PlayerCash { set{playerCash = value;} get{return playerCash;}}
    public string PlayerEnrolledCourse { set{courseEnrolled = value;} get{return courseEnrolled;}}
    public float PlayerEnrolledCourseDuration { set; get;}
    public float PlayerStudyHours { set; get;}
    public float PlayerBankSavings { set{playerBankSavings = value;} get{return playerBankSavings;}}
    public bool IsPlayerHasBankAcc { set{isPlayerHasBankAcc = value;} get{return isPlayerHasBankAcc;}}
    public Gender PlayerGender { set{playerGender = value;} get{return playerGender;}}
    public ResBuilding CurrentPlayerPlace { set{currentPlayerPlace = value;} get{return currentPlayerPlace;}}
    public IDictionary<PlayerStats, float> PlayerStatsDict {set{playerStatsDict = value;} get{return playerStatsDict;}}
    public List<Items> PlayerOwnedVehicles { get{return playerOwnedVehicles;}}
    public List<Items> PlayerOwnedAppliances { get{return playerOwnedAppliances;}}
    public List<Items> PlayerOwnedGroceries { get{return playerOwnedGroceries;}}
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
        PlayerCash = 5000f;
        PlayerBankSavings = 20000f;
        playerStatsDict.Add(PlayerStats.HAPPINESS, 100f);
        playerStatsDict.Add(PlayerStats.HUNGER, 100f);
        playerStatsDict.Add(PlayerStats.ENERGY, 100f);
        playerStatsDict.Add(PlayerStats.MONEY, PlayerCash);
        currentPlayerPlace = null;
        isPlayerHasBankAcc = false;
        PlayerStatsObserver.onPlayerStatChanged(PlayerStats.ALL, playerStatsDict);
    }


    public void EatDrink(float happinessAddValue, float energyLevelAddValue, float hungerLevelCutValue, float amount, float eatingTimeValue)
    {
        TimeManager.Instance.AddClockTime(eatingTimeValue);
        playerStatsDict[PlayerStats.HAPPINESS] += happinessAddValue;
        playerStatsDict[PlayerStats.ENERGY] += energyLevelAddValue;
        playerStatsDict[PlayerStats.HUNGER] += hungerLevelCutValue;
        playerStatsDict[PlayerStats.MONEY] -= amount;

        PlayerStatsObserver.onPlayerStatChanged(PlayerStats.ALL, playerStatsDict);
        //Debug.Log("HAPPY:" + playerStatsDict[PlayerStats.HAPPINESS]);
        StatsChecker();
        //Debug.Log("HAPPY AFTER:" + playerStatsDict[PlayerStats.HAPPINESS]);
    }


    public void Work(float salary)
    {
        playerStatsDict[PlayerStats.MONEY] += salary;
        PlayerStatsObserver.onPlayerStatChanged(PlayerStats.MONEY, playerStatsDict);
    }

    /*public float checkInputDuration(float duration)
    {
        float targetTime = duration + currentTime;
        if (targetTime > 17.0f)
        {
            float excessTime = targetTime - 17;
            duration -= excessTime;
        }

        return duration;
    }*/


    public void Study(float studyDurationValue)
    {
        //studyDurationValue = checkInputDuration(studyDurationValue);
        //AddClockTime(studyDurationValue);
        playerStatsDict[PlayerStats.ENERGY] -= studyDurationValue * 10;
        playerStatsDict[PlayerStats.HAPPINESS] -= studyDurationValue * 3;
        playerStatsDict[PlayerStats.HUNGER] -= studyDurationValue * 5;
        PlayerStatsObserver.onPlayerStatChanged(PlayerStats.ENERGY, playerStatsDict);
        PlayerStatsObserver.onPlayerStatChanged(PlayerStats.HAPPINESS, playerStatsDict);
        PlayerStatsObserver.onPlayerStatChanged(PlayerStats.HUNGER, playerStatsDict);

        StatsChecker();
        University.Instance.UpdateStudyHours(studyDurationValue);
    }


    public void UpdateStudyHours(float studyDurationValue)
    {
        PlayerStudyHours += studyDurationValue;
        //studyHours.text = PlayerStudyHours.ToString();
    }


    public void Enroll(string courseName, float courseDuration)
    {
        UpdateStudyHours(0);
    }


    public void Purchase(float energyLevelCutValue, Items item)
    {
        TimeManager.Instance.AddClockTime(0.30f);
        playerStatsDict[PlayerStats.ENERGY] -= energyLevelCutValue;
        playerStatsDict[PlayerStats.MONEY] -= item.itemPrice;
        PlayerStatsObserver.onPlayerStatChanged(PlayerStats.ALL, playerStatsDict);
        StatsChecker();
    }


    public void PurchaseMallItem(float energyLevelCutValue, float amount, float happinessAddValue)
    {
        //AddClockTime(0.30f);
        playerStatsDict[PlayerStats.HAPPINESS] += happinessAddValue;
        playerStatsDict[PlayerStats.ENERGY] -= energyLevelCutValue;
        playerStatsDict[PlayerStats.MONEY] -= amount;
        PlayerStatsObserver.onPlayerStatChanged(PlayerStats.ALL, playerStatsDict);
        StatsChecker();
    }


    public void Walk(float energyLevelCutValue)
    {
        TimeManager.Instance.AddClockTime(2);
        StatsChecker();
        playerStatsDict[PlayerStats.ENERGY] -= energyLevelCutValue;
        PlayerStatsObserver.onPlayerStatChanged(PlayerStats.ENERGY, playerStatsDict);
    }


    public void Ride(float energyLevelCutValue)
    {
        TimeManager.Instance.AddClockTime(0.5f);
        StatsChecker();
        playerStatsDict[PlayerStats.ENERGY] -= energyLevelCutValue;
        PlayerStatsObserver.onPlayerStatChanged(PlayerStats.ENERGY, playerStatsDict);
    }


    public void BuyGrocery(float amount)
    {
        playerStatsDict[PlayerStats.MONEY] -= amount;
        PlayerStatsObserver.onPlayerStatChanged(PlayerStats.MONEY, playerStatsDict);
    }


    public void StatsChecker()
    {
        if (playerStatsDict[PlayerStats.ENERGY] <= 10 | playerStatsDict[PlayerStats.HAPPINESS] <= 10 | playerStatsDict[PlayerStats.HUNGER] <= 10)//checks if any of the stats reaches lower limit
        {
            Debug.Log("LUH");
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
        if (playerStatsDict[PlayerStats.ENERGY] > 100)
        {
            playerStatsDict[PlayerStats.ENERGY] = 100;
        }
        if (playerStatsDict[PlayerStats.HAPPINESS] > 100)
        {
            playerStatsDict[PlayerStats.HAPPINESS] = 100;
        }
        if (playerStatsDict[PlayerStats.HUNGER] > 100)
        {
            playerStatsDict[PlayerStats.HUNGER] = 100;
        }
    }
}
