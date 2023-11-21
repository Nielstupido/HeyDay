using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System;
using Newtonsoft.Json;


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
    NA,
    VEHICLE,
    APPLIANCE,
    CONSUMABLE,
    SERVICE
}


public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject bottomOverlay;
    [SerializeField] private GameObject pauseBtn;
    [SerializeField] private BudgetSetter budgetSetter;
    private int currentGameLevel = 1;
    
    public int CurrentGameLevel {get{return currentGameLevel;}}
    public static GameManager Instance { get; private set; }


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
        StartLevel();
    }


    public void StartGame()
    {
        bottomOverlay.SetActive(true);
        pauseBtn.SetActive(true);
        //AudioManager.Instance.PlayMusic("Theme");
    }


    public void LoadSavedGame()
    {

    }


    private void StartLevel()
    {
        budgetSetter.PrepareBudgeSetter(100f);
    }


    public string EnumStringParser<T>(T enumElement)
    {
        return enumElement.ToString().Replace("_", " ");
    }
}
