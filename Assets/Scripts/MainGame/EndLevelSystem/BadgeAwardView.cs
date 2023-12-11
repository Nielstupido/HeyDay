using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BadgeAwardView : MonoBehaviour
{
    [SerializeField] private Image badgeImage;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private List<Sprite> badgeImages = new List<Sprite>();
    private Sprite playerBadge;


    public void PrepareBadgeAwardView(BadgeAwards badgeAward)
    {
        switch (badgeAward)
        {
            case BadgeAwards.BRONZE:
                playerBadge = badgeImages[0];
                badgeImage.sprite = badgeImages[0];
                messageText.text = "Congratulations! Adjustments may be needed, but your gaming journey is a learning process. Keep having fun!";
                break;
            case BadgeAwards.SILVER:
                playerBadge = badgeImages[1];
                badgeImage.sprite = badgeImages[1];
                messageText.text = "Congratulations! Your balanced approach, spending close to the budget and saving as expected, is a winning strategy";
                break;
            case BadgeAwards.GOLD:
                playerBadge = badgeImages[2];
                badgeImage.sprite = badgeImages[2];
                messageText.text = "Congratulations! Your frugal spending and exceeding expected savings truly set you apart.";
                break;
        }

        badgeImage.SetNativeSize();
        this.gameObject.SetActive(true);
    }


    public void ShowLeaderboard()
    {
        EndLevelManager.Instance.OpenLeaderboardView(playerBadge);
    }
}
