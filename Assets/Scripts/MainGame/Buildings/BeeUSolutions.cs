using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeUSolutions : Building
{
    private void Start()
    {
        this.buildingStringName = "Bee U Solutions";
        this.buildingEnumName = Buildings.BEEUSOLUTIONS;
        this.buildingOpeningTime = 8f;
        this.buildingClosingTime = 17f;
        this.buildingDescription = "You can visit the cutting-edge Bee U IT Solutions, a tech hub where innovation and creativity thrive. The sleek building exterior reflects a commitment to pushing the boundaries of technology.";

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

        if (this.currentlyHired)
        {
            this.actionButtons.Add(Buttons.WORK);
            this.actionButtons.Add(Buttons.QUIT);  
        }
    }
}
