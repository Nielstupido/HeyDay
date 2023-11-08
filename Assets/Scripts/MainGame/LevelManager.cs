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
    MAXSTATS
}


public enum MissionStatus
{
    PENDING,
    COMPLETED
}


public class LevelManager : MonoBehaviour
{
    public delegate void OnFinishedPlayerAction(MissionType missionType, float addedNumber = 0, Building building = null, PlayerStats playerStats = PlayerStats.NONE);
    public static OnFinishedPlayerAction onFinishedPlayerAction;


    public void RemoveMission()
    {
    }
}
