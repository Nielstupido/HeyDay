using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hospital : Building
{
    private void Start()
    {
        this.buildingStringName = "Heyday Hospital";
        this.buildingEnumName = Buildings.HOSPITAL;
        this.buildingOpeningTime = 0f;
        this.buildingClosingTime = 0f;
        this.totalWorkHours = 0f;
        this.currentlyHired = false;

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
                    JobManager.Instance.Apply(this);
                    break;
                case Buttons.WORK:
                    JobManager.Instance.Work();
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
