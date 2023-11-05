using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachineResults : MonoBehaviour
{
    [SerializeField] private GameObject winPrompt;
    [SerializeField] private Text winAmount;
    [SerializeField] private GameObject losePrompt;
    public List<Sprite> results = new List<Sprite>();
    public static SlotMachineResults Instance { get; private set; }
    int matchCount = 0;

    private void Awake() 
    { 
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    public void CheckForMatches()
    {
        //Player.Instance.Purchase(5f,100f,0.5f);
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
