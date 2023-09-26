using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bank : Building
{
    [SerializeField] private GameObject menuOverlay;
    [SerializeField] private GameObject depositOverlay;
    [SerializeField] private GameObject withdrawOverlay;
    [SerializeField] private GameObject balanceOverlay;
    [SerializeField] private TMP_InputField depositAmountField;
    [SerializeField] private TMP_InputField withdrawAmountField;



    private void Start()
    {
        buildingName = Buildings.BANK;
        actionButtons = new List<Buttons>(){Buttons.DEPOSITMONEY, Buttons.APPLY, Buttons.WORK, Buttons.QUIT};
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
                case Buttons.DEPOSITMONEY:
                    Debug.Log("money deposited");
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


    public void OpenDepositMenu()
    {
        depositOverlay.SetActive(true);
    }


    public void OpenWithdrawMenu()
    {
        withdrawOverlay.SetActive(true);
    }


    public void OpenBalanceMenu()
    {
        balanceOverlay.SetActive(true);
    }


    public void CancelTransaction()
    {
        if (depositOverlay.activeSelf)
        {
            depositOverlay.SetActive(false);
        }
        else if (withdrawOverlay.activeSelf)
        {
            withdrawOverlay.SetActive(false);
        }
        else if (balanceOverlay.activeSelf)
        {
            balanceOverlay.SetActive(false);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }


    public void DepositMoney()
    {
        Debug.Log(int.Parse(depositAmountField.text).ToString());
    }


    public void WithdrawMoney()
    {
        Debug.Log(int.Parse(withdrawAmountField.text).ToString());
    }


    public void CheckBalance()
    {
        
    }
}
