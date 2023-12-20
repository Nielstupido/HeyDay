using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobManager : MonoBehaviour
{
    [SerializeField] private JobProfileView jobProfileView;
    [SerializeField] private Prompts salaryReceived;
    private Player currentPlayer;
    private float unpaidWorkHrs;
    private float totalWorkHrs;
    private bool isGettingSalary;
    public static JobManager Instance {private set; get;}


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    private void Start()
    {
        unpaidWorkHrs = 0;
    }


    private IEnumerator SalaryAnim()
    {
        TutorialManager.Instance.IsSalaryReceived = true;
        yield return new WaitForSeconds(0.2f);
        AnimOverlayManager.Instance.StartAnim(ActionAnimations.SALARY);
        yield return new WaitForSeconds(1f);
        AnimOverlayManager.Instance.StopAnim();
        PromptManager.Instance.ShowPrompt(salaryReceived);
        yield return null;
    }


    public void Work()
    {
        jobProfileView.SetupJobProfileView();
    }
    
    public void HideJobProfileView()
    {
        AudioManager.Instance.PlaySFX("Select");
        this.gameObject.SetActive(false);
        OverlayAnimations.Instance.CloseOverlayAnim(jobProfileView.gameObject);
    }


    public void Apply(Building currentBuilding)
    {
        JobApplicationManager.Instance.ShowAvailablePositons(currentBuilding);
    }


    public void WorkShiftFinished(float workHrs, GameObject jobProfileView, GameObject jobSystemOverlay)
    {
        jobProfileView.SetActive(false);
        jobSystemOverlay.SetActive(false);

        isGettingSalary = false;
        totalWorkHrs = unpaidWorkHrs + workHrs;

        if (totalWorkHrs >= 4)
        {
            isGettingSalary = true;
            unpaidWorkHrs = 0;
            StartCoroutine(SalaryAnim());
        }

        Player.Instance.Work(isGettingSalary, workHrs, totalWorkHrs);
    }


    public void QuitWork()
    {
        jobProfileView.Resign();
    }


    public void ArrangeJobAcceptance(JobPositions newJobData)
    {
        currentPlayer = Player.Instance;

        if (currentPlayer.CurrentPlayerJob != null)
        {
            if (currentPlayer.CurrentPlayerJob.workField != newJobData.workField)
            {
                if (currentPlayer.PlayerWorkFieldHistory.ContainsKey(currentPlayer.CurrentPlayerJob.workField))
                {
                    currentPlayer.PlayerWorkFieldHistory[currentPlayer.CurrentPlayerJob.workField] = currentPlayer.CurrentWorkHours;
                }
                else
                {
                    currentPlayer.PlayerWorkFieldHistory.Add(currentPlayer.CurrentPlayerJob.workField, currentPlayer.CurrentWorkHours);
                }

                if (currentPlayer.PlayerWorkFieldHistory.ContainsKey(newJobData.workField))
                {
                    currentPlayer.CurrentWorkHours = currentPlayer.PlayerWorkFieldHistory[newJobData.workField];
                }
                else
                {
                    currentPlayer.CurrentWorkHours = 0;
                }
            }
        }

        currentPlayer.CurrentPlayerJob = newJobData;
        BuildingManager.Instance.CurrentSelectedBuilding.currentlyHired = true;
        BuildingManager.Instance.PrepareButtons(BuildingManager.Instance.CurrentSelectedBuilding);
    }


    public void ArrangeResignation(GameObject jobProfileView, GameObject jobSystemOverlay)
    {
        jobProfileView.SetActive(false);
        jobSystemOverlay.SetActive(false);
        currentPlayer = Player.Instance;

        BuildingManager.Instance.CurrentSelectedBuilding.currentlyHired = false;
        BuildingManager.Instance.CurrentSelectedBuilding.actionButtons.RemoveAt(BuildingManager.Instance.CurrentSelectedBuilding.actionButtons.Count-1);
        BuildingManager.Instance.CurrentSelectedBuilding.actionButtons.RemoveAt(BuildingManager.Instance.CurrentSelectedBuilding.actionButtons.Count-2);
        
        if (currentPlayer.PlayerWorkFieldHistory.ContainsKey(currentPlayer.CurrentPlayerJob.workField))
        {
            currentPlayer.PlayerWorkFieldHistory[currentPlayer.CurrentPlayerJob.workField] = currentPlayer.CurrentWorkHours;
        }
        else
        {
            currentPlayer.PlayerWorkFieldHistory.Add(currentPlayer.CurrentPlayerJob.workField, currentPlayer.CurrentWorkHours);
        }

        currentPlayer.CurrentPlayerJob = null;
        BuildingManager.Instance.PrepareButtons(BuildingManager.Instance.CurrentSelectedBuilding);
    }
}
