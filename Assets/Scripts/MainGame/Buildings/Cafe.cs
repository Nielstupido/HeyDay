using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cafe : Building
{
    private void Start()
    {
        this.buildingName = Buildings.CAFE;
        this.buildingOpeningTime = 10;
        this.buildingClosingTime = 22;

        this.actionButtons = new List<Buttons>(){Buttons.BUYFOOD, Buttons.BUYDRINK, Buttons.APPLY, Buttons.WORK, Buttons.QUIT};
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
                case Buttons.BUYFOOD:
                    Debug.Log("money deposited");
                    break;
                case Buttons.BUYDRINK:
                    Debug.Log("money deposited");
                    break;
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
