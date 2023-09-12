using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightClub : Building
{
    private void Start()
    {
        buildingName = Buildings.NIGHTCLUB;
        actionButtons = new List<Buttons>(){Buttons.PARTY};
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
                case Buttons.PARTY:
                    Debug.Log("party party");
                    break;
            }
    }
}
