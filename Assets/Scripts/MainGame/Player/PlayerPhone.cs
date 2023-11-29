using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerPhone : MonoBehaviour
{
    private string chosenCourse = "BS Information Technology"; //Temporary variable
    private float totalSavings = 100; //Temporary variable - Total Bank Savings
    private float totalEmergencyFunds = 1000; //Temporary variable
    private float totalCash = 5000; //Temporary variable
    private float currentIncomePerHour = 20; //Temporary variable
    private float taxRate = 0.5f; //Temporary variable
    private float totalDebt = 200; //Temporary variable
    private float outflowPerMonth; //Temporary variable - Rent + Water & Elec Bill
    private float rentRate = 1500; //Temporary variable
    private float waterBillRate = 200; //Temporary variable
    private float electricityBillRate = 250; //Temporary variable
    private float groceryPrice = 200; //Temporary variable
    
    [SerializeField] private GameObject goalTrackerOverlay;
    [SerializeField] private GameObject financeTrackerOverlay;
    [SerializeField] private GameObject phoneBookOverlay;
    [SerializeField] private GameObject oLShopOverlay;
    [SerializeField] private TextMeshProUGUI courseValue;
    [SerializeField] private TextMeshProUGUI savingsValue;
    [SerializeField] private TextMeshProUGUI bankBalValue;
    [SerializeField] private TextMeshProUGUI emergencyFundsValue;
    [SerializeField] private TextMeshProUGUI cashBalValue;
    [SerializeField] private TextMeshProUGUI incomeValue;
    [SerializeField] private TextMeshProUGUI taxValue;
    [SerializeField] private TextMeshProUGUI debtValue;
    [SerializeField] private TextMeshProUGUI monthlyOutflowValue;
    [SerializeField] private TextMeshProUGUI groceryPriceValue;
    [SerializeField] private Slider groceryBar;
    [SerializeField] private GameObject callBtn;
    [SerializeField] private TextMeshProUGUI AINameValue;


    public void GoalTracker()
    {
        goalTrackerOverlay.SetActive(true);
        courseValue.text = chosenCourse;
        savingsValue.text = "₱" + totalSavings.ToString() + "/100000";
        LevelManager.onFinishedPlayerAction(MissionType.USEAPP, interactedApp:APPS.GOALTRACKER);
    }


    public void FinanceTracker()
    {
        financeTrackerOverlay.SetActive(true);
        bankBalValue.text = "₱" + totalSavings.ToString();
        emergencyFundsValue.text = "₱" + totalEmergencyFunds.ToString();
        cashBalValue.text = "₱" + totalCash.ToString();
        incomeValue.text = currentIncomePerHour.ToString() + "/hr";
        taxValue.text = taxRate.ToString() + "%";
        debtValue.text = "₱" + totalDebt.ToString();
        monthlyOutflowValue.text = "₱" + (rentRate + waterBillRate + electricityBillRate).ToString();
        LevelManager.onFinishedPlayerAction(MissionType.USEAPP, interactedApp:APPS.FINANCETRACKER);
    }


    public void OLShop()
    {
        oLShopOverlay.SetActive(true);
        groceryPriceValue.text = "₱" + groceryPrice.ToString();
        LevelManager.onFinishedPlayerAction(MissionType.USEAPP, interactedApp:APPS.OLSHOP);
    }


    public void BuyGrocery()
    {
        groceryBar.value += 10;
        // Player.Instance.Purchase(false, 200f);
    }


    public void PhoneBook()
    {
        phoneBookOverlay.SetActive(true); 
        LevelManager.onFinishedPlayerAction(MissionType.USEAPP, interactedApp:APPS.PHONEBOOK);
    }


    public void SelectAIName()
    {
        callBtn.SetActive(true);
    }
}
