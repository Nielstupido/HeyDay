using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : Building
{
    private void Start()
    {
        buildingName = Buildings.FACTORY;
        actionButtons = new List<Buttons>(){Buttons.APPLY, Buttons.WORK, Buttons.QUIT};     
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
                case Buttons.APPLY:
                    Debug.Log("money deposited");
                    break;
                case Buttons.WORK:
                    Debug.Log("money deposited");
                    break;
                case Buttons.QUIT:
                    Debug.Log("money deposited");
                    break;
            }
    }
}
