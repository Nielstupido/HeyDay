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

public class Player : MonoBehaviour
{
    private IDictionary<PlayerStats, float> playerStatsDict = new Dictionary<PlayerStats, float>();
    private string playerName;
    private float hospitalFee = 1500; // hospital fee per day
    private float numOfdays = 0;
    private float currentTime = 11; //time will start at 7AM
    private int toggleCounter;
    private float currentDayCount = 1;// number of days that have passed
    
    [SerializeField] private GameObject hospitalizedPrompt;
    [SerializeField] private TextMeshProUGUI daysHospitalized;
    [SerializeField] private TextMeshProUGUI totalBill;
    [SerializeField] private TextMeshProUGUI clockValue;
    [SerializeField] private TextMeshProUGUI indicatorAMPM;
    [SerializeField] private TextMeshProUGUI dayValue;

    public string PlayerName { set{playerName = value;} get{return playerName;}}


    private void Start()
    {
        playerStatsDict.Add(PlayerStats.HAPPINESS, 100f);
        playerStatsDict.Add(PlayerStats.HUNGER, 100f);
        playerStatsDict.Add(PlayerStats.ENERGY, 100f);
        playerStatsDict.Add(PlayerStats.MONEY, 5000f);
    }


    public void EatDrink(float happinessAddValue, float energyLevelAddValue, float hungerLevelCutValue, float amount)
    {
        playerStatsDict[PlayerStats.HAPPINESS] += happinessAddValue;
        playerStatsDict[PlayerStats.ENERGY] += energyLevelAddValue;
        playerStatsDict[PlayerStats.HUNGER] -= hungerLevelCutValue;
        playerStatsDict[PlayerStats.MONEY] -= amount;

        PlayerStatsObserver.onPlayerStatChanged(PlayerStats.ALL, playerStatsDict);
    }


    public void Sleep(float energyLevelAddValue)
    {
        playerStatsDict[PlayerStats.ENERGY] += energyLevelAddValue;
        PlayerStatsObserver.onPlayerStatChanged(PlayerStats.ENERGY, playerStatsDict);
    }


    public void Work(float salary)
    {
        playerStatsDict[PlayerStats.MONEY] += salary;
        PlayerStatsObserver.onPlayerStatChanged(PlayerStats.MONEY, playerStatsDict);
    }


    public void Study()
    {

    }


    public void Walk(float energyLevelCutValue)
    {
        AddClockTime(2.3f);
        StatsChecker();
        playerStatsDict[PlayerStats.ENERGY] -= energyLevelCutValue;
        PlayerStatsObserver.onPlayerStatChanged(PlayerStats.ENERGY, playerStatsDict);
    }


    public void Ride(float energyLevelCutValue)
    {
        playerStatsDict[PlayerStats.ENERGY] -= energyLevelCutValue;
        PlayerStatsObserver.onPlayerStatChanged(PlayerStats.ENERGY, playerStatsDict);
    }

    public void StatsChecker()
    {
        if (playerStatsDict[PlayerStats.ENERGY] <= 10 | playerStatsDict[PlayerStats.HAPPINESS] <= 10 | playerStatsDict[PlayerStats.HUNGER] <= 10)//checks if any of the stats reaches lower limit
        {
            float sumOfStats = playerStatsDict[PlayerStats.ENERGY] + playerStatsDict[PlayerStats.HAPPINESS] + playerStatsDict[PlayerStats.HUNGER];

            if ((sumOfStats*100) / 300 <= 10)
            {
                Hospitalized(5);
            }
            else if ((sumOfStats*100) / 300 <= 30)
            {
                Hospitalized(4);
            }
            else if ((sumOfStats*100) / 300 <= 50)
            {
                Hospitalized(3);
            }
            else if ((sumOfStats*100) / 300 <= 70)
            {
                Hospitalized(2);
            }
            
        }
    }

    public void Hospitalized(float dayCount)
    {
        hospitalizedPrompt.SetActive(true);
        daysHospitalized.text = dayCount.ToString();
        float bill = hospitalFee*dayCount;
        totalBill.text = bill.ToString();

        numOfdays = dayCount;
    }
    
    public void PayHospitalFees()
    {
        playerStatsDict[PlayerStats.MONEY] -= numOfdays*hospitalFee;
        playerStatsDict[PlayerStats.HAPPINESS] = 100;
        playerStatsDict[PlayerStats.ENERGY] = 100;
        playerStatsDict[PlayerStats.HUNGER] = 100;

        PlayerStatsObserver.onPlayerStatChanged(PlayerStats.ALL, playerStatsDict);

        hospitalizedPrompt.SetActive(false);
    }

    public void AddClockTime(float addedClockValue)
    {
        currentTime += addedClockValue;
        int minutes = 0;
        int hours = 0;
        int transposedValue; 

        if (currentTime % 1 != 0)
        {
            hours = (int)currentTime;
            float temp = (currentTime % 1) * 100;
            minutes = (int)temp;

            if (minutes >= 60)
            {
                minutes = 0;
                hours ++;

                if (hours > 24)
                {
                    hours = 1;
                    toggleCounter++;
                }

                transposedValue = TransposeTimeValue((int)currentTime);
                clockValue.text = transposedValue.ToString() + ":00";
                currentTime = hours;
            }
            else if (hours > 24)
            {
                hours = 1;
                currentTime = hours + (minutes / 100);
                transposedValue = TransposeTimeValue((int)currentTime);
                clockValue.text = transposedValue.ToString() + ":" + minutes.ToString();
                toggleCounter++;
            }
            else
            {
                transposedValue = TransposeTimeValue((int)currentTime);
                clockValue.text = transposedValue.ToString() + ":" + minutes.ToString();
            }
        }
        else
        {
            hours = (int)currentTime;
            clockValue.text = hours.ToString() + ":00";
        }

        AmOrPm();
        IncrementDayCount();
    }

    public static int TransposeTimeValue(int currentTime)
    {
        switch (currentTime)
        {
            case 13: return 1;
            case 14: return 2;
            case 15: return 3;
            case 16: return 4;
            case 17: return 5;
            case 18: return 6;
            case 19: return 7;
            case 20: return 8;
            case 21: return 9;
            case 22: return 10;
            case 23: return 11;
            case 24: return 12;
            default: return (currentTime);
        }
    }

    public void AmOrPm()
    {
        if (currentTime >= 12)
        {
            indicatorAMPM.text = "PM";
        }
        else
        {
            indicatorAMPM.text = "AM";
        }
    }

    public void IncrementDayCount()
    {
        if (toggleCounter == 1)
        {
            currentDayCount++;
            Debug.Log("Current Day Count = " + currentDayCount);
            dayValue.text = currentDayCount.ToString();
            toggleCounter = 0;
        }
    }
}
