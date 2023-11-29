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
    private float hospitalBill = 1500; 
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


    public void Hospitalized(float dayCount, float bill = 800)
    {
        hospitalBill = bill * dayCount;
        hospitalizedPrompt.SetActive(true);
        daysHospitalized.text = dayCount.ToString();
        totalBill.text = hospitalBill.ToString();

        numOfdays = dayCount;

        playerStatsDictTemp[PlayerStats.HAPPINESS] = 100;
        playerStatsDictTemp[PlayerStats.ENERGY] = 100;
        playerStatsDictTemp[PlayerStats.HUNGER] = 100;
        Player.Instance.PlayerStatsDict = playerStatsDictTemp;
        PlayerStatsObserver.onPlayerStatChanged(PlayerStats.ALL, playerStatsDictTemp);
    }
    
    
    public void PayHospitalFees()
    {
        playerStatsDictTemp[PlayerStats.MONEY] = Player.Instance.PlayerStatsDict[PlayerStats.MONEY] - (hospitalBill);
        Player.Instance.PlayerStatsDict = playerStatsDictTemp;
        PlayerStatsObserver.onPlayerStatChanged(PlayerStats.MONEY, playerStatsDictTemp);
        hospitalizedPrompt.SetActive(false);
    }
}
