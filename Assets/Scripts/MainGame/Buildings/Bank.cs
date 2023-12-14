using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bank : Building
{
    private void Start()
    {
        this.buildingStringName = "Intelli Cash Bank";
        this.buildingEnumName = Buildings.BANK;
        this.buildingOpeningTime = 8f;
        this.buildingClosingTime = 16f;
        this.buildingDescription = "You can enter the secure Intellicash Bank, a financial institution where modern architecture meets trust and reliability. The bank stands tall, symbolizing a pillar of financial stability in the city.";

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
                case Buttons.OPENSAVINGSACCOUNT:
                    BankSystemManager.Instance.CreateSavingsAcc();
                    break;
                case Buttons.ACCESSSAVINGSACCOUNT:
                    BankSystemManager.Instance.OpenBankSystem();
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
        this.actionButtons = new List<Buttons>(){Buttons.OPENSAVINGSACCOUNT, Buttons.ACCESSSAVINGSACCOUNT, Buttons.APPLY};

        if (Player.Instance.IsPlayerHasBankAcc)
        {
            this.actionButtons.Remove(Buttons.OPENSAVINGSACCOUNT);   
        }

        if (this.currentlyHired)
        {
            this.actionButtons.Add(Buttons.WORK);
            this.actionButtons.Add(Buttons.QUIT);   
        }
    }
}
