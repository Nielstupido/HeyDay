using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JobPositionObj : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI jobTitleText;
    [SerializeField] private TextMeshProUGUI jobReqsText;
    [SerializeField] private TextMeshProUGUI jobSalary;
    [SerializeField] private TextMeshProUGUI moreDetsText;
    private JobPositions jobPositionData;


    public void PrepareJobDets(JobPositions jobData, string jobPosStat = null)
    {
        this.jobPositionData = jobData;
        jobTitleText.text = this.jobPositionData.jobPosName;
        jobReqsText.text = "Qualifications : " + this.jobPositionData.jobPosReqs;
        jobSalary.text = this.jobPositionData.salaryPerHr.ToString() + "per hour";
        if (jobPosStat != null)
        {
            moreDetsText.text = jobPosStat;
        }
        this.gameObject.GetComponent<Button>().onClick.AddListener( () => {JobApplicationManager.Instance.ShowSelectedJobPosition(this.jobPositionData);} );
    }
}
