using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachineResults : MonoBehaviour
{
    [SerializeField] private GameObject winPrompt;
    [SerializeField] private Text winAmount;
    [SerializeField] private GameObject losePrompt;
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
        if (amount > Player.Instance.PlayerCash)
        {
            PromptManager.Instance.ShowPrompt(notEnoughMoneyPrompt);
            return false;
        }

        TimeManager.Instance.AddClockTime(timeAdded);
        Player.Instance.PlayerCash -= amount;
        Player.Instance.PlayerStatsDict[PlayerStats.MONEY] -= amount;
        PlayerStatsObserver.onPlayerStatChanged(PlayerStats.ALL, Player.Instance.PlayerStatsDict);
        
        return true;
    }


    public void Play()
    {
        if (Pay(5f, 100000f, 0.5f))
        {
            slotMachine1.StartRand();
            slotMachine2.StartRand();
            slotMachine3.StartRand();
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
                winAmount.text = "1,000";
                Player.Instance.PlayerCash += 1000;
                StartCoroutine(ShowPrompt(1.5f));
            }
            else if (matchCount == 3)
            {
                winAmount.text = "100,000";
                Player.Instance.PlayerCash += 100000;
                StartCoroutine(ShowPrompt(1.5f));
            }
            else
            {
                StartCoroutine(ShowPrompt(1.5f));
            }
        }
        StartCoroutine(ClosePrompt(2.5f));
    }


    private IEnumerator ShowPrompt(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (matchCount == 0)
        {
            losePrompt.SetActive(true);
        }
        else
        {
            Player.Instance.PlayerStatsDict[PlayerStats.MONEY] = Player.Instance.PlayerCash;
            PlayerStatsObserver.onPlayerStatChanged(PlayerStats.ALL, Player.Instance.PlayerStatsDict);
            winPrompt.SetActive(true);
        }
    }


    private IEnumerator ClosePrompt(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        
        winPrompt.SetActive(false);
        losePrompt.SetActive(false);
    }
}
