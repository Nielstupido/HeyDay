using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LvlLeaderboardObj : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerRankText;
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private TextMeshProUGUI playerScoreText;

    public void SetupLeaderboardObj(int playerRank, string playerName, float playerScore)
    {
        playerRankText.text = playerRank.ToString();
        playerNameText.text = playerName;
        playerScoreText.text = playerScore.ToString();
    }
}
