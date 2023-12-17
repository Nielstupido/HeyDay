using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public enum MissionType
{
    VISIT,
    BROWSEJOB,
    STUDYHR,
    WORKHR,
    SLEEPHR,
    MAXSTATS,
    RENTROOM,
    OPENSAVINGSACC,
    APPLYJOB,
    DEPOSITSAVINGSACC,
    DEBTFREE,
    FINISHDEGREE,
    ENROLL,
    WALK,
    BUY,
    EAT,
    PARTY,
    USEAPP,
    WATCHMOVIE,
    HAIRSERVICE,
    INTERACT,
    UPRELATIONSHIP,
    COMMUTE,
    MASSAGE,
    BORROWMONEY,
    MAKEFRIEND,
    MAKECLOSEFRIEND
}


public enum MissionStatus
{
    PENDING,
    COMPLETED
}


public enum APPS
{
    NONE,
    OLSHOP,
    FINANCETRACKER,
    GOALTRACKER,
    PHONEBOOK
}


public class LevelManager : MonoBehaviour
{
    public delegate void OnFinishedPlayerAction(
        MissionType actionMissionType, 
        float actionAddedNumber = 0, 
        Buildings interactedBuilding = Buildings.NONE, 
        PlayerStats affectedPlayerStats = PlayerStats.NONE, 
        ItemType interactedItemType = ItemType.NA,
        APPS interactedApp = APPS.NONE,
        PlayerStats interactedPlayerStats = PlayerStats.NONE);
    public static OnFinishedPlayerAction onFinishedPlayerAction;

    [SerializeField] private MissionsHolder missionsHolder;
    [SerializeField] private Transform missionPrefabsHolder;
    [SerializeField] private GameObject missionPrefab;
    [SerializeField] private GameObject missionOverlay;
    [SerializeField] private GameObject missionPopUp;
    [SerializeField] private GameObject nextLevelBtn;
    [SerializeField] private TextMeshProUGUI missionOverlayLevelText; 
    [SerializeField] private CameraMovement cameraMovement; 
    private Dictionary<string, List<MissionsScriptableObj>> allMissions = new Dictionary<string, List<MissionsScriptableObj>>();
    public List<MissionsScriptableObj> currentActiveMissions = new List<MissionsScriptableObj>();
    private string tempLevelName;
    private string[] tempSplitIdHolder;
    private int missionLevelCounter;
    private UIactions lastUIactions;
    public static LevelManager Instance { get; private set; }


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

        GameManager.onSaveGameStateData += SaveGameData;
        LoadAllMissions();
    }


    private void OnDestroy()
    {
        GameManager.onSaveGameStateData -= SaveGameData;
    }


    private void SaveGameData()
    {
        GameManager.Instance.CurrentGameStateData.currentActiveMissions = this.currentActiveMissions;
    }


    //loads all missions to a dictionary for easy access during gameplay
    private void LoadAllMissions()
    {
        missionLevelCounter = 1;

        foreach (MissionsScriptableObj mission in missionsHolder.missions)
        {
            tempLevelName = "Level ";
            tempSplitIdHolder = mission.id.Split(".");

            if (tempSplitIdHolder[0] != missionLevelCounter.ToString())
            {
                missionLevelCounter++;
            }

            tempLevelName += missionLevelCounter.ToString();

            if (allMissions.ContainsKey(tempLevelName))
            {
                allMissions[tempLevelName].Add(mission);
            }
            else
            {
                allMissions.Add(tempLevelName, new List<MissionsScriptableObj>{mission});
            }
        }
    }


    //prepares all assigned missions for the current level
    public void PrepareCurrentLevelMissions()
    {
        currentActiveMissions.Clear();
        tempLevelName = "Level ";

        if (GameManager.Instance.CurrentGameLevel == 0)
        {
            tempLevelName += "1";
        }
        else
        {
            tempLevelName += GameManager.Instance.CurrentGameLevel.ToString();
        }

        missionOverlayLevelText.text = tempLevelName;

        foreach (MissionsScriptableObj mission in allMissions[tempLevelName])
        {
            currentActiveMissions.Add(mission);
        }

        InstantiateCurrentLvlMissions();
    }
    

    public void InstantiateCurrentLvlMissions()
    {
        for (int i = 0; i < missionPrefabsHolder.childCount; i++)
        {
            Object.Destroy(missionPrefabsHolder.GetChild(i).gameObject);
        }

        foreach(MissionsScriptableObj mission in currentActiveMissions)
        {
            GameObject newMissionObj = Instantiate(missionPrefab, Vector3.zero, Quaternion.identity, missionPrefabsHolder);
            Missions newMission = newMissionObj.GetComponent<Missions>();
            newMission.LoadMissionDets(mission);
        }
    }


    public void OnMissionFinished(MissionsScriptableObj mission)
    {
        currentActiveMissions.Remove(mission);

        if (currentActiveMissions.Count == 0)
        {
            nextLevelBtn.SetActive(true);
        }
    }


    public void CloseMissionOverlay()
    {
        cameraMovement.enabled = true;
        AudioManager.Instance.PlaySFX("Select");
        OverlayAnimations.Instance.AnimCloseOverlay(missionPopUp, missionOverlay);
        GameManager.Instance.UpdateBottomOverlay(lastUIactions);
    }


    public void OpenMissionOverlay()
    {
        cameraMovement.enabled = false;
        missionOverlay.SetActive(true);
        AudioManager.Instance.PlaySFX("Select");
        OverlayAnimations.Instance.AnimOpenOverlay(missionPopUp);
        lastUIactions = GameManager.Instance.GetCurrentUIaction();
        GameManager.Instance.UpdateBottomOverlay(UIactions.HIDE_BOTTOM_OVERLAY);
    }


    public void OnLevelFinished()
    {
        AnimOverlayManager.Instance.StartScreenFadeLoadScreen();
        CloseMissionOverlay();
        nextLevelBtn.SetActive(false);
        GameManager.Instance.UpdateBottomOverlay(UIactions.HIDE_BOTTOM_OVERLAY);
        EndLevelManager.Instance.LevelFinished();
    }
}
