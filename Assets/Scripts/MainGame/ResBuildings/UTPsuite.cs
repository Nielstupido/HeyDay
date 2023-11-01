using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UTPsuite : ResBuilding
{
    private void Start()
    {
        this.buildingName = ResBuildings.UTPSUITE;
        this.buildingNameStr = "UTP Suite";
        this.monthlyRent = 4500f;
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

    }


    public override void CheckBtnClicked(Buttons clickedBtn)
    {
        if (Player.Instance.CurrentPlayerPlace == this)
            switch (clickedBtn)
            {
                case Buttons.SLEEP:
                    Sleep();
                    break;
                case Buttons.EAT:
                    Debug.Log("i'll be eating");
                    break;
            }
    }
}
