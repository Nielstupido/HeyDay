using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegalSanctuary : ResBuilding
{
    private void Start()
    {
        this.buildingEnumName = ResBuildings.REGALSANCTUARY;
        this.buildingNameStr = "Regal Sanctuary";
        this.monthlyRent = 2500f;
        this.monthlyElecCharge = 600f;
        this.monthlyWaterCharge = 200f;
        this.dailyAdtnlHappiness = 15f; 
        this.adtnlEnergyForSleep = 10f;

        this.actionButtons = new List<Buttons>(){Buttons.SLEEP, Buttons.EAT, Buttons.PAY};
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
