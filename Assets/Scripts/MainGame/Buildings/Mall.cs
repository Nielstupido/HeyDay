using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mall : Building
{
    private void Start()
    {
        this.buildingStringName = "Laz Mall";
        this.buildingEnumName = Buildings.MALL;
        this.buildingOpeningTime = 10f;
        this.buildingClosingTime = 22f;
        this.buildingDescription = "Welcome to Laz Mall, your one-stop destination for a diverse range of shopping delights. From sleek electronics to home essentials, the wide corridors showcase a variety of stores offering appliances and specialized shop services. The hum of transactions intertwines with the buzz of service providers, painting a dynamic shopping experience.";

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
                case Buttons.SHOP:
                    BuildingManager.Instance.OpenMallOverlay();
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
