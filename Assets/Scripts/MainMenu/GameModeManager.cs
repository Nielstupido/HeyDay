using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameModeManager : MonoBehaviour
{
    [SerializeField] private GameObject noSavedGamesSign;
    [SerializeField] private GameObject savedGamesOverlay;
    [SerializeField] private GameObject savedGamePrefab;
    [SerializeField] private GameObject deleteConfirmationOverlay;
    [SerializeField] private GameObject deleteConfirmationPanel;
    [SerializeField] private Transform savedGamesHolder;
    private string playerNameToDelete;
    public static GameModeManager Instance { get; private set; }


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


    private void StartGame()
    {
        MenuObjManager.onGameStart();
        AudioManager.Instance.StopMusic();
        GameLoader.Instance.LoadGameScene();
    }


    public void NewGame()
    {
        AudioManager.Instance.PlaySFX("Select");
        PlayerPrefs.SetInt("GameMode", 0); // 0 = new game
        StartGame();
    }


    public void LoadGame(string name)
    {
        PlayerPrefs.SetInt("GameMode", 1); // 1 = load game
        PlayerPrefs.SetString("PlayerName", name);
        StartGame();
    }


    public void CloseSavedGames()
    {
        savedGamesOverlay.SetActive(false);
    }


    public void ShowSavedGames()
    {
        AudioManager.Instance.PlaySFX("Select");
        savedGamesOverlay.SetActive(true);
        DisplaySavedGames();
    }


    public void DeleteGameSave(string playerName)
    {
        playerNameToDelete = playerName;
        deleteConfirmationOverlay.GetComponent<RectTransform>().localScale = Vector3.zero;
        LeanTween.scale(deleteConfirmationOverlay, Vector3.one, 0.4f).setEaseOutElastic();
        deleteConfirmationOverlay.SetActive(true);
        deleteConfirmationPanel.SetActive(true);
    }


    public void ConfirmDeletion()
    {
        GameDataManager.Instance.AllPlayersGameStateData.Remove(playerNameToDelete);
        GameDataManager.Instance.SaveGameData();
        CloseConfirmDeletionOverlay();
        DisplaySavedGames();
    }


    public void CloseConfirmDeletionOverlay()
    {
        LeanTween.scale(deleteConfirmationOverlay, Vector3.zero, 0.4f).setEaseInBack().setOnComplete(HideDeletionOverlay);
    }


    private void HideDeletionOverlay()
    {
        deleteConfirmationOverlay.SetActive(false);
        deleteConfirmationPanel.SetActive(false);
    }


    private void DisplaySavedGames()
    {
        for (int i = 0; i < savedGamesHolder.childCount; i++)
        {
            Object.Destroy(savedGamesHolder.GetChild(i).gameObject);
        }

        if (GameDataManager.Instance.AllPlayersGameStateData.Keys.Count == 0)
        {
            noSavedGamesSign.SetActive(true);
            return;
        }
        noSavedGamesSign.SetActive(false);

        foreach (var savedGame in GameDataManager.Instance.AllPlayersGameStateData)
        {
            GameObject newEntry = Instantiate(savedGamePrefab, savedGamesHolder);
            SavedGameObj leaderboardEntry = newEntry.GetComponent<SavedGameObj>();
            leaderboardEntry.SetupSavedGames(savedGame.Key);
        }
    }
}
