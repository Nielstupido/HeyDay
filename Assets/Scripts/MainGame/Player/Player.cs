using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerStats
{
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
    private IDictionary<PlayerStats, float> playerStatsDict = new Dictionary<PlayerStats, float>();
    private string playerName;
    private Gender playerGender;
    public string PlayerName { set{playerName = value;} get{return playerName;}}
    public Gender PlayerGender { set{playerGender = value;} get{return playerGender;}}
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
        playerStatsDict.Add(PlayerStats.HAPPINESS, 100f);
        playerStatsDict.Add(PlayerStats.HUNGER, 100f);
        playerStatsDict.Add(PlayerStats.ENERGY, 100f);
        playerStatsDict.Add(PlayerStats.MONEY, 100f);
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
        playerStatsDict[PlayerStats.ENERGY] -= energyLevelCutValue;
        PlayerStatsObserver.onPlayerStatChanged(PlayerStats.ENERGY, playerStatsDict);
    }


    public void Ride(float energyLevelCutValue)
    {
        playerStatsDict[PlayerStats.ENERGY] -= energyLevelCutValue;
        PlayerStatsObserver.onPlayerStatChanged(PlayerStats.ENERGY, playerStatsDict);
    }
}
