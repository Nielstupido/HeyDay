using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UTPsuite : ResBuilding
{
    private void Start()
    {
        this.buildingEnumName = ResBuildings.UTPSUITE;
        this.buildingNameStr = "UTP Suite";
        this.monthlyRent = 2500f;
        this.monthlyElecCharge = 800f;
        this.monthlyWaterCharge = 2500f;
        this.dailyAdtnlHappiness = 5f; 
        this.adtnlEnergyForSleep = 1f;

        this.actionButtons = new List<Buttons>(){Buttons.SLEEP, Buttons.EAT};
        BuildingManager.Instance.onBuildingBtnClicked += CheckBtnClicked;
    }


    private void OnDestroy()
    {
        BuildingManager.Instance.onBuildingBtnClicked -= CheckBtnClicked;
    } 


    public override void Sleep()
    {
        SleepManager.Instance.ShowSleepOverlay(this.adtnlEnergyForSleep);
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
            }
    }
}
