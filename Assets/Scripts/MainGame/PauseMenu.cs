using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.Search;

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
        SaveGame();
        StartCoroutine(DirectingHome());
    }


    public void Restart()
    {
        pauseMenuOverlay.SetActive(false);
        GameDataManager.Instance.PlayerRecords[Player.Instance.PlayerName] = 0;
        GameDataManager.Instance.SavePlayerRecords(Player.Instance.PlayerName, 0);
        GameDataManager.Instance.NewGameState();
        GameManager.Instance.StartLevel();
    }


    public void SaveGame()
    {
        GameDataManager.Instance.SavePlayerRecords(Player.Instance.PlayerName, 0);
        Debugger.Instance.ShowError(GameDataManager.Instance.SaveGameStateData(GameManager.Instance.CurrentGameStateData).Item2);
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
