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

        this.actionButtons = new List<Buttons>(){Buttons.OPENSAVINGSACCOUNT, Buttons.ACCESSSAVINGSACCOUNT};
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
                case Buttons.OPENSAVINGSACCOUNT:
                    BankSystemManager.Instance.CreateSavingsAcc();
                    break;
                case Buttons.ACCESSSAVINGSACCOUNT:
                    BankSystemManager.Instance.OpenBankSystem();
                    break;
                case Buttons.APPLY:
                    Debug.Log("money deposited");
                    break;
                case Buttons.WORK:
                    Debug.Log("money deposited");
                    break;
                case Buttons.QUIT:
                    Debug.Log("money deposited");
                    break;
            }
    }


    public override void CheckButtons()
    {
        if (this.currentlyHired)
        {
            this.actionButtons.Add(Buttons.WORK);
            this.actionButtons.Add(Buttons.QUIT);   
        }
        else
        {
            this.actionButtons.Add(Buttons.APPLY);   
        }
    }
}
