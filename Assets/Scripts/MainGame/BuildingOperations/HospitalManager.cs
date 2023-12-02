using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HospitalManager : MonoBehaviour
{
    [SerializeField] private GameObject hospitalizedPrompt;
    [SerializeField] private TextMeshProUGUI daysHospitalized;
    [SerializeField] private TextMeshProUGUI totalBill; 
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

        Player.Instance.PlayerStatsDict[PlayerStats.HAPPINESS] = 100;
        Player.Instance.PlayerStatsDict[PlayerStats.ENERGY] = 100;
        Player.Instance.PlayerStatsDict[PlayerStats.HUNGER] = 100;
        PlayerStatsObserver.onPlayerStatChanged(PlayerStats.ALL, Player.Instance.PlayerStatsDict);
    }
    
    
    public void PayHospitalFees()
    {
        Player.Instance.PlayerStatsDict[PlayerStats.MONEY] -= (hospitalBill);
        PlayerStatsObserver.onPlayerStatChanged(PlayerStats.MONEY, Player.Instance.PlayerStatsDict);
        hospitalizedPrompt.SetActive(false);
    }
}
