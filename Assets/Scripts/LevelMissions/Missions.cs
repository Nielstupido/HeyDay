using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Missions : MonoBehaviour
{
    [SerializeField] private Sprite uncheckedBox;
    [SerializeField] private Sprite checkedBox;
    [SerializeField] private Image missionCheckBox;
    [SerializeField] private TextMeshProUGUI missionDetsText;
    [SerializeField] private TextMeshProUGUI missionProgressText;
    private string missionID;
    private MissionStatus missionStatus;
    private MissionType missionType;
    private string missionDets;
    private float currentNumberForMission;
    private float requiredNumberForMission;
    private Buildings targetBuilding;
    private ItemType targetIitemType;
    private APPS targetApp;
    private PlayerStats targetPlayerStats;
    private MissionsScriptableObj missionCopy;




    public void LoadMissionDets(MissionsScriptableObj mission)
    {
        this.missionCopy = mission;
        // this.missionID = mission.id;
        // this.missionStatus = mission.missionStatus;
        // this.missionDets = mission.missionDets;
        // this.missionType = mission.missionType;
        // this.currentNumberForMission = mission.currentNumberForMission;
        // this.requiredNumberForMission = mission.requiredNumberForMission;
        // this.targetBuilding = mission.targetBuilding;
        // this.targetIitemType = mission.targetIitemType;
        // this.targetApp = mission.targetApp;
        // this.targetPlayerStats = mission.targetPlayerStats;
        this.missionDetsText.text = this.missionCopy.missionDets;
        this.missionCheckBox.sprite = uncheckedBox;

        if (this.requiredNumberForMission == 0)
        {
            this.missionProgressText.gameObject.SetActive(false);
        }
        else
        {
            this.missionProgressText.text = this.currentNumberForMission.ToString() + "/" + this.requiredNumberForMission.ToString();
        }

        LevelManager.onFinishedPlayerAction += OnPlayerActionCompleted;

        if (this.checkedBox != null && this.missionCopy.missionStatus == MissionStatus.COMPLETED)
        {
            this.missionCheckBox.sprite = checkedBox;
            this.missionCheckBox.GetComponent<RectTransform>().sizeDelta = new Vector2(60f, 60f);
        }
    }


    private void OnDestroy()
    {
        LevelManager.onFinishedPlayerAction -= OnPlayerActionCompleted;
    }


    private void OnEnable()
    {
        LevelManager.onFinishedPlayerAction += OnPlayerActionCompleted;
    }


    private void OnDisable()
    {
        LevelManager.onFinishedPlayerAction -= OnPlayerActionCompleted;
    }


    //checks if the player action/activity links to an active mission
    private void OnPlayerActionCompleted(
        MissionType actionMissionType, 
        float actionAddedNumber = 0, 
        Buildings interactedBuilding = Buildings.NONE, 
        PlayerStats affectedPlayerStats = PlayerStats.NONE,
        ItemType interactedItemType = ItemType.NA,
        APPS interactedApp = APPS.NONE,
        PlayerStats interactedPlayerStats = PlayerStats.NONE)
    {
        if (this.missionCopy.missionType != actionMissionType)
        {
            return;
        }

        if (actionMissionType == MissionType.RENTROOM || actionMissionType == MissionType.OPENSAVINGSACC || actionMissionType == MissionType.DEPOSITSAVINGSACC ||
                actionMissionType == MissionType.APPLYJOB || actionMissionType == MissionType.DEBTFREE  || actionMissionType == MissionType.FINISHDEGREE ||
                actionMissionType == MissionType.ENROLL || actionMissionType == MissionType.WALK || actionMissionType == MissionType.PARTY ||
                actionMissionType == MissionType.WATCHMOVIE || actionMissionType == MissionType.HAIRSERVICE || actionMissionType == MissionType.INTERACT ||
                actionMissionType == MissionType.UPRELATIONSHIP || actionMissionType == MissionType.COMMUTE)
        {
            MissionDone();
        }
        else if (actionMissionType == MissionType.VISIT || actionMissionType == MissionType.BROWSEJOB || actionMissionType == MissionType.EAT)
        {
            if (this.missionCopy.targetBuilding == interactedBuilding)
            {
                MissionDone();
            }
        }
        else if (actionMissionType == MissionType.STUDYHR || actionMissionType == MissionType.WORKHR)
        {
            this.missionCopy.currentNumberForMission += actionAddedNumber;
            this.missionProgressText.text = this.missionCopy.currentNumberForMission.ToString() + "/" + this.missionCopy.requiredNumberForMission.ToString();

            if (this.missionCopy.requiredNumberForMission <= this.missionCopy.currentNumberForMission)
            {
                MissionDone();
            }
        }
        else if (actionMissionType == MissionType.SLEEPHR)
        {
            if (this.missionCopy.requiredNumberForMission <= actionAddedNumber)
            {
                this.missionProgressText.text = actionAddedNumber.ToString() + "/" + this.missionCopy.requiredNumberForMission.ToString();
                MissionDone();
            }
        }
        else if (actionMissionType == MissionType.MAXSTATS)
        {
            if (this.missionCopy.targetPlayerStats == interactedPlayerStats)
            {
                MissionDone();
            }
        }
        else if (actionMissionType == MissionType.BUY)
        {
            if (this.missionCopy.targetIitemType == interactedItemType || this.missionCopy.targetIitemType == ItemType.NA)
            {
                MissionDone();
            }
        }
        else if (actionMissionType == MissionType.USEAPP)
        {
            if (this.missionCopy.targetApp == interactedApp)
            {
                MissionDone();
            }
        }
    }


    private void MissionDone()
    {
        this.missionCopy.missionStatus = MissionStatus.COMPLETED;
        LevelManager.Instance.OnMissionFinished(this.missionCopy);

        if (this.missionCheckBox != null)
        {
            if (checkedBox != null)
            {
                this.missionCheckBox.sprite = checkedBox;
                this.missionCheckBox.GetComponent<RectTransform>().sizeDelta = new Vector2(60f, 60f);
            }
        }
    }
}
