using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStatsUiManager : MonoBehaviour
{
    [SerializeField] private Slider hungerBar;
    [SerializeField] private Slider happinessBar;
    [SerializeField] private Slider energyBar;
    [SerializeField] private Slider hungerBarSmallOverlay;
    [SerializeField] private Slider happinessBarSmallOverlay;
    [SerializeField] private Slider energyBarSmallOverlay;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI moneyTextSmallOverlay;


    private void Start()
    {
        PlayerStatsObserver.onPlayerStatChanged += UpdateStatsText;
        hungerBar.interactable = false;
        happinessBar.interactable = false;
        energyBar.interactable = false;
        hungerBarSmallOverlay.interactable = false;
        happinessBarSmallOverlay.interactable = false;
        energyBarSmallOverlay.interactable = false;
    }


    private void OnDestroy()
    {
        PlayerStatsObserver.onPlayerStatChanged -= UpdateStatsText;
    }


    private void UpdateStatsText(PlayerStats statName, Dictionary<PlayerStats, float> playerStatsDict)
    {
        switch (statName)
        {
            case PlayerStats.ALL:
                hungerBar.value = playerStatsDict[PlayerStats.HUNGER];
                happinessBar.value = playerStatsDict[PlayerStats.HAPPINESS];
                energyBar.value = playerStatsDict[PlayerStats.ENERGY];
                moneyText.text = playerStatsDict[PlayerStats.MONEY].ToString();
                hungerBarSmallOverlay.value = playerStatsDict[PlayerStats.HUNGER];
                happinessBarSmallOverlay.value = playerStatsDict[PlayerStats.HAPPINESS];
                energyBarSmallOverlay.value = playerStatsDict[PlayerStats.ENERGY];
                moneyTextSmallOverlay.text = playerStatsDict[PlayerStats.MONEY].ToString();
                break;
            case PlayerStats.HUNGER:
                hungerBar.value = playerStatsDict[statName];
                hungerBarSmallOverlay.value = playerStatsDict[statName];
                break;
            case PlayerStats.HAPPINESS:
                happinessBar.value = playerStatsDict[statName];
                happinessBarSmallOverlay.value = playerStatsDict[statName];
                break;
            case PlayerStats.ENERGY:
                energyBar.value = playerStatsDict[statName];
                energyBarSmallOverlay.value = playerStatsDict[statName];
                break;
            case PlayerStats.MONEY:
                moneyText.text = playerStatsDict[statName].ToString();
                moneyTextSmallOverlay.text = playerStatsDict[statName].ToString();
                break;
        }
    }
}
