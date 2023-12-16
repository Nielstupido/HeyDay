using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earthquake : LifeEvent
{
    public override void TriggerLifeEvent()
    {
        Player.Instance.PlayerOwnedAppliances.RemoveRange(0, (Player.Instance.PlayerOwnedAppliances.Count / 2));
        this.message = "Alert! The city has been rocked by a powerful earthquake, " +
                        "causing significant damage to your apartment and belongings. " +
                        "Brace for the aftermath and strategically navigate the upheaval " +
                        "as you assess the impact on your possessions. The road to recovery " +
                        "awaits, but first, be prepared to deal with the consequences of this seismic event";
        LifeEventsPrompt.Instance.DisplayPrompt(message);
        AudioManager.Instance.PlaySFX("Earthquake");
    }
}