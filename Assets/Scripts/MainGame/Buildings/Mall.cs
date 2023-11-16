using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mall : Building
{
    private void Start()
    {
        this.buildingStringName = "Luz Mall";
        this.buildingEnumName = Buildings.MALL;
        this.buildingOpeningTime = 10f;
        this.buildingClosingTime = 22f;

        this.actionButtons = new List<Buttons>(){Buttons.SHOP, Buttons.APPLY, Buttons.WORK, Buttons.QUIT};
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
    }
}
