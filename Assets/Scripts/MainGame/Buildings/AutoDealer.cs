using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDealer : Building
{
    private void Start()
    {
        this.buildingStringName = "Auto Dealer";
        this.buildingEnumName = Buildings.AUTODEALER;
        this.buildingOpeningTime = 8f;
        this.buildingClosingTime = 16f;
        this.buildingDescription = "You have the opportunity to discover the lively Beep Beep Auto Dealer, where stylish vehicles and the aroma of fresh leather permeate the atmosphere. The exhibition area displays the most recent advancements and creativity in the automobile industry. Additionally, it offers a selection of affordable used cars.";

        BuildingManager.Instance.onBuildingBtnClicked += CheckBtnClicked;
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
                case Buttons.SHOP:
                    BuildingManager.Instance.OpenCarCatalogueOverlay();
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
        this.actionButtons = new List<Buttons>(){Buttons.SHOP, Buttons.APPLY};

        if (this.currentlyHired)
        {
            this.actionButtons.Add(Buttons.WORK);
            this.actionButtons.Add(Buttons.QUIT);   
        }
    }
}
