using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampaguitaCondotel : ResBuilding
{
    private void Awake()
    {
        this.buildingEnumName = ResBuildings.SAMPAGUITACONDOTEL;
        this.buildingNameStr = "Sampaguita Condotel";
        this.monthlyRent = 10000f;
        this.monthlyElecCharge = 1200f;
        this.monthlyWaterCharge = 500f;
        this.dailyAdtnlHappiness = 25f; 
        this.adtnlEnergyForSleep = 25f;

        this.actionButtons = new List<Buttons>(){Buttons.SLEEP, Buttons.EAT, Buttons.PAY};
    }


    private void Start()
    {
        BuildingManager.Instance.onBuildingBtnClicked += CheckBtnClicked;
    }


    private void OnDestroy()
    {
        BuildingManager.Instance.onBuildingBtnClicked -= CheckBtnClicked;
    } 


    public override void Sleep()
    {
        SleepManager.Instance.ShowSleepOverlay();
    }


    public override void Eat()
    {
        Player.Instance.ConsumeGrocery();
    }


    public override void CheckBtnClicked(Buttons clickedBtn)
    {
        if (Player.Instance.CurrentPlayerPlace == this)
            switch (clickedBtn)
            {
                case Buttons.SLEEP:
                    this.Sleep();
                    break;
                case Buttons.EAT:
                    this.Eat();
                    break;
                case Buttons.PAY:
                    ResBuildingManager.Instance.PayDebt();
                    break;
            }
    }
}
