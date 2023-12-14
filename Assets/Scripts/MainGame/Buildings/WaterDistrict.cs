using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDistrict : Building
{
    private void Start()
    {
        this.buildingStringName = "Heyday Water District";
        this.buildingEnumName = Buildings.WATERDISTRICT;
        this.buildingOpeningTime = 8f;
        this.buildingClosingTime = 17f;
        this.buildingDescription = "You have the opportunity to explore the HeyDay Water District, a haven for the management and preservation of water resources.   With state-of-the-art purification systems and flourishing vegetation, the establishment demonstrates its dedication to sustainable water management.";

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
