using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class JobProfileView : MonoBehaviour
{
    [SerializeField] private GameObject jobSystemOverlay;
    [SerializeField] private GameObject resignationOverlay;
    [SerializeField] private Image playerProfileImage;
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private TextMeshProUGUI playerPositionText;
    [SerializeField] private TextMeshProUGUI playerEducationText;
    [SerializeField] private TextMeshProUGUI playerCompanyText;
    [SerializeField] private TextMeshProUGUI playerTotalWHtext;
    [SerializeField] private TextMeshProUGUI playerCurrentWHLabel;
    [SerializeField] private TextMeshProUGUI playerCurrentWHtext;
    [SerializeField] private TextMeshProUGUI playerSalaryPerHrText;
    [SerializeField] private TextMeshProUGUI workHrsText;
    private float workHrs;
    private Player currentPlayer;


    public void SetupJobProfileView()
    {
        jobSystemOverlay.SetActive(true);
        this.gameObject.SetActive(true);

        currentPlayer = Player.Instance;

        playerProfileImage.sprite = Player.Instance.CurrentCharacter.bustIcon;
        playerNameText.text = currentPlayer.PlayerName;
        playerPositionText.text = currentPlayer.CurrentPlayerJob.jobPosName;
        playerEducationText.text = GameManager.Instance.EnumStringParser(currentPlayer.PlayerEnrolledCourse);
        playerCompanyText.text = currentPlayer.CurrentPlayerJob.establishment.ToString();
        playerTotalWHtext.text = currentPlayer.GetTotalWorkHours().ToString() + "hrs";
        playerCurrentWHLabel.text = "Current Work Hours [" + GameManager.Instance.EnumStringParser(currentPlayer.CurrentPlayerJob.workField) +"]";
        playerCurrentWHtext.text = currentPlayer.CurrentWorkHours.ToString() + "hrs";
        playerSalaryPerHrText.text = "₱" + currentPlayer.CurrentPlayerJob.salaryPerHr.ToString();
    }


    public void StartWorkShift()
    {
        workHrs = float.Parse(workHrsText.text);
        TimeManager.Instance.AddClockTime(false, workHrs);
        StartCoroutine(WorkingAnim(workHrs));
    }


    private void WorkShiftDone()
    {
        LevelManager.onFinishedPlayerAction(MissionType.WORKHR, workHrs);
        JobManager.Instance.WorkShiftFinished(workHrs, this.gameObject, jobSystemOverlay);
    }


    private IEnumerator WorkingAnim(float waitingTime)
    {
        AnimOverlayManager.Instance.StartAnim(ActionAnimations.WORK);
        yield return new WaitForSeconds(waitingTime);
        AnimOverlayManager.Instance.StopAnim();
        WorkShiftDone();
        yield return null;
    }


    public void Resign()
    {
        jobSystemOverlay.SetActive(true);
        this.gameObject.SetActive(true);
        resignationOverlay.SetActive(true);
    }


    public void ConfirmResign()
    {
        StartCoroutine(ResigningAnim(2f));
    }


    private void ProceedResignation()
    {
        JobManager.Instance.ArrangeResignation(this.gameObject, jobSystemOverlay);
    }
    

    private IEnumerator ResigningAnim(float waitingTime)
    {
        AnimOverlayManager.Instance.StartAnim(ActionAnimations.RESIGN);
        yield return new WaitForSeconds(waitingTime);
        resignationOverlay.SetActive(false);
        AnimOverlayManager.Instance.StopAnim();
        ProceedResignation();
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
