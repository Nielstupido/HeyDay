using System.Collections;
using System.Collections.Generic;
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

    private bool isOnMusic = true;
    private bool isOnSFX = true;

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenuOverlay.SetActive(true);
        AudioManager.Instance.PlaySFX("Select");
    }

    public void Resume()
    {
        AudioManager.Instance.PlaySFX("Select");
        Time.timeScale = 1;
        pauseMenuOverlay.SetActive(false);
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
