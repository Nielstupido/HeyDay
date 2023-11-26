using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class JobDetailedViewObj : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI buildingName;
    [SerializeField] private TextMeshProUGUI jobTitleText;
    [SerializeField] private TextMeshProUGUI jobReqsText;
    [SerializeField] private TextMeshProUGUI jobSalary;
    [SerializeField] private Button applyBtn;
    private JobPositions jobPositionData;


    public void PrepareDetailedJobDets(JobPositions jobData)
    {
        jobPositionData = jobData;
        buildingName.text = BuildingManager.Instance.CurrentSelectedBuilding.buildingStringName;
        jobTitleText.text = this.jobPositionData.jobPosName;
        jobReqsText.text = this.jobPositionData.jobPosReqs;
        jobSalary.text = this.jobPositionData.salaryPerHr.ToString();
        applyBtn.onClick.AddListener(Apply);
    }


    public void Apply()
    {
        JobApplicationManager.Instance.ApplyJob(jobPositionData);
    }
}
