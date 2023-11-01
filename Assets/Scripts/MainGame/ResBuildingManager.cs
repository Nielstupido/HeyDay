using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
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
    [SerializeField] private GameObject btnPrefab;
    [SerializeField] private Transform buttonsHolder;
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
    public delegate void OnResBuildingBtnClicked(Buttons clickedBtn);
    public OnResBuildingBtnClicked onResBuildingBtnClicked;
    public static ResBuildingManager Instance { get; private set; }


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


    private void Start()
    {
        enterBtn.GetComponent<Button>().onClick.AddListener(delegate { EnterRoom(currentSelectedResBuilding); });
        rentBtn.GetComponent<Button>().onClick.AddListener(delegate { Rent(currentSelectedResBuilding); });
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


    public void Rent(ResBuilding selectedBuilding)
    {
        stayCount = 0;
        Player.Instance.CurrentPlayerPlace = selectedBuilding;
        EnterRoom(selectedBuilding);
    }


    public void EnterRoom(ResBuilding selectedBuilding)
    {
        resBuildingSelectOverlay.SetActive(false);
        roomBgOverlay.SetActive(true);
        PrepareButtons(selectedBuilding);
        Debug.Log(selectedBuilding.actionButtons);
    }


    private void PrepareButtons(ResBuilding selectedBuilding)
    {
        foreach(Buttons btn in selectedBuilding.actionButtons)
        {
            GameObject newBtn = Instantiate(btnPrefab, Vector3.zero, Quaternion.identity, buttonsHolder);
            newBtn.GetComponent<Image>().sprite = BuildingManager.Instance.ButtonImages[((int)btn)];
            newBtn.GetComponent<Button>().onClick.AddListener(delegate { BuildingManager.Instance.onBuildingBtnClicked(btn); });
        }
    }
}