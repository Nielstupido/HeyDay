using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegalSanctuary : ResBuilding
{
    private void Start()
    {
        this.buildingEnumName = ResBuildings.REGALSANCTUARY;
        this.buildingNameStr = "Regal Sanctuary";
        this.monthlyRent = 6000f;
        this.monthlyElecCharge = 1000f;
        this.monthlyWaterCharge = 250f;
        this.dailyAdtnlHappiness = 8f; 
        this.adtnlEnergyForSleep = 1.5f;

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
                    this.Sleep();
                    break;
                case Buttons.EAT:
                    Debug.Log("i'll be eating");
                    break;
            }
    }
}
