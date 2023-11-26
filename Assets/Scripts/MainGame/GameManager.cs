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

public enum UIactions
{
    SHOW,
    HIDE
}


public class GameUiController : MonoBehaviour
{
    public delegate void OnScreenOverlayChanged(UIactions uIaction);
    public static OnScreenOverlayChanged onScreenOverlayChanged;
}


public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject defaultBottomOverlay;
    [SerializeField] private GameObject smallBottomOverlay;
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
    
        GameUiController.onScreenOverlayChanged += UpdateBottomOverlay;
    }


    private void OnDestroy()
    {
        GameUiController.onScreenOverlayChanged -= UpdateBottomOverlay;
    }


    public void StartGame()
    {
        defaultBottomOverlay.SetActive(true);
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


    private void UpdateBottomOverlay(UIactions uIaction)
    {
        if (uIaction == UIactions.SHOW)
        {
            smallBottomOverlay.SetActive(true);
        }
        else if (uIaction == UIactions.HIDE)
        {
            smallBottomOverlay.SetActive(false);
        }
    }


    public string EnumStringParser<T>(T enumElement)
    {
        return enumElement.ToString().Replace("_", " ");
    }
}
