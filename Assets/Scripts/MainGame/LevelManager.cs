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
    MASSAGE
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
    private IDictionary<string, List<MissionsScriptableObj>> allMissions = new Dictionary<string, List<MissionsScriptableObj>>();
    private List<MissionsScriptableObj> currentActiveMissions = new List<MissionsScriptableObj>();
    private string tempLevelName;
    private string[] tempSplitIdHolder;
    private int levelCounter;
    private int currentLevel;


    private void Start()
    {
        LoadAllMissions();
    }


    //loads all missions to a dictionary for easy access during gameplay
    private void LoadAllMissions()
    {
        levelCounter = 1;

        foreach (MissionsScriptableObj mission in missionsHolder.missions)
        {
            tempLevelName = "Level ";
            tempSplitIdHolder = mission.id.Split(".");

            if (tempSplitIdHolder[0] != levelCounter.ToString())
            {
                levelCounter++;
            }

            tempLevelName += levelCounter.ToString();

            if (allMissions.ContainsKey(tempLevelName))
            {
                allMissions[tempLevelName].Add(mission);
            }
            else
            {
                allMissions.Add(tempLevelName, new List<MissionsScriptableObj>{mission});
            }
        }

        PrepareCurrentLevelMissions();
    }


    //prepares all assigned missions for the current level
    public void PrepareCurrentLevelMissions()
    {
        currentActiveMissions.Clear();
        tempLevelName = "Level ";
        tempLevelName += GameManager.Instance.CurrentGameLevel.ToString();

        foreach (MissionsScriptableObj mission in allMissions[tempLevelName])
        {
            currentActiveMissions.Add(mission);
        }

        foreach(MissionsScriptableObj mission in currentActiveMissions)
        {
            GameObject newMissionObj = Instantiate(missionPrefab, Vector3.zero, Quaternion.identity, missionPrefabsHolder);
            Missions newMission = newMissionObj.GetComponent<Missions>();
            newMission.LoadMissionDets(mission);
            newMission.MissionCheckBox = newMissionObj.transform.GetChild(0).GetComponent<Image>();
            newMission.MissionDetsText = newMissionObj.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        }
    }
}
