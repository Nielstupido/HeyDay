using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodXpress : Building
{
    private void Start()
    {
        this.buildingStringName = "Food Xpress";
        this.buildingEnumName = Buildings.FOODXPRESS;
        this.buildingOpeningTime = 0f;
        this.buildingClosingTime = 0f;
        this.buildingDescription = "You can savor the hustle and bustle of FoodXpress Fastfood, where enticing aromas and quick service define the fast-paced dining experience. Colorful signage and a lively atmosphere make it a popular spot.";

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
