using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResBuildingManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI monthlyRentText;
    [SerializeField] private TextMeshProUGUI monthlyElecText;
    [SerializeField] private TextMeshProUGUI monthlyWaterText;
    [SerializeField] private TextMeshProUGUI dailyHappinessAdtnlText;
    [SerializeField] private TextMeshProUGUI resBuildingName;
    [SerializeField] private GameObject rentBtn;
    [SerializeField] private GameObject enterBtn;
    [SerializeField] private GameObject resBuildingSelectOverlay;
    [SerializeField] private GameObject roomBgOverlay;
    private ResBuilding currentSelectedResBuilding;
    private int stayCount;
    private float totalBilling;
    public GameObject ResBuildingSelectOverlay { get{return resBuildingSelectOverlay;}}
    public ResBuilding CurrentSelectedResBuilding { set{currentSelectedResBuilding = value; ShowBtn();} get{return currentSelectedResBuilding;}}
    public string MonthlyRentText {set{monthlyRentText.text = "Monthly Rent : ₱" + value;}}
    public string MonthlyElecText {set{monthlyElecText.text = "Monthly Electricity Charge : ₱" + value;}}
    public string MonthlyWaterText {set{monthlyWaterText.text = "Monthly Water Charge : ₱" + value;}}
    public string DailyHappinessAdtnlText {set{dailyHappinessAdtnlText.text = "+" + value + " Happiness / Day";}}
    public string ResBuildingName {set{resBuildingName.text = value;}}


    private void Start()
    {
        TimeManager.onDayAdded += ComputeBillings;
        stayCount = 0;
        totalBilling = 0;
    }


    private void ShowBtn()
    {
        if(Player.Instance.CurrentPlayerPlace == currentSelectedResBuilding)
        {
            rentBtn.SetActive(false);
            enterBtn.SetActive(true);
        }
        else
        {
            rentBtn.SetActive(true);
            enterBtn.SetActive(false);
        }
    }


    private void ComputeBillings(float currentDayCount)
    {
        stayCount++;
        totalBilling = 0;

        float temp = stayCount / 30;
        if (Mathf.Approximately(temp, Mathf.RoundToInt(temp)))
        {
            totalBilling = Player.Instance.CurrentPlayerPlace.monthlyElecCharge + Player.Instance.CurrentPlayerPlace.monthlyRent + Player.Instance.CurrentPlayerPlace.monthlyWaterCharge;
            if (Player.Instance.PlayerCash > totalBilling)
            {
                Player.Instance.PlayerCash = Player.Instance.PlayerCash - totalBilling;
            }
            else if (Player.Instance.PlayerBankSavings > totalBilling)
            {
                Player.Instance.PlayerBankSavings = Player.Instance.PlayerBankSavings - totalBilling;
            }
            else
            {
                //player will be kicked out from the current apartment
            }
        }
    }


    public void Rent()
    {
        stayCount = 0;
        Player.Instance.CurrentPlayerPlace = currentSelectedResBuilding;
        EnterRoom();
    }


    public void EnterRoom()
    {
        roomBgOverlay.SetActive(true);
    }
}