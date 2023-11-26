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
                case Buttons.WATCHMOVIE:
                    Debug.Log("WATCHED MOVIE");
                    break;
            }
    }


    public override void CheckButtons()
    {
        this.actionButtons = new List<Buttons>(){Buttons.WATCHMOVIE};     
    }
}
