using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory: Building
{
    private void Start()
    {
        this.buildingStringName = "Bene Factory";
        this.buildingEnumName = Buildings.FACTORY;
        this.buildingOpeningTime = 8f;
        this.buildingClosingTime = 18f;

        this.actionButtons = new List<Buttons>(){Buttons.APPLY};
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
            this.actionButtons = new List<Buttons>(){Buttons.WORK, Buttons.QUIT};     
        }
    }
}
