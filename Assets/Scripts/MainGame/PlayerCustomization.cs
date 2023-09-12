using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerCustomization : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private TextMeshProUGUI playerInput;
    [SerializeField] private PlayerInfoManager playerInfoManager;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject ChoosePlayerOverlay;
    [SerializeField] private GameObject PlayerCustomizationOverlay;
    public void Continue()
    {
        player.PlayerName = playerInput.text;
        playerInfoManager.SetName();
        gameManager.StartGame();
        this.gameObject.SetActive(false);
    }
    public void ChoosePlayer(bool isBoy)
    {
        if (isBoy)
        {
            ChoosePlayerOverlay.SetActive(false);
            PlayerCustomizationOverlay.SetActive(true);
        }
        else
        {
            ChoosePlayerOverlay.SetActive(false);
            PlayerCustomizationOverlay.SetActive(true);
        }

    
    }
}
