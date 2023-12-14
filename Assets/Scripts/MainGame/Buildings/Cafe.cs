using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cafe : Building
{
    private void Start()
    {
        this.buildingStringName = "DDoS Cafe";
        this.buildingEnumName = Buildings.CAFE;
        this.buildingOpeningTime = 10f;
        this.buildingClosingTime = 22f;
        this.buildingDescription = "You can step into the dynamic Ddos Cafe, a tech-inspired haven where the aroma of coffee mingles with the hum of devices. Modern decor and high-speed internet create an atmosphere perfect for work or relaxation.";

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

        if (this.currentlyHired && this.buildingEnumName == Player.Instance.CurrentPlayerJob.establishment)
        {
            this.actionButtons.Add(Buttons.WORK);
            this.actionButtons.Add(Buttons.QUIT);   
        }
    }
}
