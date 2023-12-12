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
        int randomChance;
        int possibilityPercentage;
        LifeEvent upcomingEvent;

        switch (lifeEvent)
        {
            case LifeEvents.ROBBERY:
                upcomingEvent = new Robbery();
                possibilityPercentage = 2;
                if (Player.Instance.PlayerCash > 10000)
                {
                    possibilityPercentage = 4;
                }
                break;
            case LifeEvents.ACCIDENT:
                if (Player.Instance.PlayerHospitalOutstandingDebt != 0f)
                {
                    return;
                }
                upcomingEvent = new Accident();
                possibilityPercentage = 2;
                break;  
            default:
                randomChance = Random.Range(0, 2);
                possibilityPercentage = 1;
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

        randomChance = Random.Range(1, 31);

        if (randomChance > possibilityPercentage) 
        {
            return;
        }

        upcomingEvent.TriggerLifeEvent();
    }
}
