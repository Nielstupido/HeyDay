using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameModeManager : MonoBehaviour
{
    [SerializeField] private GameObject noSavedGamesSign;
    [SerializeField] private GameObject savedGamesOverlay;
    [SerializeField] private GameObject savedGamePrefab;
    [SerializeField] private Transform savedGamesHolder;
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
        SceneManager.LoadScene("MainGame");
    }


    public void NewGame()
    {
        AnimOverlayManager.Instance.StartScreenFadeLoadScreen(5f);
        PlayerPrefs.SetInt("GameMode", 0); // 0 = new game
        StartGame();
    }


    public void LoadGame(string name)
    {
        AnimOverlayManager.Instance.StartScreenFadeLoadScreen(5f);
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
        // PlayerPrefs.SetInt("GameMode", 1); // 1 = load game

        savedGamesOverlay.SetActive(true);
        DisplaySavedGames();
    }


    private void DisplaySavedGames()
    {
        if (GameDataManager.Instance.GetAllGameStateData().Keys.Count == 0)
        {
            noSavedGamesSign.SetActive(true);
            return;
        }
        noSavedGamesSign.SetActive(false);

        for (int i = 0; i < savedGamesHolder.childCount; i++)
        {
            Object.Destroy(savedGamesHolder.GetChild(i).gameObject);
        }

        foreach (var savedGame in GameDataManager.Instance.GetAllGameStateData())
        {
            GameObject newEntry = Instantiate(savedGamePrefab, savedGamesHolder);
            SavedGameObj leaderboardEntry = newEntry.GetComponent<SavedGameObj>();
            leaderboardEntry.SetupSavedGames(savedGame.Key);
        }
    }
}
