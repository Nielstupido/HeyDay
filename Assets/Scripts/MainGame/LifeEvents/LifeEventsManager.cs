using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LifeEvents
{
    RANDOM,
    ROBBERY, 
    INFLATION,
    EARTHQUAKE,
    ACCIDENT
}


public class LifeEventsManager : MonoBehaviour
{
    public static LifeEventsManager Instance { get; private set; }


    private void Awake() 
    { 
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    public void StartLifeEvent(LifeEvents lifeEvent = LifeEvents.RANDOM)
    {
        float randomChance;
        float possibilityPercentage;
        LifeEvent upcomingEvent;

        switch (lifeEvent)
        {
            case LifeEvents.ROBBERY:
                upcomingEvent = new Robbery();
                possibilityPercentage = 0.1f;
                if (Player.Instance.PlayerCash > 10000)
                {
                    possibilityPercentage = 0.4f;
                }
                break;
            case LifeEvents.ACCIDENT:
                upcomingEvent = new Accident();
                possibilityPercentage = 0.2f;
                break;  
            default:
                randomChance = Random.Range(0, 2);
                possibilityPercentage = 0.05f;
                if (randomChance == 0)
                {
                    upcomingEvent = new Earthquake();
                }
                else
                {
                    upcomingEvent = new Inflation();
                }
                break;
        }

        randomChance = Random.Range(0f, 1f);

        if (randomChance > possibilityPercentage) 
        {
            return;
        }

        upcomingEvent.TriggerLifeEvent();
    }
}
