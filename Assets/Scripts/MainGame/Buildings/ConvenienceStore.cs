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
        this.buildingDescription = "You can step into the familiar Uncle Ben Convenience Store, a neighborhood hub offering quick essentials and friendly service. The store's warm lighting welcomes patrons day and night.";

        BuildingManager.Instance.onBuildingBtnClicked += CheckBtnClicked;
        GameManager.Instance.MeetupLocBuildings.Add(this);
    }


    private void OnDestroy()
    {
        BuildingManager.Instance.onBuildingBtnClicked -= CheckBtnClicked;
    }
    private void OnEnable()
    {
        BuildingManager.Instance.onBuildingBtnClicked += CheckBtnClicked;
    }
    private void OnDisable()
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
