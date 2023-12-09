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

        BuildingManager.Instance.onBuildingBtnClicked += CheckBtnClicked;
        GameManager.Instance.MeetupLocBuildings.Add(this);
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
                    BuildingManager.Instance.OpenConsumablesOverlay();
                    break;
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
        this.actionButtons = new List<Buttons>(){Buttons.BUY, Buttons.APPLY};

        if (this.currentlyHired)
        {
            this.actionButtons.Add(Buttons.WORK);
            this.actionButtons.Add(Buttons.QUIT);   
        }
    }
}
