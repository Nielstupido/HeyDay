using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Sprite musicOff;
    [SerializeField] private Sprite sfxOff;
    [SerializeField] private Sprite musicOn;
    [SerializeField] private Sprite sfxOn;
    [SerializeField] private Button musicBtn;
    [SerializeField] private Button sfxBtn;
    [SerializeField] private GameObject pauseMenuOverlay;
    [SerializeField] private GameObject pauseMenuPopUp;
    [SerializeField] private GameObject gameSavingOverlay;
    [SerializeField] private GameObject gameSavedOverlay;

    private bool isOnMusic = true;
    private bool isOnSFX = true;


    private IEnumerator DirectingHome()
    {
        AnimOverlayManager.Instance.StartScreenFadeLoadScreen();
        yield return new WaitForSeconds(1.8f);
        SceneManager.LoadScene("MainMenu");
        yield return null;
    }


    public void ShowPauseMenu()
    {
        GameManager.onSaveGameStateData();
        pauseMenuOverlay.SetActive(true);
        OverlayAnimations.Instance.AnimOpenOverlay(pauseMenuPopUp);
        PauseGame();
    }


    public void PauseGame()
    {
        GameManager.Instance.UpdateBottomOverlay(UIactions.HIDE_BOTTOM_OVERLAY);
        //Time.timeScale = 0;
        AudioManager.Instance.PlaySFX("Select");
    }


    public void Resume()
    {
        GameManager.Instance.UpdateBottomOverlay(UIactions.SHOW_DEFAULT_BOTTOM_OVERLAY);
        AudioManager.Instance.PlaySFX("Select");
        Time.timeScale = 1;
        pauseMenuOverlay.SetActive(false);
    }


    public void Home()
    {
        AudioManager.Instance.PlaySFX("Select");
        SaveGame();
        StartCoroutine(DirectingHome());
    }


    public void Restart()
    {
        LevelManager.Instance.ResetAllMissionStats();
        AudioManager.Instance.PlaySFX("Select");
        pauseMenuOverlay.SetActive(false);
        GameDataManager.Instance.PlayerRecords[Player.Instance.PlayerName] = 0;
        GameDataManager.Instance.SavePlayerRecords(Player.Instance.PlayerName, 0);
        GameManager.Instance.CurrentGameStateData = new GameStateData();
        GameDataManager.Instance.AllPlayersGameStateData[Player.Instance.PlayerName] = new GameStateData();
        
        PlayerPrefs.SetInt("FirstLoad", 1);
        PlayerPrefs.SetInt("GameStart", 0);
        PlayerPrefs.SetInt("GameRestart", 0);
        GameManager.Instance.StartGame(GameManager.Instance.CurrentGameStateData);
        GoalSetter.Instance.SetGoal();
    }


    public void SaveGame()
    {
        AudioManager.Instance.PlaySFX("Select");
        StartCoroutine(ProceedGameSave());
    }


    private IEnumerator ProceedGameSave()
    {
        gameSavingOverlay.SetActive(true);
        yield return new WaitForSeconds(2f);

        GameDataManager.Instance.SaveGameData(Player.Instance.PlayerName, GameManager.Instance.CurrentGameStateData);
        
        yield return new WaitForSeconds(2f);
        gameSavedOverlay.SetActive(true);
        gameSavingOverlay.SetActive(false);
        yield return new WaitForSeconds(1f);
        gameSavedOverlay.SetActive(false);
        Debug.Log("saving current place == " + Player.Instance.CurrentPlayerPlace);
        Debug.Log("saving current place == " + GameManager.Instance.CurrentGameStateData.currentPlayerPlace);
        Debug.Log("saving current place == " + GameDataManager.Instance.AllPlayersGameStateData[Player.Instance.PlayerName].currentPlayerPlace);
        yield return null;
    }


    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
        AudioManager.Instance.PlaySFX("Select");

        if (isOnMusic)
        {
            musicBtn.image.sprite = musicOff;
        }
        else
        {
            musicBtn.image.sprite = musicOn;
        }

        isOnMusic = !isOnMusic;
    }


    public void ToggleSFX()
    {
         AudioManager.Instance.ToggleSFX();
         AudioManager.Instance.PlaySFX("Select");

        if (isOnSFX)
        {
            sfxBtn.image.sprite = sfxOff;
        }
        else
        {
            sfxBtn.image.sprite = sfxOn;
        }

        isOnSFX = !isOnSFX;
    }
}
