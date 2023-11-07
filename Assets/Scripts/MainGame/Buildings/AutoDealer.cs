using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDealer : Building
{
    private void Start()
    {
        this.buildingName = Buildings.AUTODEALER;
        this.buildingOpeningTime = 8f;
        this.buildingClosingTime = 16f;

        this.actionButtons = new List<Buttons>(){Buttons.SHOP};
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
                case Buttons.SHOP:
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


    public override void CheckButtons()
    {
        if (this.currentlyHired)
        {
            this.actionButtons.Add(Buttons.WORK);
            this.actionButtons.Add(Buttons.QUIT);   
        }
        else
        {
            this.actionButtons.Add(Buttons.APPLY);   
        }
    }
}
