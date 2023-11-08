using UnityEngine;

[CreateAssetMenu(menuName = "Game Missions")]
public class MissionsScriptableObj : ScriptableObject
{
    public MissionStatus missionStatus = MissionStatus.PENDING;
    public string missionDets;
    public float currentNumberForGoal;
    public float requiredNumberForGoal;
    public Buildings targetBuilding;
}