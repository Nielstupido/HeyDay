using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hospital : Building
{
    private void Start()
    {
        this.buildingStringName = "Heyday Hospital";
        this.buildingEnumName = Buildings.HOSPITAL;
        this.buildingOpeningTime = 0f;
        this.buildingClosingTime = 0f;
        this.totalWorkHours = 0f;
        this.currentlyHired = false;
        this.buildingDescription = "You can visit the bustling hospital, where skilled professionals work tirelessly to heal the sick and injured. Inside, the halls hum with activity, offering care, hope, and comfort to those in need.";

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
                case Buttons.PAY:
                    HospitalManager.Instance.OpenBillingOverlay();
                    break;
            }
    }


    public override void CheckButtons()
    {
        this.actionButtons = new List<Buttons>();

        if (Player.Instance.PlayerHospitalOutstandingDebt != 0f)
        {
            this.actionButtons.Add(Buttons.PAY);
        }

        this.actionButtons.Add(Buttons.APPLY);

        if (this.currentlyHired)
        {
            this.actionButtons.Add(Buttons.WORK);
            this.actionButtons.Add(Buttons.QUIT);     
        }
    }
}
