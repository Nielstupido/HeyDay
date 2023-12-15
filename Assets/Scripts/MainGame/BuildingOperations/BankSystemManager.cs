using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BankSystemManager : MonoBehaviour
{
    [SerializeField] private GameObject bankSystemOverlay;
    [SerializeField] private GameObject bankSystemMenu;
    [SerializeField] private GameObject depositOverlay;
    [SerializeField] private GameObject depositMenu;
    [SerializeField] private GameObject depositProcessOverlay;
    [SerializeField] private GameObject depositProcessingOverlay;
    [SerializeField] private GameObject depositProcessedOverlay;
    [SerializeField] private GameObject withdrawOverlay;
    [SerializeField] private GameObject withdrawMenu;
    [SerializeField] private GameObject withdrawProcessOverlay;
    [SerializeField] private GameObject withdrawProcessingOverlay;
    [SerializeField] private GameObject withdrawProcessedOverlay;
    [SerializeField] private GameObject balanceOverlay;
    [SerializeField] private GameObject balPopUp;
    [SerializeField] private GameObject createAccountOverlay;
    [SerializeField] private GameObject createAccountPopUp;
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
        AudioManager.Instance.PlaySFX("Select");
        if (!Player.Instance.IsPlayerHasBankAcc)
        {
            PromptManager.Instance.ShowPrompt(createAccPrompt);
            return;
        }

        bankSystemOverlay.SetActive(true);
        OverlayAnimations.Instance.AnimOpenOverlay(bankSystemMenu);
    }


    public void CreateSavingsAcc()
    {
        AudioManager.Instance.PlaySFX("Select");
        createAccountOverlay.SetActive(true);
        OverlayAnimations.Instance.AnimOpenOverlay(createAccountPopUp);
    }


    public void OnCreateSavingsAcc()
    {
        AudioManager.Instance.PlaySFX("Select");
        Player.Instance.IsPlayerHasBankAcc = true;
        BuildingManager.Instance.PrepareButtons(BuildingManager.Instance.CurrentSelectedBuilding);
        LevelManager.onFinishedPlayerAction(MissionType.OPENSAVINGSACC);
        PromptManager.Instance.ShowPrompt(justCreatedAccPrompt);
        createAccountOverlay.SetActive(false);
        OverlayAnimations.Instance.AnimCloseOverlay(createAccountPopUp, createAccountOverlay);
    }


    public void OpenDepositMenu()
    {
        AudioManager.Instance.PlaySFX("Select");
        depositOverlay.SetActive(true);
        OverlayAnimations.Instance.AnimOpenOverlay(depositMenu);
    }


    public void OpenWithdrawMenu()
    {
        AudioManager.Instance.PlaySFX("Select");
        withdrawOverlay.SetActive(true);
        OverlayAnimations.Instance.AnimOpenOverlay(withdrawMenu);
    }


    public void OpenBalanceMenu()
    {
        AudioManager.Instance.PlaySFX("Select");
        balanceOverlay.SetActive(true);
        OverlayAnimations.Instance.AnimOpenOverlay(balPopUp);
        CheckBalance();
    }

    public void BankBtnClicked()
    {
        AudioManager.Instance.PlaySFX("Select");
    }


    public void CancelTransaction()
    {
        AudioManager.Instance.PlaySFX("Select");
        if (depositOverlay.activeSelf)
        {
            depositOverlay.SetActive(false);
            OverlayAnimations.Instance.AnimCloseOverlay(depositMenu, depositOverlay);
        }
        else if (withdrawOverlay.activeSelf)
        {
            withdrawOverlay.SetActive(false);
            OverlayAnimations.Instance.AnimCloseOverlay(withdrawMenu, withdrawOverlay);
        }
        else if (balanceOverlay.activeSelf)
        {
            balanceOverlay.SetActive(false);
            OverlayAnimations.Instance.AnimCloseOverlay(balPopUp, balanceOverlay);
        }
        else
        {
            bankSystemOverlay.SetActive(false);
            OverlayAnimations.Instance.AnimCloseOverlay(bankSystemMenu, bankSystemOverlay);
        }
    }


    public void WithdrawMoney()
    {
        AudioManager.Instance.PlaySFX("Select");
        if (float.Parse(withdrawAmountField.text) > Player.Instance.PlayerBankSavings)
        {
            PromptManager.Instance.ShowPrompt(withdrawError);
            return;
        }
        StartCoroutine(ProcessTransaction(1));
    }


    public void DepositMoney()
    {
        AudioManager.Instance.PlaySFX("Select");
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
