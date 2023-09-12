using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cinema : Building
{
    private void Start()
    {
        buildingName = Buildings.CINEMA;
        actionButtons = new List<Buttons>(){Buttons.WATCHMOVIE};     
        BuildingManager.Instance.onBuildingBtnClicked += CheckBtnClicked;
    }


    private void OnDestroy()
    {
        BuildingManager.Instance.onBuildingBtnClicked -= CheckBtnClicked;
    }


    public override void CheckBtnClicked(Buttons clickedBtn)
    {
        if (BuildingManager.Instance.CurrentSelectedBuilding.buildingName == this.buildingName)
            switch (clickedBtn)
            {
                case Buttons.WATCHMOVIE:
                    Debug.Log("WATCHED MOVIE");
                    break;
            }
    }
}
