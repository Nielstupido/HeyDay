using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


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
    ASSISTANTMECHANIC
}


public class JobSystemManager : MonoBehaviour
{
    [SerializeField] private GameObject jobSystemOverlay;
    [SerializeField] private Transform availableJobPositionsHolder;
    [SerializeField] private GameObject jobDetailedViewOverlay;
    [SerializeField] private GameObject jobPositionPrefab;
    [SerializeField] private Prompts notQualifiedPrompt;
    [SerializeField] private GameObject qualifiedMsgOverlay;
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


    public void ApplyJob(JobPositions jobData)
    {
        if (IsPlayerQualified(jobData))
        {
            qualifiedMsgOverlay.SetActive(true);
        }
        else
        {
            PromptManager.Instance.ShowPrompt(notQualifiedPrompt);
        }
    }


    private bool IsPlayerQualified(JobPositions jobData)
    {
        if (jobData.reqWorkField != JobFields.NONE && jobData.reqWorkField != Player.Instance.PlayerJobField)
        {
            return false;
        }

        if (jobData.reqStudyField != StudyFields.NONE && jobData.reqStudyField != Player.Instance.PlayerEnrolledStudyField)
        {
            return false;
        }

        if (jobData.reqWorkHrs > Player.Instance.CurrentWorkHours)
        {
            return false;
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
