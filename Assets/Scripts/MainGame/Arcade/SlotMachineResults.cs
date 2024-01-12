using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachineResults : MonoBehaviour
{
    [SerializeField] private GameObject winPrompt;
    [SerializeField] private GameObject winPopUp;
    [SerializeField] private Text winAmount;
    [SerializeField] private GameObject losePrompt;
    [SerializeField] private GameObject losePopUp;
    [SerializeField] private Prompts notEnoughMoneyPrompt;
    [SerializeField] private SlotMachine slotMachine1;
    [SerializeField] private SlotMachine slotMachine2;
    [SerializeField] private SlotMachine slotMachine3;
    public List<Sprite> results = new List<Sprite>();
    public static SlotMachineResults Instance { get; private set; }
    int matchCount = 0;


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


    private bool Pay(float energyLevelCutValue, float amount, float timeAdded)
    {
        return Player.Instance.Pay(false, amount, 15f, timeAdded, 2f, notEnoughMoneyPrompt, energyLevelCutValue);
    }


    public void Play()
    {
        if (Pay(5f, 200f, 0.5f))
        {
            Player.Instance.PlayerExpensesArcade += 200f;
            slotMachine1.StartRand();
            slotMachine2.StartRand();
            slotMachine3.StartRand();
            AudioManager.Instance.PlaySFX("Pay");
        }
    }


    public void CheckForMatches()
    {
        if (results.Count == 3)
        {
            for (int i = 0; i < results.Count; i++)
            {
                for (int j = i + 1; j < results.Count; j++)
                {
                    if (results[i] == results[j])
                    {
                        matchCount++;
                    }
                }
            }
            Debug.Log("matches: " + matchCount);


            if (matchCount == 1)
            {
                AudioManager.Instance.PlaySFX("Payout");
                winAmount.text = "100";
                Player.Instance.PlayerCash += 100;
                StartCoroutine(ShowPrompt(1.5f));
            }
            else if (matchCount == 3)
            {
                AudioManager.Instance.PlaySFX("Payout");
                winAmount.text = "100,000";
                Player.Instance.PlayerCash += 100000;
                StartCoroutine(ShowPrompt(1.5f));
            }
            else
            {
                StartCoroutine(ShowPrompt(1.5f));
            }
        }
        StartCoroutine(ClosePrompt(4f));
    }


    private IEnumerator ShowPrompt(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (matchCount == 0)
        {
            losePrompt.SetActive(true);
            OverlayAnimations.Instance.AnimOpenOverlay(losePopUp);
        }
        else
        {
            Player.Instance.PlayerStatsDict[PlayerStats.MONEY] = Player.Instance.PlayerCash;
            PlayerStatsObserver.onPlayerStatChanged(PlayerStats.ALL, Player.Instance.PlayerStatsDict);
            winPrompt.SetActive(true);
            OverlayAnimations.Instance.AnimOpenOverlay(winPopUp);
            matchCount = 0;
        }
    }


    private IEnumerator ClosePrompt(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        
        AudioManager.Instance.StopSFX();
        winPrompt.SetActive(false);
        OverlayAnimations.Instance.AnimCloseOverlay(winPopUp, winPrompt);
        losePrompt.SetActive(false);
        OverlayAnimations.Instance.AnimCloseOverlay(losePopUp, losePrompt);
    }
}
