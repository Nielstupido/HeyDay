using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HospitalManager : MonoBehaviour
{
    //hospitalized prompt
    [SerializeField] private GameObject hospitalizedPrompt;
    [SerializeField] private GameObject hospitalizedPopUp;
    [SerializeField] private TextMeshProUGUI daysHospitalized;
    [SerializeField] private TextMeshProUGUI totalBill; 

    //hospital bill overlay
    [SerializeField] private GameObject payBillsOverlay;
    [SerializeField] private GameObject payBillsPopUp;
    [SerializeField] private TextMeshProUGUI playerName; 
    [SerializeField] private TextMeshProUGUI refNum; 
    [SerializeField] private TextMeshProUGUI totalOutstandingBill; 

    [SerializeField] private Hospital hospitalObj; 
    [SerializeField] private GameObject debtReminderOverlay;
    [SerializeField] private GameObject debtReminderPopUp;  
    [SerializeField] private Prompts notEnoughMoneyForBills; 
    [SerializeField] private Prompts paidBills; 

    private const float BillInterest = 30f;
    private bool addInterest = false;
    private int daysUnpaid = 0; 
    private float hospitalBill; 
    public static HospitalManager Instance { get; private set; }


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

        TimeManager.onDayAdded += AddInterest;
        GameManager.onGameStarted += LoadGameData;
        GameManager.onSaveGameStateData += SaveGameData;
    }


    private void OnDestroy()
    {
        GameManager.onGameStarted -= LoadGameData;
        GameManager.onSaveGameStateData -= SaveGameData;
        TimeManager.onDayAdded -= AddInterest;
    }


    private void OnEnable()
    {
        TimeManager.onDayAdded += AddInterest;
    }
    

    private void OnDisable()
    {
        TimeManager.onDayAdded -= AddInterest;
    }


    private void LoadGameData()
    {
        this.daysUnpaid = GameManager.Instance.CurrentGameStateData.daysUnpaid;
    }


    private void SaveGameData()
    {
        GameManager.Instance.CurrentGameStateData.daysUnpaid = this.daysUnpaid;
    }


    public void Hospitalized(int dayCount, float bill = 250)
    {
        AudioManager.Instance.PlaySFX("Ambulance");
        hospitalBill = bill * dayCount;
        daysHospitalized.text = dayCount.ToString();
        Player.Instance.PlayerHospitalOutstandingDebt += hospitalBill;
        totalBill.text = Player.Instance.PlayerHospitalOutstandingDebt.ToString();

        hospitalizedPrompt.SetActive(true);
        OverlayAnimations.Instance.AnimOpenOverlay(hospitalizedPopUp);
        
        Player.Instance.PlayerStatsDict[PlayerStats.HAPPINESS] = 100;
        Player.Instance.PlayerStatsDict[PlayerStats.ENERGY] = 100;
        Player.Instance.PlayerStatsDict[PlayerStats.HUNGER] = 100;
        PlayerStatsObserver.onPlayerStatChanged(PlayerStats.ALL, Player.Instance.PlayerStatsDict);
        PlayerTravelManager.Instance.MovePlayerModel(true, hospitalObj);
        BuildingManager.Instance.CurrentSelectedBuilding = hospitalObj;
        BuildingManager.Instance.EnterBuilding(hospitalObj);
        addInterest = false;
        TimeManager.Instance.IncrementDayCount(true, dayCount);
    }


    public void CloseHospitalizedOverlay()
    {
        AudioManager.Instance.PlaySFX("Select");
        hospitalizedPrompt.SetActive(false);
        OverlayAnimations.Instance.AnimCloseOverlay(hospitalizedPopUp, hospitalizedPrompt);
        StartCoroutine(ShowReminder());
    }


    public void OpenBillingOverlay()
    {
        payBillsOverlay.SetActive(true);
        OverlayAnimations.Instance.AnimOpenOverlay(payBillsPopUp);
        playerName.text = Player.Instance.PlayerName;
        refNum.text = Random.Range(11000, 100000).ToString();
        totalOutstandingBill.text = Player.Instance.PlayerHospitalOutstandingDebt.ToString();
    }


    public void CloseBillingOverlay()
    {
        AudioManager.Instance.PlaySFX("Select");
        payBillsOverlay.SetActive(false);
        OverlayAnimations.Instance.AnimCloseOverlay(payBillsPopUp, payBillsOverlay);
    }
    
    
    public void PayHospitalFees()
    {
        AudioManager.Instance.PlaySFX("Select");
        if (Player.Instance.Pay(false, Player.Instance.PlayerHospitalOutstandingDebt, 0.5f, 10f, 5, notEnoughMoneyForBills))
        {
            daysUnpaid = 0;
            Player.Instance.PlayerHospitalOutstandingDebt = 0f;
            StartCoroutine(PayBills());
        }
    }


    private void AddInterest(int dayCount)
    {
        if (addInterest)
        {
            if (Player.Instance.PlayerHospitalOutstandingDebt != 0f)
            {
                Player.Instance.PlayerHospitalOutstandingDebt += BillInterest;
                daysUnpaid++;

                if (daysUnpaid >= 15)
                {
                    GameManager.Instance.GameOver("HOSPITAL BILLS");
                }
            }
        }

        addInterest = true;
    }


    private IEnumerator PayBills()
    {
        AnimOverlayManager.Instance.StartAnim(ActionAnimations.BUY);
        yield return new WaitForSeconds(2f);
        AnimOverlayManager.Instance.StopAnim();
        payBillsOverlay.SetActive(false);
        PromptManager.Instance.ShowPrompt(paidBills);
        BuildingManager.Instance.PrepareButtons(hospitalObj);
        yield return null;
    }


    private IEnumerator ShowReminder()
    {
        yield return new WaitForSeconds(0.5f);
        debtReminderOverlay.SetActive(true);
        OverlayAnimations.Instance.AnimOpenOverlay(debtReminderPopUp);
        yield return null;
    }
}
