using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BudgetSystem : MonoBehaviour
{
    [SerializeField] private BudgetTrackerEndLevelView budgetTrackerEndLevelView;

    //Budget values
    private float currentBillsBudget = 0f;
    private float currentSavingsBudget = 0f;
    private float currentConsumablesBudget = 0f;
    private float currentEmergencyBudget = 0f;
    public float CurrentBillsBudget {set{currentBillsBudget = value;} get{return currentBillsBudget;}}
    public float CurrentSavingsBudget {set{currentSavingsBudget = value;} get{return currentSavingsBudget;}}
    public float CurrentConsumablesBudget {set{currentConsumablesBudget = value;} get{return currentConsumablesBudget;}}
    public float CurrentEmergencyBudget {set{currentEmergencyBudget = value;} get{return currentEmergencyBudget;}}

    //local var
    private float playerIniPoints;
    private float playerLvlSavingsTotal;
    private float playerCurrentFinanceBudgetTotal;
    private float totalCurrentBudget;
    public static BudgetSystem Instance { get; private set; }


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
        
        GameManager.onGameStarted += LoadGameData;
        GameManager.onSaveGameStateData += SaveGameData;
    }


    private void OnDestroy()
    {
        GameManager.onGameStarted -= LoadGameData;
        GameManager.onSaveGameStateData -= SaveGameData;
    }


    private void LoadGameData()
    {
        if (this.currentBillsBudget == 0f && this.currentSavingsBudget == 0f && this.currentConsumablesBudget == 0f && this.currentEmergencyBudget == 0f)
        {
            this.currentBillsBudget = GameManager.Instance.CurrentGameStateData.currentBillsBudget;
            this.currentSavingsBudget = GameManager.Instance.CurrentGameStateData.currentSavingsBudget;
            this.currentConsumablesBudget = GameManager.Instance.CurrentGameStateData.currentConsumablesBudget;
            this.currentEmergencyBudget = GameManager.Instance.CurrentGameStateData.currentEmergencyBudget;
        }
    }


    private void SaveGameData()
    {
        GameManager.Instance.CurrentGameStateData.currentBillsBudget = this.currentBillsBudget;
        GameManager.Instance.CurrentGameStateData.currentSavingsBudget = this.currentSavingsBudget;
        GameManager.Instance.CurrentGameStateData.currentConsumablesBudget = this.currentConsumablesBudget;
        GameManager.Instance.CurrentGameStateData.currentEmergencyBudget = this.currentEmergencyBudget;
    }


    public void SaveBudget(float billsBudget, float savingsBudget, float consumablesBudget, float emergencyBudget)
    {
        currentBillsBudget = billsBudget;
        currentSavingsBudget = savingsBudget;
        currentConsumablesBudget = consumablesBudget;
        currentEmergencyBudget = emergencyBudget;
        totalCurrentBudget = billsBudget + savingsBudget + consumablesBudget + emergencyBudget;
    }


    public BadgeAwards AnalyzeBudgetResult()
    {
        playerIniPoints = 0f;

        //Bills
        playerIniPoints += GetResPoints(false, (Player.Instance.PlayerLvlBillExpenses / currentBillsBudget));

        //Savings & Emergency Fund
        playerLvlSavingsTotal = Player.Instance.PlayerLvlSavings + Player.Instance.PlayerLvlEmergencyFunds;
        playerCurrentFinanceBudgetTotal = currentSavingsBudget + currentEmergencyBudget;
        playerIniPoints += GetResPoints(true, (playerLvlSavingsTotal / playerCurrentFinanceBudgetTotal));

        //Bills
        playerIniPoints += GetResPoints(false, (Player.Instance.PlayerLvlConsumablesExpenses / currentConsumablesBudget));

        switch (playerIniPoints)
        {
            case 9f:
                GameDataManager.Instance.SavePlayerRecords(Player.Instance.PlayerName, 100);
                return BadgeAwards.GOLD;
            case > 5f:
                GameDataManager.Instance.SavePlayerRecords(Player.Instance.PlayerName, 50);
                return BadgeAwards.SILVER;
            case < 6f:
                GameDataManager.Instance.SavePlayerRecords(Player.Instance.PlayerName, 25);
                return BadgeAwards.BRONZE;
            default:
                return BadgeAwards.NA;
        }
    }


    public void ResetBudget()
    {
        currentBillsBudget = 0f;
        currentSavingsBudget = 0f;
        currentConsumablesBudget = 0f;
        currentEmergencyBudget = 0f;
        totalCurrentBudget = 0f;
    }


    public float[] GetCurrentBudgetList()
    {
        return new float[] {currentBillsBudget, currentSavingsBudget, currentConsumablesBudget, currentEmergencyBudget, totalCurrentBudget};
    }


    public float[] GetCurrentExpensesList()
    {
        return new float[] {Player.Instance.PlayerLvlBillExpenses, Player.Instance.PlayerLvlSavings, Player.Instance.PlayerLvlConsumablesExpenses, 
                        Player.Instance.PlayerLvlEmergencyFunds, Player.Instance.GetLvlTotalExpenses()};
    }



    private float GetResPoints(bool isSavingsEmergency, float percentageResult)
    {
        switch (percentageResult)
        {
            case > 1f:
                if (isSavingsEmergency)
                {
                    return 3f; //vgood
                }
                return 1f; //bad

            case > 0.6f:
                return 2f; //good

            case < 0.61f:
                if (isSavingsEmergency)
                {
                    return 1f; //bad
                }
                return 3f; //vgood

            default:
                return 0f;
        }
    }
}
