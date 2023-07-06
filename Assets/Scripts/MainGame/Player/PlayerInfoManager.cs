using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PlayerInfoManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private Player player;


    public void SetName()
    {
        playerNameText.text = player.PlayerName;
    }


    public void SetAge(int ageValue)
    {

    }
}
