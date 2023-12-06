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
    [SerializeField] private GameObject miniBtnsHolder;
    [SerializeField] private GameObject btnPrefab;
    [SerializeField] private Transform buttonsHolder;
    [SerializeField] private Button vechiclesBtn;
    [SerializeField] private Button appliancesBtn;
    [SerializeField] private Button groceriesBtn;
    [SerializeField] private PlayerItemsListManager playerItemsListManager;
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
        vechiclesBtn.onClick.AddListener( () => {ShowItems(ItemType.VEHICLE);} );
        appliancesBtn.onClick.AddListener( () => {ShowItems(ItemType.APPLIANCE);} );
        groceriesBtn.onClick.AddListener( () => {ShowItems(ItemType.CONSUMABLE);} );
        enterBtn.GetComponent<Button>().onClick.AddListener( () => {EnterRoom(currentSelectedResBuilding);} );
        rentBtn.GetComponent<Button>().onClick.AddListener( () => {Rent(currentSelectedResBuilding);} );
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
        if (Player.Instance.CurrentPlayerPlace == null)
        {
            return;
        }
        
        stayCount++;
        totalBilling = 0;

        float temp = stayCount / 30;
        if (Mathf.Approximately(temp, Mathf.RoundToInt(temp)))
        {
            totalBilling = Player.Instance.CurrentPlayerPlace.monthlyElecCharge + Player.Instance.CurrentPlayerPlace.monthlyRent + Player.Instance.CurrentPlayerPlace.monthlyWaterCharge;
            if (Player.Instance.PlayerCash > totalBilling)
            {
                Player.Instance.PlayerCash = Player.Instance.PlayerCash - totalBilling;
                Player.Instance.PlayerStatsDict[PlayerStats.MONEY] = Player.Instance.PlayerCash;
                PlayerStatsObserver.onPlayerStatChanged(PlayerStats.ALL, Player.Instance.PlayerStatsDict);
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


    private void PrepareButtons(ResBuilding selectedBuilding)
    {
        foreach(Buttons btn in selectedBuilding.actionButtons)
        {
            GameObject newBtn = Instantiate(btnPrefab, Vector3.zero, Quaternion.identity, buttonsHolder);
            newBtn.GetComponent<Image>().sprite = BuildingManager.Instance.ButtonImages[((int)btn)];
            newBtn.GetComponent<Button>().onClick.AddListener( () => {BuildingManager.Instance.onBuildingBtnClicked(btn);} );
        }
    }


    public void Rent(ResBuilding selectedBuilding)
    {
        stayCount = 0;
        Player.Instance.CurrentPlayerPlace = selectedBuilding;
        EnterRoom(selectedBuilding);
        LevelManager.onFinishedPlayerAction(MissionType.RENTROOM);
    }


    public void EnterRoom(ResBuilding selectedBuilding)
    {
        resBuildingSelectOverlay.SetActive(false);
        roomBgOverlay.SetActive(true);
        roomBgOverlay.GetComponent<Image>().sprite = selectedBuilding.apartmentBgImage;
        miniBtnsHolder.SetActive(true);
        PrepareButtons(selectedBuilding);
        Debug.Log(selectedBuilding.actionButtons);
    }


    public void LeaveRoom()
    {
        roomBgOverlay.SetActive(false);
        miniBtnsHolder.SetActive(false);
        
        for (var i = buttonsHolder.childCount - 1; i >= 0; i--)
        {
            Object.Destroy(buttonsHolder.GetChild(i).gameObject);
        }
    }


    public void ShowItems(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.VEHICLE:
                playerItemsListManager.ShowItems(Player.Instance.PlayerOwnedVehicles);
                break;
            case ItemType.APPLIANCE:
                playerItemsListManager.ShowItems(Player.Instance.PlayerOwnedAppliances);
                break;
            case ItemType.CONSUMABLE:
                playerItemsListManager.ShowItems(Player.Instance.PlayerOwnedGroceries);
                break;
        }
    }
}