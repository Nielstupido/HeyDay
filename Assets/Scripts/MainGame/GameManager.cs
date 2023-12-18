using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System;
using System.Linq;
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
    SHOW_SMALL_BOTTOM_OVERLAY,
    HIDE_BOTTOM_OVERLAY
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
    [SerializeField] private InteractionSystemManager interactionSystemManager;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private EndGameManager endGameManager;
    [SerializeField] private List<CharactersScriptableObj> characters = new List<CharactersScriptableObj>();

    private List<string> characterNamesMale = new List<string>(){
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

    private List<string> characterNamesFemale = new List<string>(){
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
    private int currentGameLevel = 0;
    private int randomNum;
    private int inflationDuration;
    private float inflationRate = 0f;
    private UIactions currentUIaction;
    public float InflationRate { get{return inflationRate;} set{inflationRate = value;}}
    public int InflationDuration { get{return inflationDuration;} set{inflationDuration = value;}}
    private List<Building> meetupLocBuildings = new List<Building>();
    private List<Buildings> buildingsArr = new List<Buildings>();
    public int CurrentGameLevel {set{currentGameLevel = value;} get{return currentGameLevel;}}
    public List<CharactersScriptableObj> Characters {set{characters = value;} get{return characters;}}
    public List<Building> MeetupLocBuildings {set{meetupLocBuildings = value;} get{return meetupLocBuildings;}}
    public GameStateData CurrentGameStateData {set{currentGameStateData = value;} get{return currentGameStateData;}}
    public delegate void OnGameStarted();
    public static OnGameStarted onGameStarted;
    public delegate void OnSaveGameStateData();
    public static OnSaveGameStateData onSaveGameStateData;
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
        TimeManager.onDayAdded += AssignNpcToBuilding;
        buildingsArr = Enum.GetValues(typeof(Buildings)).Cast<Buildings>().ToList();
        onSaveGameStateData += SaveGameData;
    }


    private void OnDestroy()
    {
        GameUiController.onScreenOverlayChanged -= UpdateBottomOverlay;
        TimeManager.onDayAdded -= AssignNpcToBuilding;
        onSaveGameStateData -= SaveGameData;
    }


    private void OnEnable()
    {
        GameUiController.onScreenOverlayChanged += UpdateBottomOverlay;
        TimeManager.onDayAdded += AssignNpcToBuilding;
    }


    private void OnDisable()
    {
        GameUiController.onScreenOverlayChanged -= UpdateBottomOverlay;
        TimeManager.onDayAdded -= AssignNpcToBuilding;
    }


    private void Start()
    {
        if (PlayerPrefs.GetInt("GameMode") == 0) 
        {
            PlayerPrefs.SetInt("GameStart", 0);
            playerInfoManager.OpenCharacterCreationOVerlay();
        }
        else
        {
            LoadSavedGame();
        }
    }


    private void PrepareCharacters()
    {
        if (currentGameStateData.characters.Count != 0)
        {
            characters = currentGameStateData.characters;
        }
        else
        {
            foreach (var character in characters)
            {
                if (character.characterGender == Gender.MALE)
                {
                    randomNum = UnityEngine.Random.Range(0, characterNamesMale.Count);
                    character.PrepareCharacter(characterNamesMale[randomNum], 15, RelStatus.STRANGERS);
                    characterNamesMale.RemoveAt(randomNum);
                }
                else
                {
                    randomNum = UnityEngine.Random.Range(0, characterNamesFemale.Count);
                    character.PrepareCharacter(characterNamesFemale[randomNum], 15, RelStatus.STRANGERS);
                    characterNamesFemale.RemoveAt(randomNum);
                }

            }
        }
    }


    public void StartGame(GameStateData gameStateData)
    {
        this.currentGameStateData = gameStateData;

        if (PlayerPrefs.GetInt("GameStart") == 0)
        {
            this.currentGameStateData.playerName = Player.Instance.PlayerName;
            this.currentGameStateData.currentCharacter = Player.Instance.CurrentCharacter;
            this.currentGameStateData.playerGender = Player.Instance.PlayerGender;
            GameDataManager.Instance.SaveGameData(Player.Instance.PlayerName, new GameStateData());
        }

        if (PlayerPrefs.GetInt("FirstLoad") == 0) 
        {
            PlayerPrefs.SetInt("FirstLoad", 1);
            levelManager.currentActiveMissions = this.currentGameStateData.currentActiveMissions;
            levelManager.InstantiateCurrentLvlMissions();
        }
        else
        {
            levelManager.PrepareCurrentLevelMissions();
        }

        PlayerPrefs.SetInt("GameStart", 1);
        PrepareCharacters();
        UpdateBottomOverlay(UIactions.SHOW_DEFAULT_BOTTOM_OVERLAY);
        pauseBtn.SetActive(true);
        AssignNpcToBuilding(0);
        TimeManager.Instance.AddClockTime(true, 7f);
        AudioManager.Instance.PlayMusic("Theme2");
        onGameStarted();
        LoadGameData();
        AnimOverlayManager.Instance.StartScreenFadeLoadScreen();
    }


    private void LoadGameData()
    {
        this.currentGameLevel = currentGameStateData.currentGameLevel;
        this.inflationDuration = currentGameStateData.inflationDuration;
        this.inflationRate = currentGameStateData.inflationRate;
    }


    private void SaveGameData()
    {
        currentGameStateData.currentGameLevel = this.currentGameLevel;
        currentGameStateData.inflationDuration = this.inflationDuration;
        currentGameStateData.inflationRate = this.inflationRate;
        levelManager.PrepareCurrentLevelMissions();
    }


    public void StartNextLevel()
    {
        UpdateBottomOverlay(UIactions.SHOW_DEFAULT_BOTTOM_OVERLAY);
        pauseBtn.SetActive(true);
        AssignNpcToBuilding(0);
        levelManager.PrepareCurrentLevelMissions();
    }


    public void StartLevel()
    {
        BudgetSystem.Instance.ResetBudget();
        Player.Instance.ResetLvlExpenses();
        budgetSetter.PrepareBudgeSetter(Player.Instance.PlayerCash);
        levelManager.PrepareCurrentLevelMissions();
    }


    public void LoadSavedGame()
    {
        // var res = GameDataManager.Instance.LoadAllGameStateData();
        var gameStateRes = GameDataManager.Instance.GetCurrentGameState(PlayerPrefs.GetString("PlayerName"));

        if (gameStateRes.Item1 == null)
        {
            PlayerPrefs.SetInt("GameStart", 0);
            playerInfoManager.OpenCharacterCreationOVerlay();
        }
        else
        {
            PlayerPrefs.SetInt("FirstLoad", 0);
            StartGame(gameStateRes.Item1);
        }
    }


    public void SaveNpcData(CharactersScriptableObj npcData)
    {
        characters[characters.FindIndex( (character) => character.name == npcData.name )] = npcData;
        characters[characters.FindIndex( (character) => character.name == npcData.name )].currentBuildiing = Buildings.NONE;
        BuildingManager.Instance.PrepareNpc();
    }


    public void InteractWithNPC(string characterName)
    {
        interactionSystemManager.Interact(GetCharacterValue(characterName));
    }


    public void UpdateBottomOverlay(UIactions uIaction)
    {
        currentUIaction = uIaction;

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
        else if (uIaction == UIactions.HIDE_BOTTOM_OVERLAY)
        {
            smallBottomOverlay.SetActive(false);
            defaultBottomOverlay.SetActive(false);
        }
    }


    public UIactions GetCurrentUIaction()
    {
        return currentUIaction;
    }


    public string EnumStringParser<T>(T enumElement)
    {
        return enumElement.ToString().Replace("_", " ");
    }


    public CharactersScriptableObj GetCharacterValue(string name)
    {
        foreach(CharactersScriptableObj character in characters)
        {
            if (name == character.characterName)
            {
                return character;
            }
        }

        return null;
    }


    public Building GetOpenBuilding(float time)
    {
        meetupLocBuildings = ShuffleList(meetupLocBuildings);

        foreach (Building highlightedBuilding in meetupLocBuildings)
        {
            if (highlightedBuilding.buildingOpeningTime > highlightedBuilding.buildingClosingTime)
            {
                if (time > highlightedBuilding.buildingOpeningTime || time < (highlightedBuilding.buildingClosingTime - 1.5f))
                {
                    return highlightedBuilding;
                }

                return null;
            }
            else
            {
                if (time > highlightedBuilding.buildingOpeningTime && time < (highlightedBuilding.buildingClosingTime - 1.5f))
                {
                    return highlightedBuilding;
                }
                    
                return null;
            }
        }

        return null;
    }


    public void GameOver()
    {
        AnimOverlayManager.Instance.StopAnim();
        endGameManager.ShowOutro(false);
    }


    public void GameFinished()
    {
        endGameManager.ShowOutro(true);
    }


    private List<Building> ShuffleList(List<Building> list)
    {
        var random = new System.Random();
        var newShuffledList = new List<Building>();
        var listcCount = list.Count;

        for (int i = 0; i < listcCount; i++)
        {
            var randomElementInList = random.Next(0, list.Count);
            newShuffledList.Add(list[randomElementInList]);
            list.Remove(list[randomElementInList]);
        }
        return newShuffledList;
    }
    

    private void AssignNpcToBuilding(int dayCount)
    {
        foreach (var character in characters)
        {
            randomNum = UnityEngine.Random.Range(0, buildingsArr.Count);
            character.currentBuildiing = buildingsArr[randomNum];
        }
    }
}
