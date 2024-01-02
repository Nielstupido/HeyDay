using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public enum BadgeAwards
{
    NA,
    BRONZE,
    SILVER,
    GOLD
}


public class EndLevelManager : MonoBehaviour
{
    [SerializeField] private GameObject endLvlOverlay;
    [SerializeField] private GameObject budgetTrackerPopUp;
    [SerializeField] private LvlLeaderboardView lvlLeaderboardView;
    [SerializeField] private BudgetTrackerEndLevelView budgetTrackerEndLevelView;
    [SerializeField] private BadgeAwardView badgeAwardView;
    public static EndLevelManager Instance { get; private set; }


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


    public void LevelFinished()
    {
        LevelManager.Instance.CameraMovementRef.enabled = false;
        GameManager.Instance.CurrentGameLevel++;
        endLvlOverlay.SetActive(true);
        OverlayAnimations.Instance.AnimOpenOverlay(endLvlOverlay);
        OverlayAnimations.Instance.AnimOpenOverlay(budgetTrackerPopUp);
        OpenBudetTrackerLevelView();
        //save game data

        if (GameManager.Instance.CurrentGameLevel == 11 || GameManager.Instance.CurrentGameLevel == 21  || GameManager.Instance.CurrentGameLevel == 31)
        {
            Player.Instance.IncrementAge();
        }
    }


    public void NextLevel()
    {
        endLvlOverlay.SetActive(false);
        OverlayAnimations.Instance.CloseOverlayAnim(endLvlOverlay);

        if (GameManager.Instance.CurrentGameLevel == 41)
        {
            AnimOverlayManager.Instance.StartWhiteScreenFadeLoadScreen();
            StartCoroutine(ProceedGoodEnding());
        }
        else
        {
            LevelMapManager.Instance.MoveToNewLevel(GameManager.Instance.CurrentGameLevel, false);
        }
    }


    public void OpenLeaderboardView(Sprite playerBadge)
    {
        Dictionary<string, int> playerRecords =  GameDataManager.Instance.GetCurrentLevelScores();
        lvlLeaderboardView.SetupLeaderboard(playerRecords, playerBadge);
    }


    public void OpenBadgeAwardView()
    {
        badgeAwardView.PrepareBadgeAwardView(BudgetSystem.Instance.AnalyzeBudgetResult());
    }


    private void OpenBudetTrackerLevelView()
    {
        budgetTrackerEndLevelView.PrepareEndLvlBudgetView(BudgetSystem.Instance.GetCurrentBudgetList(), BudgetSystem.Instance.GetCurrentExpensesList());
    }


    private IEnumerator ProceedGoodEnding()
    {
        yield return new WaitForSeconds(1f);
        GameManager.Instance.GameFinished();
        yield return null;
    }
}
