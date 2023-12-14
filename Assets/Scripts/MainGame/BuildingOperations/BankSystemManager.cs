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
    [SerializeField] private GameObject createAccountOverlay;
    [SerializeField] private Prompts createAccPrompt;
    [SerializeField] private Prompts justCreatedAccPrompt;
    [SerializeField] private TextMeshProUGUI balanceText;
    [SerializeField] private TMP_InputField depositAmountField;
    [SerializeField] private TMP_InputField withdrawAmountField;
    [SerializeField] private Prompts withdrawError;
    [SerializeField] private Prompts depositError;
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
        createAccountOverlay.SetActive(true);
    }


    public void OnCreateSavingsAcc()
    {
        Player.Instance.IsPlayerHasBankAcc = true;
        BuildingManager.Instance.PrepareButtons(BuildingManager.Instance.CurrentSelectedBuilding);
        LevelManager.onFinishedPlayerAction(MissionType.OPENSAVINGSACC);
        PromptManager.Instance.ShowPrompt(justCreatedAccPrompt);
        createAccountOverlay.SetActive(false);
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
        if (float.Parse(withdrawAmountField.text) > Player.Instance.PlayerBankSavings)
        {
            PromptManager.Instance.ShowPrompt(withdrawError);
            return;
        }
        StartCoroutine(ProcessTransaction(1));
    }


    public void DepositMoney()
    {
        if (float.Parse(depositAmountField.text) > Player.Instance.PlayerCash)
        {
            PromptManager.Instance.ShowPrompt(depositError);
            return;
        }
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
            withdrawProcessOverlay.SetActive(true);
            withdrawProcessingOverlay.SetActive(true);
            yield return new WaitForSeconds(2f);
            withdrawProcessingOverlay.SetActive(false);
            withdrawProcessedOverlay.SetActive(true);
            yield return new WaitForSeconds(2f);

            Player.Instance.PlayerCash += float.Parse(withdrawAmountField.text);
            Player.Instance.PlayerBankSavings -= float.Parse(withdrawAmountField.text);
            Player.Instance.PlayerLvlEmergencyFunds -= float.Parse(withdrawAmountField.text);
            Player.Instance.PlayerLvlSavings -= float.Parse(withdrawAmountField.text);

            withdrawOverlay.SetActive(false);
            withdrawProcessOverlay.SetActive(true);
            withdrawProcessingOverlay.SetActive(true);
            withdrawProcessedOverlay.SetActive(false);
        }
        else if (transacType == 2)
        {
            depositProcessOverlay.SetActive(true);
            depositProcessingOverlay.SetActive(true);
            yield return new WaitForSeconds(2f);
            depositProcessingOverlay.SetActive(false);
            depositProcessedOverlay.SetActive(true);
            yield return new WaitForSeconds(2f);

            Player.Instance.PlayerCash -= float.Parse(depositAmountField.text);
            Player.Instance.PlayerBankSavings += float.Parse(depositAmountField.text);
            Player.Instance.PlayerLvlEmergencyFunds += float.Parse(depositAmountField.text);
            Player.Instance.PlayerLvlSavings += float.Parse(depositAmountField.text);

            depositOverlay.SetActive(false);
            depositProcessOverlay.SetActive(true);
            depositProcessingOverlay.SetActive(true);
            depositProcessedOverlay.SetActive(false);
            LevelManager.onFinishedPlayerAction(MissionType.DEPOSITSAVINGSACC);
        }

        Player.Instance.PlayerStatsDict[PlayerStats.MONEY] = Player.Instance.PlayerCash;
        PlayerStatsObserver.onPlayerStatChanged(PlayerStats.ALL, Player.Instance.PlayerStatsDict);
        
        yield return null;
    }
}
