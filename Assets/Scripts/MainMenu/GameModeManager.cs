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


    private void StartGame()
    {
        MenuObjManager.onGameStart();
        SceneManager.LoadScene("MainGame");
        AudioManager.Instance.StopMusic();
    }


    public void NewGame()
    {
        PlayerPrefs.SetInt("GameMode", 0); // 0 = new game
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
        if (GameDataManager.Instance.GetAllGameStateData().Count == 0)
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
            leaderboardEntry.SetupSavedGames(savedGame.playerName);
        }
    }
}
