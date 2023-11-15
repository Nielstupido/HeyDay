using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Residential : Building
{
    private void Start()
    {
        this.buildingName = Buildings.RESIDENTIAL;
        this.buildingOpeningTime = 0f;
        this.buildingClosingTime = 0f;

        this.actionButtons = new List<Buttons>(){Buttons.ENTER};
        BuildingManager.Instance.onBuildingBtnClicked += CheckBtnClicked;
    }


    private void OnDestroy()
    {
        BuildingManager.Instance.onBuildingBtnClicked -= CheckBtnClicked;
    }


    public override void CheckBtnClicked(Buttons clickedBtn)
    {
        if (BuildingManager.Instance.CurrentSelectedBuilding.buildingName == this.buildingName)
            switch (clickedBtn)
            {
                case Buttons.ENTER:
                    BuildingManager.Instance.EnterResidentialArea();
                    break;
            }
    }


    public override void CheckButtons()
    {
    }
}
