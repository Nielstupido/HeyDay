using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;


public enum JobFields
{
    NONE,
    ASSISTANTNURSE,
    ASSISTANTMIDWIFE,
    ASSISTANTMEDTECH,
    ASSISTANTRADTECH,
    ASSISTANTPHARMACIST,
    ASSISTANTRESEARCHLAB,
    OFFICEASSISTANT,
    HRASSISTANT,
    ITASSISTANT,
    ASSISTANTENGINEER,
    ASSISTANTARCHITECT,
    ASSISTANTTEACHER,
    ASSISTANTMANAGER,
    BOOKKEEPER,
    ASSISTANTPROGRAMMER,
    ASSISTANTDATAANALYST,
    DATAANALYST,
    ASSISTANTSALESREP,
    ASSISTANTMECHANIC,
    SERVICESTAFF,
    CALLCENTERAGENT
}


public class JobSystemManager : MonoBehaviour
{
    [SerializeField] private GameObject jobSystemOverlay;
    [SerializeField] private Transform availableJobPositionsHolder;
    [SerializeField] private GameObject jobDetailedViewOverlay;
    [SerializeField] private GameObject jobPositionPrefab;
    [SerializeField] private Prompts notQualifiedPrompt;
    [SerializeField] private GameObject qualifiedMsgOverlay;
    [SerializeField] private TextMeshProUGUI congratulatoryMsg;
    [SerializeField] private List<JobPositions> hospitalJobPositions = new List<JobPositions>();
    [SerializeField] private List<JobPositions> cityHallJobPositions = new List<JobPositions>();
    [SerializeField] private List<JobPositions> universityJobPositions = new List<JobPositions>();
    [SerializeField] private List<JobPositions> fireStationJobPositions = new List<JobPositions>();
    [SerializeField] private List<JobPositions> cafeteriaJobPositions = new List<JobPositions>();
    [SerializeField] private List<JobPositions> policeStationJobPositions = new List<JobPositions>();
    [SerializeField] private List<JobPositions> wasteMngmntFacilityJobPositions = new List<JobPositions>();
    [SerializeField] private List<JobPositions> waterDistrictJobPositions = new List<JobPositions>();
    [SerializeField] private List<JobPositions> bankJobPositions = new List<JobPositions>();
    [SerializeField] private List<JobPositions> cafeJobPositions = new List<JobPositions>();
    [SerializeField] private List<JobPositions> foodXpressJobPositions = new List<JobPositions>();
    [SerializeField] private List<JobPositions> beeUsolutionsJobPositions = new List<JobPositions>();
    [SerializeField] private List<JobPositions> autoDealerJobPositions = new List<JobPositions>();
    [SerializeField] private List<JobPositions> convenienceStoreJobPositions = new List<JobPositions>();
    [SerializeField] private List<JobPositions> factoryJobPositions = new List<JobPositions>();
    [SerializeField] private List<JobPositions> callCenterJobPositions = new List<JobPositions>();
    [SerializeField] private List<JobPositions> electricCompanyJobPositions = new List<JobPositions>();
    private Player Player;
    private bool isHalfQuali;
    private List<JobPositions> jobPositionsList = new List<JobPositions>();

    public static JobSystemManager Instance {private set; get;}


    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }


    public void ShowAvailablePositons(Building currentBuilding)
    {
        jobSystemOverlay.SetActive(true);
        jobPositionsList = JobPositionsList(currentBuilding.buildingEnumName);

        foreach(JobPositions jobPosition in jobPositionsList)
        {
            GameObject newJobPositionObj = Instantiate(jobPositionPrefab, Vector3.zero, Quaternion.identity, availableJobPositionsHolder);
            JobPositionObj newJobPosition = newJobPositionObj.GetComponent<JobPositionObj>();
            newJobPosition.PrepareJobDets(jobPosition);
        }
    }


    public void ShowSelectedJobPosition(JobPositions selectedJobPosition)
    {
        jobDetailedViewOverlay.GetComponent<JobDetailedViewObj>().PrepareDetailedJobDets(selectedJobPosition);
        jobDetailedViewOverlay.SetActive(true);
    }


    public void ApplyJob(JobPositions newJobData)
    {
        Player = Player.Instance;

        if (IsPlayerQualified(newJobData))
        {
            qualifiedMsgOverlay.SetActive(true);

            congratulatoryMsg.text = ("Congratulations on applying for the " + newJobData.jobPosName + " position at " + 
                                        BuildingManager.Instance.CurrentSelectedBuilding.buildingStringName + "! Wishing you the best of luck in this exciting opportunity.");

            if (Player.CurrentPlayerJob != null)
            {
                if (Player.CurrentPlayerJob.workField != newJobData.workField)
                {
                    if (Player.PlayerWorkFieldHistory.ContainsKey(Player.CurrentPlayerJob.workField))
                    {
                        Player.PlayerWorkFieldHistory[Player.CurrentPlayerJob.workField] = Player.CurrentWorkHours;
                    }
                    else
                    {
                        Player.PlayerWorkFieldHistory.Add(Player.CurrentPlayerJob.workField, Player.CurrentWorkHours);
                    }

                    if (Player.PlayerWorkFieldHistory.ContainsKey(newJobData.workField))
                    {
                        Player.CurrentWorkHours = Player.PlayerWorkFieldHistory[newJobData.workField];
                    }
                    else
                    {
                        Player.CurrentWorkHours = 0;
                    }
                }
            }

            Player.CurrentPlayerJob = newJobData;
            //game save
        }
        else
        {
            PromptManager.Instance.ShowPrompt(notQualifiedPrompt);
        }
    }


    private bool IsPlayerQualified(JobPositions jobData)
    {
        Player = Player.Instance;
        isHalfQuali = false;

        if (jobData.reqCourse.Count > 0)
        {
            if (Player.PlayerEnrolledCourse == UniversityCourses.NONE && jobData.reqStudyField == StudyFields.NONE)
            {
                return false; //if player is not yet enrolled to any course
            }

            if (!jobData.reqCourse.Contains(UniversityCourses.ANY))
            {
                if (!jobData.reqCourse.Contains(Player.PlayerEnrolledCourse) && jobData.reqStudyField == StudyFields.NONE)
                {
                    return false; //if player's course doesn't meet the job's required course 
                }
            }
            isHalfQuali = true;
        }

        if (!isHalfQuali && jobData.reqStudyField != StudyFields.NONE)
        {
            if (jobData.reqStudyField != Player.PlayerEnrolledStudyField)
            {
                return false; //if player's study field doesn't meet the job's required study field 
            }
        }

        if (jobData.reqWorkHrs > 0f)
        {
            if (jobData.reqWorkField != JobFields.NONE)
            {
                if (Player.CurrentPlayerJob.workField == jobData.reqWorkField)
                {
                    if (jobData.reqWorkHrs > Player.CurrentWorkHours) 
                    {
                        return false; //if player doesn't have "enough" experience of the required work field
                    }
                }
                else
                {
                    if (Player.PlayerWorkFieldHistory.ContainsKey(jobData.reqWorkField))
                    {
                        if (jobData.reqWorkHrs > Player.PlayerWorkFieldHistory[jobData.reqWorkField])
                        {
                            return false; //if player doesn't have "enough" experience of the required work field
                        }
                    }
                    else
                    {
                        return false; //if player doesn't have "any" experience of the required work field
                    }
                }
            }
            else
            {
                if (jobData.reqWorkHrs > Player.GetTotalWorkHours())
                {
                    return false;
                }
            }
        }

        return true;
    }


    private List<JobPositions> JobPositionsList(Buildings currentBuildingName)
    {
        switch (currentBuildingName)
        {
            case Buildings.HOSPITAL:
                return hospitalJobPositions;
            case Buildings.CITYHALL:
                return cityHallJobPositions;
            case Buildings.UNIVERSITY:
                return universityJobPositions;
            case Buildings.FIRESTATION:
                return fireStationJobPositions;
            case Buildings.CAFETERIA:
                return cafeteriaJobPositions;
            case Buildings.POLICESTATION:
                return policeStationJobPositions;
            case Buildings.WASTEFACILITY:
                return wasteMngmntFacilityJobPositions;
            case Buildings.WATERDISTRICT:
                return waterDistrictJobPositions;
            case Buildings.BANK:
                return bankJobPositions;
            case Buildings.CAFE:
                return cafeJobPositions;
            case Buildings.FOODXPRESS:
                return foodXpressJobPositions;
            case Buildings.BEEUSOLUTIONS:
                return beeUsolutionsJobPositions;
            case Buildings.AUTODEALER:
                return autoDealerJobPositions;
            case Buildings.CONVENIENCESTORE:
                return convenienceStoreJobPositions;
            case Buildings.FACTORY:
                return factoryJobPositions;
            case Buildings.CALLCENTER:
                return callCenterJobPositions;
            case Buildings.ELECTRICCOOP:
                return electricCompanyJobPositions;
            default:
                return new List<JobPositions>();
        }
    }
}
