using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject startBtn;
    [SerializeField] private GameObject exitBtn;
    [SerializeField] private GameObject dictionaryBtn;
    [SerializeField] private GameObject dictionaryOverlay;
    [SerializeField] private GameObject settingsBtn;
    [SerializeField] private GameObject mechanicsBtn;
    [SerializeField] private GameObject creditsBtn;
    [SerializeField] private GameObject leaderboardsBtn;
    [SerializeField] private GameObject mechanicsPanel;
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject leaderboardsPanel;
    [SerializeField] private GameObject gameModePanel;
    [SerializeField] private GameObject leaderboardEntryPrefab;
    [SerializeField] private GameObject leaderboardEntryParent;
    [SerializeField] private Prompts errorLoadingPlayerRecord;

    public static MainMenuManager Instance { get; private set; }


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


    private void Start()
    {
        if (!GameDataManager.Instance.LoadPlayerRecords().Item1)
        {
            errorLoadingPlayerRecord.promptContent = GameDataManager.Instance.LoadPlayerRecords().Item2;
            PromptManager.Instance.ShowPrompt(errorLoadingPlayerRecord);
        }
        
        AnimateMainMenu();
        var res = GameDataManager.Instance.LoadAllGameStateData();
        AudioManager.Instance.PlayMusic("Theme");
    }


    private void AnimateMainMenu()
    {
        LeanTween.moveX(exitBtn, (float)(Screen.width / 5), 1.2f).setEaseOutElastic().delay = 0.15f;
        LeanTween.moveX(leaderboardsBtn, (float)(Screen.width / 5), 1.2f).setEaseOutElastic().delay = 0.15f;
        LeanTween.moveX(creditsBtn, (float)(Screen.width / 5), 1.2f).setEaseOutElastic().delay = 0.2f;
        LeanTween.moveX(dictionaryBtn, (float)(Screen.width / 5), 1.2f).setEaseOutElastic().delay = 0.25f;
        LeanTween.moveX(mechanicsBtn, (float)(Screen.width / 5), 1.2f).setEaseOutElastic().delay = 0.3f;
        LeanTween.moveX(startBtn, (float)(Screen.width / 5), 1.2f).setEaseOutElastic().setOnComplete(StartSpawn).delay = 0.4f;
    }


    private void StartSpawn()
    {
        MenuObjManager.onBtnSet();
    }


    public void ShowGameOverlay()
    {
        GameDataManager.Instance.LoadAllGameStateData();
        gameModePanel.SetActive(true);
        AudioManager.Instance.PlaySFX("Select");
    }


    public void ShowGameMechanics()
    {
        mechanicsPanel.SetActive(true);
        AudioManager.Instance.PlaySFX("Select");
    }


    public void ShowGameCredits()
    {
        creditsPanel.SetActive(true);
        AudioManager.Instance.PlaySFX("Select");
    }


    public void ShowSettings()
    {
        settingsPanel.SetActive(true);
        AudioManager.Instance.PlaySFX("Select");
    }


    public void ShowGameDictionary()
    {
        dictionaryOverlay.SetActive(true);
    }


    public void ShowLeaderboards()
    {
        DisplayPlayers();
        leaderboardsPanel.SetActive(true);
        SequenceAnimator.Instance.EnableAnimation();
    }


    private void DisplayPlayers()
    {
        for (int i = 0; i < leaderboardEntryParent.transform.childCount; i++)
        {
            Object.Destroy(leaderboardEntryParent.transform.GetChild(i).gameObject);
        }

        int index = 0;
        foreach (var savedPlayer in GameDataManager.Instance.GetCurrentLevelScores())
        {
            GameObject newEntry = Instantiate(leaderboardEntryPrefab, leaderboardEntryParent.transform);
            LeaderboardTemplate leaderboardEntry = newEntry.GetComponent<LeaderboardTemplate>();
            leaderboardEntry.SetPlayerInfo(savedPlayer.Key, ++index, savedPlayer.Value);
            index++;
        }
    }


    public void ExitGameDictionary()
    {
        dictionaryOverlay.SetActive(false);
        AudioManager.Instance.PlaySFX("Select");
    }


    public void CloseGameOverlay()
    {
        gameModePanel.SetActive(false);
        AudioManager.Instance.PlaySFX("Select");
    }


    public void ExitSettings()
    {
        settingsPanel.SetActive(false);
        AudioManager.Instance.PlaySFX("Select");
    }


    public void ExitGame()
    {
        AudioManager.Instance.PlaySFX("Select");
        Application.Quit();
    }
}
