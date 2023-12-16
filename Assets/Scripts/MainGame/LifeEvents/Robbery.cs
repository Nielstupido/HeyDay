using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robbery : LifeEvent
{
    public override void TriggerLifeEvent()
    {
        Player.Instance.PlayerCash = 0;
        Player.Instance.PlayerStatsDict[PlayerStats.MONEY] = 0;
        PlayerStatsObserver.onPlayerStatChanged(PlayerStats.MONEY, Player.Instance.PlayerStatsDict);

        this.message = "Emergency Update! You have been robbed! Assess the damage, " +
                        "strategize for recovery, and reclaim what's rightfully yours. " +
                        "To avoid robberies, consider these tips. Avoid carrying " +
                        "large amount of money. Be cautious during nighttime walks.";
        LifeEventsPrompt.Instance.DisplayPrompt(message);
    }
}