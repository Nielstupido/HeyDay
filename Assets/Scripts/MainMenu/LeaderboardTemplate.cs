using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LeaderboardTemplate : MonoBehaviour
{
    public Text playerRank;
    public Text playerNameText;
    public Text playerScoreText;


    public void SetPlayerInfo(string playerName, int rank, int playerScore)
    {
        playerRank.text = rank.ToString();
        playerNameText.text = playerName;
        playerScoreText.text = playerScore.ToString();
    }
}
