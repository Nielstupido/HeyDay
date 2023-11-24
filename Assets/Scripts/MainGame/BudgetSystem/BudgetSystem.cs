using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BudgetSystem : MonoBehaviour
{
    [SerializeField] private BudgetTrackerEndLevelView budgetTrackerEndLevelView;

    //Budget values
    private float currentBillsBudget;
    private float currentSavingsBudget;
    private float currentConsumablesBudget;
    private float currentEmergencyBudget;
    public float CurrentBillsBudget {set{currentBillsBudget = value;} get{return currentBillsBudget;}}
    public float CurrentSavingsBudget {set{currentSavingsBudget = value;} get{return currentSavingsBudget;}}
    public float CurrentConsumablesBudget {set{currentConsumablesBudget = value;} get{return currentConsumablesBudget;}}
    public float CurrentEmergencyBudget {set{currentEmergencyBudget = value;} get{return currentEmergencyBudget;}}

    //local var
    private float playerIniPoints;
    private float playerLvlSavingsTotal;
    private float playerCurrentFinanceBudgetTotal;
    private float totalCurrentBudget;
    private Player currentPlayer;
    public static BudgetSystem Instance { get; private set; }


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
        currentPlayer = Player.Instance;
        playerIniPoints = 0f;

        //Bills
        playerIniPoints += GetResPoints(false, (currentPlayer.PlayerLvlBillExpenses / currentBillsBudget));

        //Savings & Emergency Fund
        playerLvlSavingsTotal = currentPlayer.PlayerLvlSavings + currentPlayer.PlayerLvlEmergencyFunds;
        playerCurrentFinanceBudgetTotal = currentSavingsBudget + currentEmergencyBudget;
        playerIniPoints += GetResPoints(true, (playerLvlSavingsTotal / playerCurrentFinanceBudgetTotal));

        //Bills
        playerIniPoints += GetResPoints(false, (currentPlayer.PlayerLvlConsumablesExpenses / currentConsumablesBudget));

        switch (playerIniPoints)
        {
            case 9f:
                return BadgeAwards.GOLD;
            case > 5f:
                return BadgeAwards.SILVER;
            case < 6f:
                return BadgeAwards.BRONZE;
            default:
                return BadgeAwards.NA;
        }
    }


    public void ShowLevelBudgetResult()
    {
        currentPlayer = Player.Instance;

        budgetTrackerEndLevelView.PrepareEndLvlBudgetView(
            new float[] {currentBillsBudget, currentSavingsBudget, currentConsumablesBudget, currentEmergencyBudget, totalCurrentBudget},
            new float[] {currentPlayer.PlayerLvlBillExpenses, currentPlayer.PlayerLvlSavings, currentPlayer.PlayerLvlConsumablesExpenses, currentPlayer.PlayerLvlEmergencyFunds, currentPlayer.GetLvlTotalExpenses()}
            );
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
