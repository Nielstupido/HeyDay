using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HospitalManager : MonoBehaviour
{
    [SerializeField] private GameObject hospitalizedPrompt;
    [SerializeField] private TextMeshProUGUI daysHospitalized;
    [SerializeField] private TextMeshProUGUI totalBill; 
    private Dictionary<PlayerStats, float> playerStatsDictTemp = new Dictionary<PlayerStats, float>();
    private float numOfdays = 0;
    private float hospitalFee = 1500; // hospital fee per day
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
    }


    public void Hospitalized(float dayCount)
    {
        hospitalizedPrompt.SetActive(true);
        daysHospitalized.text = dayCount.ToString();
        float bill = hospitalFee*dayCount;
        totalBill.text = bill.ToString();

        numOfdays = dayCount;
    }
    
    
    public void PayHospitalFees()
    {
        playerStatsDictTemp[PlayerStats.MONEY] = Player.Instance.PlayerStatsDict[PlayerStats.MONEY] - numOfdays*hospitalFee;
        playerStatsDictTemp[PlayerStats.HAPPINESS] = 100;
        playerStatsDictTemp[PlayerStats.ENERGY] = 100;
        playerStatsDictTemp[PlayerStats.HUNGER] = 100;
        Player.Instance.PlayerStatsDict = playerStatsDictTemp;
        PlayerStatsObserver.onPlayerStatChanged(PlayerStats.ALL, playerStatsDictTemp);
        hospitalizedPrompt.SetActive(false);
    }
}
