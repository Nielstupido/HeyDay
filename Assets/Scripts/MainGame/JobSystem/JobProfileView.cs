using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JobProfileView : MonoBehaviour
{
    [SerializeField] private GameObject jobSystemOverlay;
    [SerializeField] private GameObject resignationOverlay;
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private TextMeshProUGUI playerPositionText;
    [SerializeField] private TextMeshProUGUI playerEducationText;
    [SerializeField] private TextMeshProUGUI playerCompanyText;
    [SerializeField] private TextMeshProUGUI playerTotalWHtext;
    [SerializeField] private TextMeshProUGUI playerCurrentWHLabel;
    [SerializeField] private TextMeshProUGUI playerCurrentWHtext;
    [SerializeField] private TextMeshProUGUI playerSalaryPerHr;
    [SerializeField] private TextMeshProUGUI workHrsText;
    private float workHrs;
    private Player currentPlayer;


    public void SetupJobProfileView()
    {
        jobSystemOverlay.SetActive(true);
        this.gameObject.SetActive(true);

        currentPlayer = Player.Instance;

        playerNameText.text = currentPlayer.PlayerName;
        playerPositionText.text = currentPlayer.CurrentPlayerJob.jobPosName;
        playerEducationText.text = GameManager.Instance.EnumStringParser(currentPlayer.PlayerEnrolledCourse);
        playerCompanyText.text = currentPlayer.CurrentPlayerJob.establishment.ToString();
        playerTotalWHtext.text = currentPlayer.GetTotalWorkHours().ToString() + "hrs";
        playerCurrentWHLabel.text = "Current Work Hours [" + GameManager.Instance.EnumStringParser(currentPlayer.CurrentPlayerJob.workField) +"]";
        playerCurrentWHtext.text = currentPlayer.CurrentWorkHours.ToString() + "hrs";
        playerCurrentWHtext.text = "₱" + currentPlayer.CurrentPlayerJob.salaryPerHr.ToString();
    }


    public void StartWorkShift()
    {
        workHrs = float.Parse(workHrsText.text);
        StartCoroutine(WorkingAnim(workHrs));
        TimeManager.Instance.AddClockTime(workHrs);
        LevelManager.onFinishedPlayerAction(MissionType.WORKHR, workHrs);
        JobManager.Instance.WorkShiftFinished(workHrs);
    }


    private IEnumerator WorkingAnim(float waitingTime)
    {
        AnimOverlayManager.Instance.StartAnim(Animations.WORKING);
        yield return new WaitForSeconds(waitingTime);
        this.gameObject.SetActive(false);
        jobSystemOverlay.SetActive(false);
        AnimOverlayManager.Instance.StopAnim();
        yield return null;
    }


    public void Resign()
    {
        resignationOverlay.SetActive(true);
    }


    public void ConfirmResign()
    {
        StartCoroutine(ResigningAnim(workHrs));
        JobManager.Instance.Resign();
    }


    private IEnumerator ResigningAnim(float waitingTime)
    {
        AnimOverlayManager.Instance.StartAnim(Animations.RESIGNING);
        yield return new WaitForSeconds(waitingTime);
        resignationOverlay.SetActive(false);
        jobSystemOverlay.SetActive(false);
        AnimOverlayManager.Instance.StopAnim();
        yield return null;
    }


    public void IncrementWorkHrs()
    {
        workHrs = float.Parse(workHrsText.text);
        if (workHrs < 10f)
        {
            workHrsText.text = (++workHrs).ToString();
        }
    }

    
    public void DecrementWorkHrs()
    {
        workHrs = float.Parse(workHrsText.text);
        if (workHrs > 1f)
        {
            workHrsText.text = (--workHrs).ToString();
        }
    }
}
