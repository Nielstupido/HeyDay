using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Residential : Building
{
    private void Start()
    {
        buildingName = Buildings.RESIDENTIAL;
        actionButtons = new List<Buttons>(){Buttons.ENTER};
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
}
