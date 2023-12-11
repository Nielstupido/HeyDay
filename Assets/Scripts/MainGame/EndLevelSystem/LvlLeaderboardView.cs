using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LvlLeaderboardView : MonoBehaviour
{
    [SerializeField] private Transform leaderboardIemsHolder;
    [SerializeField] private Image badgeImage;
    [SerializeField] private TextMeshProUGUI currentPlayerScoreText;
    [SerializeField] private GameObject leaderboardItemPrefab;


    public void SetupLeaderboard(Dictionary<string, int> playerRecords, Sprite playerBadge)
    {
        badgeImage.sprite = playerBadge;

        for (int i = 0; i < leaderboardIemsHolder.childCount; i++)
        {
            Object.Destroy(leaderboardIemsHolder.GetChild(i).gameObject);
        }

        int rankCounter = 1;
        foreach (var playerRecord in playerRecords)
        {
            GameObject newLeaderboardItem = Instantiate(leaderboardItemPrefab, Vector3.zero, Quaternion.identity, leaderboardIemsHolder);
            string playerName = playerRecord.Key;
            if (playerName == Player.Instance.PlayerName)
            {
                playerName = "You";
            }
            newLeaderboardItem.GetComponent<LvlLeaderboardObj>().SetupLeaderboardObj((rankCounter), playerName, playerRecord.Value);
            rankCounter++;
        }

        this.gameObject.SetActive(true);
    }


    public void NextLevel()
    {
        this.gameObject.SetActive(false);
        EndLevelManager.Instance.NextLevel();
    }
}
