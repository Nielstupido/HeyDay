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
    private float currentTime = 7; //time will start at 7AM
    
    [SerializeField] private GameObject hospitalizedPrompt;
    [SerializeField] private TextMeshProUGUI daysHospitalized;
    [SerializeField] private TextMeshProUGUI totalBill;
    [SerializeField] private TextMeshProUGUI clockValue;
    [SerializeField] private TextMeshProUGUI indicatorAMPM;


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
        AddClockTime(.3f);
        StatsChecker(playerStatsDict[PlayerStats.ENERGY], playerStatsDict[PlayerStats.HAPPINESS], playerStatsDict[PlayerStats.HUNGER]);
        /*Debug.Log("Energy:" + playerStatsDict[PlayerStats.ENERGY]);
        Debug.Log("Happiness:" + playerStatsDict[PlayerStats.HAPPINESS]);
        Debug.Log("Money:" + playerStatsDict[PlayerStats.MONEY]);*/
        playerStatsDict[PlayerStats.ENERGY] -= energyLevelCutValue;
        PlayerStatsObserver.onPlayerStatChanged(PlayerStats.ENERGY, playerStatsDict);
    }


    public void Ride(float energyLevelCutValue)
    {
        playerStatsDict[PlayerStats.ENERGY] -= energyLevelCutValue;
        PlayerStatsObserver.onPlayerStatChanged(PlayerStats.ENERGY, playerStatsDict);
    }

    public void StatsChecker(float currentEnergy, float currentHappiness, float currentHunger)
    {
        if (currentEnergy <= 10 | currentHappiness <= 10 | currentHunger <= 10)//checks if any of the stats reaches lower limit
        {
            float sumOfStats = currentEnergy + currentHappiness + currentHunger;

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
        //Debug.Log("Money after hospi:" + playerStatsDict[PlayerStats.MONEY]);

        hospitalizedPrompt.SetActive(false);
    }

    public void AddClockTime(float addedClockValue)
    {
        currentTime += addedClockValue;
        int minutes = 0;
        int hours = 0; 
        //Debug.Log("Current Time = " + currentTime);
        //clockValue.text = currentTime.ToString() + ":00";

        if (currentTime % 1 != 0)
        {
            Debug.Log("is FLOAT");
            hours = (int)currentTime;
            float temp = (currentTime % 1) * 100;
            minutes = (int)temp;
            Debug.Log("Hours = " + hours);
            Debug.Log("Minutes = " + minutes);

            if (minutes >= 60)
            {
                minutes = 0;
                hours ++;

                if (hours > 12)
                {
                    hours = 1;
                }
                clockValue.text = hours.ToString() + ":00";
                currentTime = hours;
            }
            else if (hours > 12)
            {
                hours = 1;
                clockValue.text = hours.ToString() + ":" + minutes.ToString();
                currentTime = hours + (minutes / 100);
            }
            else
            {
                clockValue.text = hours.ToString() + ":" + minutes.ToString();
            }
        }
        else
        {
            Debug.Log("is INT");
            hours = (int)currentTime;
            clockValue.text = hours.ToString() + ":00";
        }
    }
}
