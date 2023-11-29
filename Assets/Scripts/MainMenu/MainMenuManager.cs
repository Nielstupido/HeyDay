using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
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
    [SerializeField] private GameObject closeBtn;
    [SerializeField] private GameObject leaderboardEntryPrefab;
    [SerializeField] private GameObject leaderboardEntryParent;
    [SerializeField] private Sprite rank1;
    [SerializeField] private Sprite rank2;
    [SerializeField] private Sprite rank3;
    [SerializeField] private Sprite rank4;
    [SerializeField] private Sprite rank5;
    [SerializeField] private Sprite[] rankSpriteImages;

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
        AnimateMainMenu();
        rankSpriteImages = new Sprite[] {rank1, rank2, rank3, rank4, rank5};
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


    public void StartGame()
    {
        MenuObjManager.onGameStart();
        SceneManager.LoadScene("MainGame");
        AudioManager.Instance.StopMusic();
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


    public void DisplayPlayers()
    {
        int index = 0;
        foreach (var savedPlayer in GameDataManager.Instance.PlayerRecords.OrderByDescending(x => x.Value))
        {
            GameObject newEntry = Instantiate(leaderboardEntryPrefab, leaderboardEntryParent.transform);
            LeaderboardTemplate leaderboardEntry = newEntry.GetComponent<LeaderboardTemplate>();

            leaderboardEntry.playerNameText.text = savedPlayer.Key;
            leaderboardEntry.playerScoreText.text = savedPlayer.Value.ToString();
            leaderboardEntry.rankImage.sprite = rankSpriteImages[index];
            index++;
        }
    }


    public void ExitGameDictionary()
    {
        dictionaryOverlay.SetActive(false);
    }
    

    public void ExitSettings()
    {
        settingsPanel.SetActive(false);
        //AudioManager.Instance.PlaySFX("Select");
    }


    public void ExitGame()
    {
        Application.Quit();
    }
}
