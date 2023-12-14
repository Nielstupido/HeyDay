using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cinema : Building
{
    private void Start()
    {
        this.buildingStringName = "Heyday Cinema";
        this.buildingEnumName = Buildings.CINEMA;
        this.buildingOpeningTime = 13f;
        this.buildingClosingTime = 23f;
        this.buildingDescription = "You can step into the vibrant cinema, where movie posters and the scent of popcorn fill the air. People around you chatter in anticipation of the films about to unravel on the big screen. It's a lively escape, inviting you to immerse yourself in captivating stories and temporary adventures.";

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
                case Buttons.WATCHMOVIE:
                    BuildingManager.Instance.OpenCinemaOverlay();
                    break;
            }
    }


    public override void CheckButtons()
    {
        this.actionButtons = new List<Buttons>(){Buttons.WATCHMOVIE};     
    }
}
