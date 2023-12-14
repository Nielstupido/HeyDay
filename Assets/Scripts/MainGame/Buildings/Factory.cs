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
        this.buildingDescription = "You can enter the innovative Bene Factory, a manufacturing hub where cutting-edge technology meets efficient production. The factory's exterior reflects a commitment to sustainable and advanced manufacturing.";

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
                    JobManager.Instance.QuitWork();
                    break;
            }
    }


    public override void CheckButtons()
    {
        this.actionButtons = new List<Buttons>(){Buttons.APPLY};

        if (this.currentlyHired && this.buildingEnumName == Player.Instance.CurrentPlayerJob.establishment)
        {
            this.actionButtons.Add(Buttons.WORK);
            this.actionButtons.Add(Buttons.QUIT);    
        }
    }
}
