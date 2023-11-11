using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missions : MonoBehaviour
{
    private string missionID;
    private MissionStatus missionStatus;
    private MissionType missionType;
    private string missionDets;
    private float currentNumberForMission;
    private float requiredNumberForMission;
    private Buildings targetBuilding;
    private ItemType targetIitemType;
    private APPS targetApp;


    public Missions(MissionsScriptableObj mission)
    {
        this.missionID = mission.id;
        this.missionDets = mission.missionDets;
        this.missionType = mission.missionType;
        this.currentNumberForMission = mission.currentNumberForMission;
        this.requiredNumberForMission = mission.requiredNumberForMission;
        this.targetBuilding = mission.targetBuilding;
        this.targetIitemType = mission.targetIitemType;
        this.targetApp = mission.targetApp;
    }
}
