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
    private JobPositions jobPositionData;


    public void PrepareJobDets(JobPositions jobData)
    {
        this.jobPositionData = jobData;
        jobTitleText.text = this.jobPositionData.jobPosName;
        jobReqsText.text = "Qualifications : " + this.jobPositionData.jobPosReqs;
        jobSalary.text = this.jobPositionData.salaryPerHr.ToString() + "per hour";
        this.gameObject.GetComponent<Button>().onClick.AddListener( () => {JobSystemManager.Instance.ShowSelectedJobPosition(this.jobPositionData);} );
    }
}
