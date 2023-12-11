using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HospitalManager : MonoBehaviour
{
    //hospitalized prompt
    [SerializeField] private GameObject hospitalizedPrompt;
    [SerializeField] private TextMeshProUGUI daysHospitalized;
    [SerializeField] private TextMeshProUGUI totalBill; 

    //hospital bill overlay
    [SerializeField] private GameObject payBillsOverlay;
    [SerializeField] private TextMeshProUGUI playerName; 
    [SerializeField] private TextMeshProUGUI refNum; 
    [SerializeField] private TextMeshProUGUI totalOutstandingBill; 

    [SerializeField] private Hospital hospitalObj; 
    [SerializeField] private GameObject debtReminderOverlay; 
    [SerializeField] private Prompts notEnoughMoneyForBills; 
    [SerializeField] private Prompts paidBills; 

    private const float BillInterest = 100f;
    private int daysUnpaid; 
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
    }


    private void OnDestroy()
    {
        TimeManager.onDayAdded -= AddInterest;
    }


    public void Hospitalized(float dayCount, float bill = 800)
    {
        hospitalBill = bill * dayCount;
        daysHospitalized.text = dayCount.ToString();
        Player.Instance.PlayerHospitalOutstandingDebt += hospitalBill;
        totalBill.text = Player.Instance.PlayerHospitalOutstandingDebt.ToString();
        daysUnpaid = 0;
        hospitalizedPrompt.SetActive(true);
        
        Player.Instance.PlayerStatsDict[PlayerStats.HAPPINESS] = 100;
        Player.Instance.PlayerStatsDict[PlayerStats.ENERGY] = 100;
        Player.Instance.PlayerStatsDict[PlayerStats.HUNGER] = 100;
        PlayerStatsObserver.onPlayerStatChanged(PlayerStats.ALL, Player.Instance.PlayerStatsDict);
        PlayerTravelManager.Instance.MovePlayerModel(hospitalObj);
    }


    public void CloseHospitalizedOverlay()
    {
        hospitalizedPrompt.SetActive(false);
        StartCoroutine(ShowReminder());
    }


    public void OpenBillingOverlay()
    {
        payBillsOverlay.SetActive(true);
        playerName.text = Player.Instance.PlayerName;
        refNum.text = Random.Range(11000, 100000).ToString();
        totalOutstandingBill.text = Player.Instance.PlayerHospitalOutstandingDebt.ToString();
    }


    public void CloseBillingOverlay()
    {
        payBillsOverlay.SetActive(false);
    }
    
    
    public void PayHospitalFees()
    {
        if (Player.Instance.Pay(Player.Instance.PlayerHospitalOutstandingDebt, 0.5f, 10f, 5, notEnoughMoneyForBills))
        {
            daysUnpaid = 0;
            Player.Instance.PlayerHospitalOutstandingDebt = 0f;
            StartCoroutine(PayBills());
        }
    }


    private void AddInterest(int dayCount)
    {
        if (Player.Instance.PlayerHospitalOutstandingDebt != 0f)
        {
            Player.Instance.PlayerHospitalOutstandingDebt += BillInterest;
            daysUnpaid++;
        }

        if (dayCount == 6)
        {
            //start bad ending
        }
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
        yield return null;
    }
}
