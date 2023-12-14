using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityHall : Building
{
    private void Start()
    {
        this.buildingStringName = "Heyday City Hall";
        this.buildingEnumName = Buildings.CITYHALL;
        this.buildingOpeningTime = 8f;
        this.buildingClosingTime = 17f;
        this.buildingDescription = "You can explore the grand HeyDay City Hall, a governance and civic affairs hub. Offices and meeting rooms buzz with discussions on policies, urban development, and community initiatives.";

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
