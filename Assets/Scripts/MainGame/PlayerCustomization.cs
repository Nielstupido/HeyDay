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


    public void Continue()
    {
        player.PlayerName = playerInput.text;
        playerInfoManager.SetName();
        gameManager.StartGame();
        this.gameObject.SetActive(false);
    }
}
