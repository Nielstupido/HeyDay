using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BadgeAwardView : MonoBehaviour
{
    [SerializeField] private Image badgeImage;
    [SerializeField] private TextMeshProUGUI messageText;


    public void PrepareBadgeAwardView(BadgeAwards badgeAward)
    {
        switch (badgeAward)
        {
            case BadgeAwards.BRONZE:
                messageText.text = "Congratulations! Adjustments may be needed, but your gaming journey is a learning process. Keep having fun!";
                break;
            case BadgeAwards.SILVER:
                messageText.text = "Congratulations! Your balanced approach, spending close to the budget and saving as expected, is a winning strategy";
                break;
            case BadgeAwards.GOLD:
                messageText.text = "Congratulations! Your frugal spending and exceeding expected savings truly set you apart.";
                break;
        }
        this.gameObject.SetActive(true);
    }


    public void ShowLeaderboard()
    {
        EndLevelManager.Instance.OpenLeaderboardView();
    }
}
