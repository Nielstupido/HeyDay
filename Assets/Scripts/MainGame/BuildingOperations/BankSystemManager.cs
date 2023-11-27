using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BankSystemManager : MonoBehaviour
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
    [SerializeField] private Prompts createAccPrompt;
    [SerializeField] private TextMeshProUGUI balanceText;
    [SerializeField] private TMP_InputField depositAmountField;
    [SerializeField] private TMP_InputField withdrawAmountField;
    public static BankSystemManager Instance { get; private set; }


    private void Awake() 
    { 
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    public void OpenBankSystem()
    {
        if (!Player.Instance.IsPlayerHasBankAcc)
        {
            PromptManager.Instance.ShowPrompt(createAccPrompt);
            return;
        }

        bankSystemOverlay.SetActive(true);
    }


    public void CreateSavingsAcc()
    {
        LevelManager.onFinishedPlayerAction(MissionType.OPENSAVINGSACC);
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
            LevelManager.onFinishedPlayerAction(MissionType.DEPOSITSAVINGSACC);
        }

        yield return null;
    }
}
