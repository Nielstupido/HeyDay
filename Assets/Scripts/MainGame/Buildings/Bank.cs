using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bank : Building
{
    [SerializeField] private GameObject bankSystemOverlay;
    [SerializeField] private GameObject menuOverlay;
    [SerializeField] private GameObject depositOverlay;
    [SerializeField] private GameObject depositProcessOverlay;
    [SerializeField] private GameObject depositProcessingOverlay;
    [SerializeField] private GameObject depositProcessedOverlay;
    [SerializeField] private GameObject withdrawOverlay;
    [SerializeField] private GameObject withdrawProcessOverlay;
    [SerializeField] private GameObject withdrawProcessingOverlay;
    [SerializeField] private GameObject withdrawProcessedOverlay;
    [SerializeField] private GameObject balanceOverlay;
    [SerializeField] private TextMeshProUGUI balanceText;

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


    public void OpenBankSystem()
    {
        bankSystemOverlay.SetActive(true);
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
        CheckBalance();
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
            bankSystemOverlay.SetActive(false);
        }
    }


    public void WithdrawMoney()
    {
        StartCoroutine(ProcessTransaction(1));
    }


    public void DepositMoney()
    {
        StartCoroutine(ProcessTransaction(2));
    }


    public void CheckBalance()
    {
        balanceText.text = Player.Instance.PlayerBankSavings.ToString();
    }


    private IEnumerator ProcessTransaction(int transacType)
    {
        if (transacType == 1)
        {
            if (float.Parse(withdrawAmountField.text) > Player.Instance.PlayerBankSavings)
            {
                //error
            }

            Debug.Log("cash = " + Player.Instance.PlayerCash.ToString());
            Debug.Log("bank = " + Player.Instance.PlayerBankSavings.ToString());
            withdrawProcessOverlay.SetActive(true);
            yield return new WaitForSeconds(2f);
            withdrawProcessingOverlay.SetActive(false);
            withdrawProcessedOverlay.SetActive(true);
            yield return new WaitForSeconds(2f);

            Player.Instance.PlayerCash += float.Parse(withdrawAmountField.text);
            Player.Instance.PlayerBankSavings -= float.Parse(withdrawAmountField.text);

            withdrawOverlay.SetActive(false);
            withdrawProcessedOverlay.SetActive(false);
            withdrawProcessingOverlay.SetActive(true);
            withdrawProcessedOverlay.SetActive(false);
            Debug.Log("cash = " + Player.Instance.PlayerCash.ToString());
            Debug.Log("bank = " + Player.Instance.PlayerBankSavings.ToString());
        }
        else if (transacType == 2)
        {
            if (float.Parse(depositAmountField.text) > Player.Instance.PlayerCash)
            {
                //error
            }

            Debug.Log("cash = " + Player.Instance.PlayerCash.ToString());
            Debug.Log("bank = " + Player.Instance.PlayerBankSavings.ToString());
            depositProcessOverlay.SetActive(true);
            depositProcessingOverlay.SetActive(true);
            yield return new WaitForSeconds(2f);
            depositProcessingOverlay.SetActive(false);
            depositProcessedOverlay.SetActive(true);
            yield return new WaitForSeconds(2f);

            Player.Instance.PlayerCash -= float.Parse(depositAmountField.text);
            Player.Instance.PlayerBankSavings += float.Parse(depositAmountField.text);

            depositOverlay.SetActive(false);
            depositProcessOverlay.SetActive(true);
            depositProcessingOverlay.SetActive(true);
            depositProcessedOverlay.SetActive(false);
            Debug.Log("cash = " + Player.Instance.PlayerCash.ToString());
            Debug.Log("bank = " + Player.Instance.PlayerBankSavings.ToString());
        }

        yield return null;
    }
}
