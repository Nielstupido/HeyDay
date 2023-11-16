using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeyDayTriangleResidences : ResBuilding
{
    private void Start()
    {
        this.buildingEnumName = ResBuildings.HEYDAYTRIANGLERESIDENCES;
        this.buildingNameStr = "HeyDay Triangle Residences";
        this.monthlyRent = 7000f;
        this.monthlyElecCharge = 1100f;
        this.monthlyWaterCharge = 300f;
        this.dailyAdtnlHappiness = 10f; 
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
                    Sleep();
                    break;
                case Buttons.EAT:
                    Debug.Log("i'll be eating");
                    break;
            }
    }
}
