using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightClub : Building
{
    private void Start()
    {
        this.buildingStringName = "Kapuntukan Night Club";
        this.buildingEnumName = Buildings.NIGHTCLUB;
        this.buildingOpeningTime = 20f;
        this.buildingClosingTime = 5f;
        this.buildingDescription = "You can step into the vibrant nightclub of Kapuntukan, pulsating with music and colorful lights. The dance floor is alive with energy as people unwind and socialize, creating a lively atmosphere of celebration and entertainment.";

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
                case Buttons.PARTY:
                    BuildingManager.Instance.OpenBarOverlay();
                    break;
            }
    }


    public override void CheckButtons()
    {
        this.actionButtons = new List<Buttons>(){Buttons.PARTY};
    }
}
