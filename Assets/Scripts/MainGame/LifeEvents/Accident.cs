using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accident : LifeEvent
{
    private float hospitalBill = 350f;


    public override void TriggerLifeEvent()
    {
        int hospitalizedDays = Random.Range(3, 6);
        HospitalManager.Instance.Hospitalized(hospitalizedDays, hospitalBill);
        this.message = "Regrettably, your character has been involved in a road accident, " + 
                "leading to a hospital stay for the next " + hospitalizedDays.ToString() + " days. Brace yourself for " +
                "the challenges ahead, including covering the incurred hospital bills. Navigate " +
                "wisely through this unexpected twist in your journey.";
        LifeEventsPrompt.Instance.DisplayPrompt(message);
        AudioManager.Instance.PlaySFX("Ambulance");
    }
}