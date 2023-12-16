using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inflation : LifeEvent
{
    public override void TriggerLifeEvent()
    {
        float inflationRate = Random.Range(1f, 6f);
        GameManager.Instance.InflationRate = inflationRate;
        this.message = "Attention! The city is grappling with a staggering inflation " +
                        "of " + inflationRate.ToString() + "% that has taken a toll on " + 
                        "the economy. Prepare for the financial implications as prices soar." +
                        " Navigate wisely through this challenging period, making strategic " +
                        "decisions to weather the storm of rising costs and economic instability.";
        LifeEventsPrompt.Instance.DisplayPrompt(message);
        AudioManager.Instance.PlaySFX("News");
    }
}