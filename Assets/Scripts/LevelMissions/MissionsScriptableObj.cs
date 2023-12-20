using UnityEngine;
using System;


[CreateAssetMenu(menuName = "Game Missions", fileName = "Mission")]
[Serializable]
public class MissionsScriptableObj : ScriptableObject
{
    public string id;
    public MissionStatus missionStatus;
    public MissionType missionType;
    public string missionDets;
    public float currentNumberForMission;
    public float requiredNumberForMission;
    public Buildings targetBuilding;
    public ItemType targetIitemType;
    public APPS targetApp;
    public PlayerStats targetPlayerStats;

}