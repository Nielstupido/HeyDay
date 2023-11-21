using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobManager : MonoBehaviour
{
    [SerializeField] private JobProfileView jobProfileView;
    private float unpaidWorkHrs;
    private float totalWorkHrs;
    private bool isGettingSalary;
    public static JobManager Instance {private set; get;}


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


    private void Start()
    {
        unpaidWorkHrs = 0;
    }


    public void Work()
    {
        jobProfileView.SetupJobProfileView();
    }


    public void Apply(Building currentBuilding)
    {
        JobApplicationManager.Instance.ShowAvailablePositons(currentBuilding);
    }


    public void WorkShiftFinished(float workHrs)
    {
        isGettingSalary = false;
        totalWorkHrs = unpaidWorkHrs + workHrs;

        if (totalWorkHrs >= 4)
        {
            isGettingSalary = true;
            unpaidWorkHrs = 0;
        }

        Player.Instance.Work(isGettingSalary, workHrs, totalWorkHrs);
    }


    public void Resign()
    {
        
    }
}
