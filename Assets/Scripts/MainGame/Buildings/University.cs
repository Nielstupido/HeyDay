using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class University : Building
{
    private void Start()
    {
        buildingName = Buildings.UNIVERSITY;
        actionButtons = new List<Buttons>(){Buttons.STUDY, Buttons.ENROL};
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
                case Buttons.STUDY:
                    Debug.Log("money deposited");
                    break;
                case Buttons.ENROL:
                    Debug.Log("money deposited");
                    break;
            }
    }
}
