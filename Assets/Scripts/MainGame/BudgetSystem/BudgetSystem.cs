using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BudgetSystem : MonoBehaviour
{
    private float currentBillsBudget;
    private float currentSavingsBudget;
    private float currentConsumablesBudget;
    private float currentEmergencyBudget;
    public float CurrentBillsBudget {set{currentBillsBudget = value;} get{return currentBillsBudget;}}
    public float CurrentSavingsBudget {set{currentSavingsBudget = value;} get{return currentSavingsBudget;}}
    public float CurrentConsumablesBudget {set{currentConsumablesBudget = value;} get{return currentConsumablesBudget;}}
    public float CurrentEmergencyBudget {set{currentEmergencyBudget = value;} get{return currentEmergencyBudget;}}
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
    }
}
