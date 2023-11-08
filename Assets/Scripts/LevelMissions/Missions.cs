using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missions : MonoBehaviour
{
    private MissionStatus missionStatus = MissionStatus.PENDING;
    private string missionDets;
    private float currentNumberForGoal;
    private float requiredNumberForGoal;
    private Buildings targetBuilding;


    public Missions(MissionsScriptableObj mission)
    {
        this.missionDets = mission.missionDets;
        this.currentNumberForGoal = mission.currentNumberForGoal;
        this.requiredNumberForGoal = mission.requiredNumberForGoal;
        this.targetBuilding = mission.targetBuilding;
    }
}
