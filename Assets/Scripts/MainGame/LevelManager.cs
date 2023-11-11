using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
    COMMUTE
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
    public delegate void OnFinishedPlayerAction(MissionType missionType, float addedNumber = 0, Building building = null, PlayerStats playerStats = PlayerStats.NONE);
    public static OnFinishedPlayerAction onFinishedPlayerAction;

    [SerializeField] private MissionsHolder missionsHolder;
    private IDictionary<string, List<MissionsScriptableObj>> allMissions = new Dictionary<string, List<MissionsScriptableObj>>();
    private List<MissionsScriptableObj> currentMissions = new List<MissionsScriptableObj>();
    private string tempLevelName;
    private string[] tempSplitIdHolder;
    private int levelCounter;
    private int currentLevel;


    private void Start()
    {
        LoadAllMissions();
    }


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
    }


    public void PrepareCurrentLevelMissions()
    {
        currentMissions.Clear();
        tempLevelName = "Level ";
        tempLevelName += GameManager.Instance.CurrentGameLevel.ToString();

        foreach (MissionsScriptableObj mission in allMissions[tempLevelName])
        {
            currentMissions.Add(mission);
        }
    }
}
