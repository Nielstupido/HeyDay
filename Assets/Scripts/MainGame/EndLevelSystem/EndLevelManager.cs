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
        endLvlOverlay.SetActive(true);
        OpenBudetTrackerLevelView();
        //save game data
    }


    public void OpenLeaderboardView()
    {
        Dictionary<string, int> playerRecords =  GameDataManager.Instance.GetCurrentLevelScores();
        lvlLeaderboardView.SetupLeaderboard(playerRecords);
    }


    public void OpenBadgeAwardView()
    {
        badgeAwardView.PrepareBadgeAwardView(BudgetSystem.Instance.AnalyzeBudgetResult());
    }


    private void OpenBudetTrackerLevelView()
    {
        budgetTrackerEndLevelView.PrepareEndLvlBudgetView(BudgetSystem.Instance.GetCurrentBudgetList(), BudgetSystem.Instance.GetCurrentExpensesList());
    }
}
