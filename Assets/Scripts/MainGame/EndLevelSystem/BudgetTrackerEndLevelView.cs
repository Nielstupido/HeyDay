using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class BudgetTrackerEndLevelView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI budgetBillsText;
    [SerializeField] private TextMeshProUGUI budgetSavingsText;
    [SerializeField] private TextMeshProUGUI budgetConsumablesText;
    [SerializeField] private TextMeshProUGUI budgetEmergencyFundText;
    [SerializeField] private TextMeshProUGUI budetTotal;

    [SerializeField] private TextMeshProUGUI realityBillsText;
    [SerializeField] private TextMeshProUGUI realitySavingsText;
    [SerializeField] private TextMeshProUGUI realityConsumablesText;
    [SerializeField] private TextMeshProUGUI realityEmergencyFundText;
    [SerializeField] private TextMeshProUGUI realityTotal;


    public void PrepareEndLvlBudgetView(float[] budgetVals, float[] realityVals)
    {
        budgetBillsText.text = budgetVals[0].ToString();
        budgetSavingsText.text = budgetVals[1].ToString();
        budgetConsumablesText.text = budgetVals[2].ToString();
        budgetEmergencyFundText.text = budgetVals[3].ToString();
        budetTotal.text = budgetVals[4].ToString();

        realityBillsText.text = realityVals[0].ToString();
        realitySavingsText.text = realityVals[1].ToString();
        realityConsumablesText.text = realityVals[2].ToString();
        realityEmergencyFundText.text = realityVals[3].ToString();
        realityTotal.text = realityVals[4].ToString();

        this.gameObject.SetActive(true);
    }


    public void Continue()
    {
        AudioManager.Instance.PlaySFX("Select");
        this.gameObject.SetActive(false);
        OverlayAnimations.Instance.CloseOverlayAnim(this.gameObject);
        EndLevelManager.Instance.OpenBadgeAwardView();
    }
}
