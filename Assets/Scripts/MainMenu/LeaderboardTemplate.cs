using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LeaderboardTemplate : MonoBehaviour
{
    public Text playerNameText;
    public Text playerScoreText;
    public Image rankImage;

    public void SetPlayerInfo(string playerName, int playerScore)
    {
        playerNameText.text = playerName;
        playerScoreText.text = playerScore.ToString();
    }
}
