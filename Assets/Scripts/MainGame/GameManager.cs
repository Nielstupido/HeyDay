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
    [SerializeField] private PlayerInfoManager playerInfoManager;
    [SerializeField] private List<CharactersScriptableObj> characters = new List<CharactersScriptableObj>();
    private GameStateData currentGameStateData;
    private int currentGameLevel = 1;
    
    public int CurrentGameLevel {get{return currentGameLevel;}}
    public List<CharactersScriptableObj> Characters {set{characters = value;} get{return characters;}}
    public static GameManager Instance { get; private set; }


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
    
        GameUiController.onScreenOverlayChanged += UpdateBottomOverlay;
    }


    private void OnDestroy()
    {
        GameUiController.onScreenOverlayChanged -= UpdateBottomOverlay;
    }


    private void Start()
    {
        if (PlayerPrefs.GetInt("GameMode") == 0)
        {
            playerInfoManager.OpenCharacterCreationOVerlay();
        }
        else
        {
            LoadSavedGame();
        }
    }


    public void StartGame()
    {
        defaultBottomOverlay.SetActive(true);
        pauseBtn.SetActive(true);
        //AudioManager.Instance.PlayMusic("Theme");
    }


    public void LoadSavedGame()
    {
        var res = GameDataManager.Instance.LoadAllGameStateData();
        var gameStateRes = GameDataManager.Instance.GetCurrentGameState(PlayerPrefs.GetString("PlayerName"));

        if (gameStateRes.Item1 == null)
        {
            playerInfoManager.OpenCharacterCreationOVerlay();
        }
        else
        {
            currentGameStateData = gameStateRes.Item1;
            StartGame();
        }
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
