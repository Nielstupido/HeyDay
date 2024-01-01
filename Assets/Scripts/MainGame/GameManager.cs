using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System;
using System.Linq;
using Newtonsoft.Json;
using System.Collections;


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
    [SerializeField] private EndGameManager endGameManager;
    [SerializeField] private TextMeshProUGUI playerNameTextDisplay;
    [SerializeField] private TextMeshProUGUI playerAgeTextDisplay;
    [SerializeField] private Image playerBustIcon;
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
        if (currentGameStateData.charactersID.Count != 0)
        {
            for (int i = 0; i < currentGameStateData.charactersID.Count; i++)
            {
                foreach (CharactersScriptableObj charac in characters)
                {
                    if (charac.characterID == currentGameStateData.charactersID[i])
                    {
                        charac.PrepareCharacter(
                            currentGameStateData.charactersName[i],
                            currentGameStateData.charactersRelStatBarValue[i],
                            StringEnumParser<RelStatus>(currentGameStateData.charactersRelStatus[i]),
                            currentGameStateData.charactersCurrentDebt[i],
                            currentGameStateData.charactersBeenFriends[i],
                            currentGameStateData.charactersNumberObtained[i],
                            currentGameStateData.charactersGotCalledToday[i]
                            );
                        break;
                    }
                }
            }
        }
        else
        {
            foreach (CharactersScriptableObj character in characters)
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
        currentGameStateData = gameStateData;
        LoadGameData();

        if (PlayerPrefs.GetInt("GameStart") == 0)
        {
            currentGameStateData.playerName = Player.Instance.PlayerName;
            currentGameStateData.currentCharacter = Player.Instance.CurrentCharacter.characterID;
            currentGameStateData.playerGender = Player.Instance.PlayerGender.ToString();
            GameDataManager.Instance.SaveGameData(Player.Instance.PlayerName, currentGameStateData);
        }

        if (PlayerPrefs.GetInt("FirstLoad") == 0) 
        {
            PlayerPrefs.SetInt("FirstLoad", 1);


            LevelManager.Instance.currentActiveMissions.Clear();
            bool isExited = false;

            for (int i = 0; i < gameStateData.currentActiveMissionsID.Count; i++)
            {
                foreach (string missionLvl in LevelManager.Instance.AllMissions.Keys)
                {
                    foreach (MissionsScriptableObj mission in LevelManager.Instance.AllMissions[missionLvl])
                    {
                        if (mission.id == gameStateData.currentActiveMissionsID[i])
                        {
                            mission.missionStatus = StringEnumParser<MissionStatus>(gameStateData.currentActiveMissionsStatus[i]);
                            mission.currentNumberForMission = gameStateData.currentActiveMissionsCurrentNumber[i];
                            LevelManager.Instance.currentActiveMissions.Add(mission);
                            isExited = true;
                            break;
                        }
                    }

                    if (isExited)
                    {
                        isExited = false;
                        break;
                    }
                }
            }

            LevelManager.Instance.PrepareLevelDets();
            LevelManager.Instance.InstantiateCurrentLvlMissions();

            playerAgeTextDisplay.text = gameStateData.playerAge.ToString();
            playerNameTextDisplay.text = gameStateData.playerName;

            foreach(CharactersScriptableObj charac in characters)
            {
                if (charac.characterID == gameStateData.currentCharacter)
                {
                    playerBustIcon.sprite = charac.bustIcon;
                }
            }
            
            playerBustIcon.SetNativeSize();
        }
        else
        {
            LevelManager.Instance.PrepareCurrentLevelMissions();
        }

        PlayerPrefs.SetInt("GameStart", 1);
        PrepareCharacters();
        AnimOverlayManager.Instance.StartScreenFadeLoadScreen();
        pauseBtn.SetActive(true);
        AssignNpcToBuilding(0);
        TimeManager.Instance.AddClockTime(true, 7f);
        AudioManager.Instance.PlayMusic("Theme2");
        onGameStarted();
        UpdateBottomOverlay(UIactions.SHOW_DEFAULT_BOTTOM_OVERLAY);

        if (PlayerPrefs.GetInt("GameRestart") != 0)
        {
            StartCoroutine(DelayOpenMissions());
        }
        
        PlayerPrefs.SetInt("GameRestart", 1);

        Dictionary<PlayerStats, float> currentPlayerStatsDict = new Dictionary<PlayerStats, float>
        {
            { PlayerStats.HAPPINESS, currentGameStateData.playerStatsDict[0] },
            { PlayerStats.HUNGER, currentGameStateData.playerStatsDict[1] },
            { PlayerStats.ENERGY, currentGameStateData.playerStatsDict[2] },
            { PlayerStats.MONEY, currentGameStateData.playerStatsDict[3] }
        };

        PlayerStatsObserver.onPlayerStatChanged(PlayerStats.ALL, currentPlayerStatsDict);
    }


    private IEnumerator DelayOpenMissions()
    {
        LevelManager.Instance.CloseMissionOverlay();
        yield return new WaitForSeconds(2f);
        LevelManager.Instance.OpenMissionOverlay();
        yield return null;
    }


    private IEnumerator DelayRefreshStats(Dictionary<PlayerStats, float> currentPlayerStatsDict)
    {
        yield return new WaitForSeconds(2f);
        PlayerStatsObserver.onPlayerStatChanged(PlayerStats.ALL, currentPlayerStatsDict);
        yield return null;
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

        currentGameStateData.charactersID.Clear();
        currentGameStateData.charactersName.Clear();
        currentGameStateData.charactersRelStatus.Clear();
        currentGameStateData.charactersRelStatBarValue.Clear();
        currentGameStateData.charactersCurrentDebt.Clear();
        currentGameStateData.charactersNumberObtained.Clear();
        currentGameStateData.charactersBeenFriends.Clear();
        currentGameStateData.charactersGotCalledToday.Clear();

        foreach (CharactersScriptableObj charac in characters)
        {
            currentGameStateData.charactersID.Add(charac.characterID);
            currentGameStateData.charactersName.Add(charac.characterName);
            currentGameStateData.charactersRelStatus.Add(charac.relStatus.ToString());
            currentGameStateData.charactersRelStatBarValue.Add(charac.relStatBarValue);
            currentGameStateData.charactersCurrentDebt.Add(charac.currentDebt);
            currentGameStateData.charactersNumberObtained.Add(charac.numberObtained);
            currentGameStateData.charactersBeenFriends.Add(charac.beenFriends);
            currentGameStateData.charactersGotCalledToday.Add(charac.gotCalledToday);
        }

        LevelManager.Instance.PrepareCurrentLevelMissions();
    }


    public void StartNextLevel()
    {
        UpdateBottomOverlay(UIactions.SHOW_DEFAULT_BOTTOM_OVERLAY);
        pauseBtn.SetActive(true);
        AssignNpcToBuilding(0);
        LevelManager.Instance.PrepareCurrentLevelMissions();
        StartCoroutine(DelayOpenMissions());
    }


    public void StartLevel()
    {
        BudgetSystem.Instance.ResetBudget();
        Player.Instance.ResetLvlExpenses();
        LevelManager.Instance.PrepareCurrentLevelMissions();
        budgetSetter.PrepareBudgeSetter(Player.Instance.PlayerCash);
    }


    public void LoadSavedGame()
    {
        // var res = GameDataManager.Instance.LoadAllGameStateData();
        // var gameStateRes = GameDataManager.Instance.GetCurrentGameState(PlayerPrefs.GetString("PlayerName"));

        if (!GameDataManager.Instance.AllPlayersGameStateData.ContainsKey(PlayerPrefs.GetString("PlayerName")))
        {
            PlayerPrefs.SetInt("GameStart", 0);
            PlayerPrefs.SetInt("FirstLoad", 1);
            playerInfoManager.OpenCharacterCreationOVerlay();
        }
        else
        {
            PlayerPrefs.SetInt("FirstLoad", 0);
            PlayerPrefs.SetInt("GameStart", 1);
            StartGame(GameDataManager.Instance.AllPlayersGameStateData[PlayerPrefs.GetString("PlayerName")]);
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


    public T StringEnumParser<T>(string enumString)
    {
        T parsed_enum = (T)System.Enum.Parse( typeof(T), enumString);
        return parsed_enum;
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


    public void GameOver(string gameOverReason)
    {
        AnimOverlayManager.Instance.StopAnim();
        endGameManager.ShowOutro(false, gameOverReason);
    }


    public void GameFinished()
    {
        endGameManager.ShowOutro(true, "");
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
