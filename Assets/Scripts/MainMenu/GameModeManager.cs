using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameModeManager : MonoBehaviour
{
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


    public void LoadGame()
    {
        // PlayerPrefs.SetInt("GameMode", 1); // 1 = load game
    }
}
