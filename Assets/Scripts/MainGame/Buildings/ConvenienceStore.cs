using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvenienceStore : Building
{
    private void Start()
    {
        this.buildingStringName = "Uncle Ben Convenience Store";
        this.buildingEnumName = Buildings.CONVENIENCESTORE;
        this.buildingOpeningTime = 0f;
        this.buildingClosingTime = 0f;

        this.actionButtons = new List<Buttons>(){Buttons.BUY};
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
                case Buttons.BUY:
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
