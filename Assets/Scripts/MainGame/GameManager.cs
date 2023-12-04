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
    SHOW_DEFAULT_BOTTOM_OVERLAY,
    SHOW_SMALL_BOTTOM_OVERLAY
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
    [SerializeField] private List<string> characterNamesMale = new List<string>(){
                                                            "Jose Reyes",
                                                            "Andres Cruz",
                                                            "Gabriel Santos",
                                                            "Juan Rivera",
                                                            "Ricardo Dela Cruz",
                                                            "Antonio Ramos",
                                                            "Miguel Castro",
                                                            "Eduardo Lim",
                                                            "Manuel Ocampo",
                                                            "Rodrigo Gonzales"
                                                            };

    [SerializeField] private List<string> characterNamesFemale = new List<string>(){
                                                            "Maria Rodriguez",
                                                            "Sofia Garcia",
                                                            "Isabella Fernandez",
                                                            "Andrea Lopez",
                                                            "Beatriz Santos",
                                                            "Carla Reyes",
                                                            "Elena Cruz",
                                                            "Patricia Reyes",
                                                            "Angelica Rivera",
                                                            "Camila Villanueva"
                                                            };

    private GameStateData currentGameStateData;
    private int currentGameLevel = 1;
    private int randomNum;
    
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
        StartGame(); //for debugging
        // if (PlayerPrefs.GetInt("GameMode") == 0) 
        // {
        //     playerInfoManager.OpenCharacterCreationOVerlay();
        // }
        // else
        // {
        //     LoadSavedGame();
        // }
    }


    private void PrepareCharacters()
    {
        foreach (var character in characters)
        {
            if (character.characterGender == Gender.MALE)
            {
                randomNum = UnityEngine.Random.Range(0, characterNamesMale.Count);
                character.PrepareCharacter(characterNamesMale[randomNum], 15);
                characterNamesMale.RemoveAt(randomNum);
            }
            else
            {
                randomNum = UnityEngine.Random.Range(0, characterNamesFemale.Count);
                character.PrepareCharacter(characterNamesFemale[randomNum], 15);
                characterNamesFemale.RemoveAt(randomNum);
            }

        }
    }


    public void StartGame()
    {
        PrepareCharacters();
        UpdateBottomOverlay(UIactions.SHOW_DEFAULT_BOTTOM_OVERLAY);
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


    public void UpdateBottomOverlay(UIactions uIaction)
    {
        if (uIaction == UIactions.SHOW_SMALL_BOTTOM_OVERLAY)
        {
            smallBottomOverlay.SetActive(true);
            defaultBottomOverlay.SetActive(false);
        }
        else if (uIaction == UIactions.SHOW_DEFAULT_BOTTOM_OVERLAY)
        {
            smallBottomOverlay.SetActive(false);
            defaultBottomOverlay.SetActive(true);
        }
    }


    public string EnumStringParser<T>(T enumElement)
    {
        return enumElement.ToString().Replace("_", " ");
    }
}
