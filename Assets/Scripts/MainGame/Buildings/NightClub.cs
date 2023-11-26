using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightClub : Building
{
    private void Start()
    {
        this.buildingStringName = "Kapuntukan Night Club";
        this.buildingEnumName = Buildings.NIGHTCLUB;
        this.buildingOpeningTime = 20f;
        this.buildingClosingTime = 5f;

        BuildingManager.Instance.onBuildingBtnClicked += CheckBtnClicked;
    }


    private void OnDestroy()
    {
        BuildingManager.Instance.onBuildingBtnClicked -= CheckBtnClicked;
    }


    public override void CheckBtnClicked(Buttons clickedBtn)
    {
        if (BuildingManager.Instance.CurrentSelectedBuilding.buildingEnumName == this.buildingEnumName)
            switch (clickedBtn)
            {
                case Buttons.PARTY:
                    Debug.Log("party party");
                    break;
            }
    }


    public override void CheckButtons()
    {
        this.actionButtons = new List<Buttons>(){Buttons.PARTY};
    }
}
