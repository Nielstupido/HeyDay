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
    [SerializeField] private PlayerItemsListManager playerItemsListManager;
    [SerializeField] private Prompts notEnoughMoneyRent;
    [SerializeField] private Prompts notEnoughMoneyDebt;
    [SerializeField] private Prompts debtReminder;
    [SerializeField] private Prompts rentBillPaid;
    [SerializeField] private Prompts existingDebt;
    [SerializeField] private GameObject debtReminderOverlay; 

    private ResBuilding currentSelectedResBuilding;
    private int stayCount;
    private float unpaidBill;
    private int daysUnpaid;
    public GameObject ResBuildingSelectOverlay { get{return resBuildingSelectOverlay;}}
    public ResBuilding CurrentSelectedResBuilding { set{currentSelectedResBuilding = value; ShowBtn();} get{return currentSelectedResBuilding;}}
    public float UnpaidBill { set{unpaidBill = value; } get{return unpaidBill;}}
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
        enterBtn.GetComponent<Button>().onClick.AddListener( () => {EnterRoom(currentSelectedResBuilding);} );
        rentBtn.GetComponent<Button>().onClick.AddListener( () => {Rent(currentSelectedResBuilding);} );
        TimeManager.onDayAdded += NewDay;
        stayCount = 0;
        daysUnpaid = 0;
        unpaidBill = 0f;
    }


    private void OnDestroy()
    {
        TimeManager.onDayAdded -= NewDay;
    }

    private void OnEnable()
    {
        TimeManager.onDayAdded += NewDay;
    }
    private void OnDisable()
    {
        TimeManager.onDayAdded -= NewDay;
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


    private void NewDay(int dayCount)
    {
        Player.Instance.PlayerStatsDict[PlayerStats.HAPPINESS] += Player.Instance.CurrentPlayerPlace.dailyAdtnlHappiness;
        PlayerStatsObserver.onPlayerStatChanged(PlayerStats.HAPPINESS, Player.Instance.PlayerStatsDict);
        ComputeBillings(dayCount);
    }


    private void ComputeBillings(int currentDayCount)
    {
        if (Player.Instance.CurrentPlayerPlace == null)
        {
            return;
        }
        
        stayCount++;
        float totalBilling = 0;

        if (stayCount % 3 == 0)
        {
            totalBilling = Player.Instance.CurrentPlayerPlace.monthlyElecCharge + Player.Instance.CurrentPlayerPlace.monthlyRent + Player.Instance.CurrentPlayerPlace.monthlyWaterCharge;

            if (Player.Instance.PlayerCash > totalBilling)
            {
                Player.Instance.PlayerLvlBillExpenses += totalBilling;
                Player.Instance.PlayerCash = Player.Instance.PlayerCash - totalBilling;
                Player.Instance.PlayerStatsDict[PlayerStats.MONEY] = Player.Instance.PlayerCash;
                PlayerStatsObserver.onPlayerStatChanged(PlayerStats.ALL, Player.Instance.PlayerStatsDict);
                daysUnpaid = 0;
                unpaidBill = 0f;
                PromptManager.Instance.ShowPrompt(rentBillPaid);
            }
            else if (Player.Instance.PlayerBankSavings > totalBilling)
            {
                Player.Instance.PlayerLvlBillExpenses += totalBilling;
                Player.Instance.PlayerBankSavings = Player.Instance.PlayerBankSavings - totalBilling;
                daysUnpaid = 0;
                unpaidBill = 0f;
                PromptManager.Instance.ShowPrompt(rentBillPaid);
            }
            else
            {
                unpaidBill = totalBilling;
                debtReminderOverlay.SetActive(true);
            }
        }

        if (unpaidBill != 0f)
        {
            if (daysUnpaid >= 5)
            {
                GameManager.Instance.GameOver();
                return;
            }

            if (daysUnpaid > 0)
            {
                PromptManager.Instance.ShowPrompt(debtReminder);
            }                

            daysUnpaid++;
        }

    }


    private void PrepareButtons(ResBuilding selectedBuilding)
    {
        for (var i = buttonsHolder.childCount - 1; i >= 0; i--)
        {
            Object.Destroy(buttonsHolder.GetChild(i).gameObject);
        }

        foreach(Buttons btn in selectedBuilding.actionButtons)
        {
            GameObject newBtn = Instantiate(btnPrefab, Vector3.zero, Quaternion.identity, buttonsHolder);
            newBtn.GetComponent<Image>().sprite = BuildingManager.Instance.ButtonImages[((int)btn)];
            newBtn.GetComponent<Button>().onClick.AddListener( () => {BuildingManager.Instance.onBuildingBtnClicked(btn);} );

            if (btn == Buttons.PAY)
            {
                if (unpaidBill == 0f)
                {
                    newBtn.SetActive(false);
                }
            }
        }
    }


    public void PayDebt()
    {
        float totalBilling = Player.Instance.CurrentPlayerPlace.monthlyElecCharge + Player.Instance.CurrentPlayerPlace.monthlyRent + Player.Instance.CurrentPlayerPlace.monthlyWaterCharge;
       
        if (Player.Instance.Pay(true, totalBilling, 0.2f, 5f, 2f, notEnoughMoneyDebt, 3f)) 
        {
            unpaidBill = 0f;
            daysUnpaid = 0;
            Player.Instance.PlayerLvlBillExpenses += totalBilling;
            PromptManager.Instance.ShowPrompt(rentBillPaid);
            PrepareButtons(Player.Instance.CurrentPlayerPlace);
        }
    }


    public void Rent(ResBuilding selectedBuilding)
    {
        if (unpaidBill != 0f)
        {
            PromptManager.Instance.ShowPrompt(existingDebt);
            return;
        }

        if (Player.Instance.Pay(true, selectedBuilding.monthlyRent, 0.2f, 5f, 2f, notEnoughMoneyRent, 3f)) 
        {
            Player.Instance.PlayerLvlBillExpenses += selectedBuilding.monthlyRent;
            stayCount = 0;
            Player.Instance.CurrentPlayerPlace = selectedBuilding;
            EnterRoom(selectedBuilding);
            LevelManager.onFinishedPlayerAction(MissionType.RENTROOM);
        }
    }


    public void EnterRoom(ResBuilding selectedBuilding)
    {
        AudioManager.Instance.PlaySFX("Select");
        resBuildingSelectOverlay.SetActive(false);
        roomBgOverlay.SetActive(true);
        roomBgOverlay.GetComponent<Image>().sprite = selectedBuilding.apartmentBgImage;
        miniBtnsHolder.SetActive(true);
        PrepareButtons(selectedBuilding);
        Debug.Log(selectedBuilding.actionButtons);
    }


    public void LeaveRoom()
    {
        AudioManager.Instance.PlaySFX("Select");
        roomBgOverlay.SetActive(false);
        miniBtnsHolder.SetActive(false);
        
        for (var i = buttonsHolder.childCount - 1; i >= 0; i--)
        {
            Object.Destroy(buttonsHolder.GetChild(i).gameObject);
        }
    }


    public void ShowItems(ItemType itemType)
    {
        AudioManager.Instance.PlaySFX("Select");
        switch (itemType)
        {
            case ItemType.VEHICLE:
                playerItemsListManager.ShowItems(itemType, Player.Instance.PlayerOwnedVehicles);
                break;
            case ItemType.APPLIANCE:
                playerItemsListManager.ShowItems(itemType, Player.Instance.PlayerOwnedAppliances);
                break;
        }
    }
}