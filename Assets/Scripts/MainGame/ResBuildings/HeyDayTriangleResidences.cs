using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeyDayTriangleResidences : ResBuilding
{
    private void Start()
    {
        this.buildingEnumName = ResBuildings.HEYDAYTRIANGLERESIDENCES;
        this.buildingNameStr = "HeyDay Triangle Residences";
        this.monthlyRent = 3500f;
        this.monthlyElecCharge = 700f;
        this.monthlyWaterCharge = 350f;
        this.dailyAdtnlHappiness = 20f; 
        this.adtnlEnergyForSleep = 15f;

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
