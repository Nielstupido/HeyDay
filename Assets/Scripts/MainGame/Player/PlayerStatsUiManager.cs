using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStatsUiManager : MonoBehaviour
{
    [SerializeField] private Slider hungerBar;
    [SerializeField] private Slider happinessBar;
    [SerializeField] private Slider energyBar;
    [SerializeField] private TextMeshProUGUI moneyText;


    private void Start()
    {
        PlayerStatsObserver.onPlayerStatChanged += UpdateStatsText;
    }


    private void UpdateStatsText(PlayerStats statName, IDictionary<PlayerStats, float> playerStatsDict)
    {
        switch (statName)
        {
            case PlayerStats.ALL:
                hungerBar.value = playerStatsDict[PlayerStats.HUNGER];
                happinessBar.value = playerStatsDict[PlayerStats.HAPPINESS];
                energyBar.value = playerStatsDict[PlayerStats.ENERGY];
                moneyText.text = playerStatsDict[PlayerStats.MONEY].ToString();
                break;
            case PlayerStats.HUNGER:
                hungerBar.value = playerStatsDict[statName];
                break;
            case PlayerStats.HAPPINESS:
                happinessBar.value = playerStatsDict[statName];
                break;
            case PlayerStats.ENERGY:
                energyBar.value = playerStatsDict[statName];
                break;
            case PlayerStats.MONEY:
                moneyText.text = playerStatsDict[statName].ToString();
                break;
        }
    }
}
