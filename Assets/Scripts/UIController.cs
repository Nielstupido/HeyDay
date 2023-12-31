using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Button musicBtn;
    [SerializeField] private Button sfxBtn;

    public Slider _musicSlider, _sfxSlider;


    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
        AudioManager.Instance.PlaySFX("Select");

        if (musicBtn.image.color  == musicBtn.colors.normalColor)
        {
            musicBtn.image.color = musicBtn.colors.disabledColor;
        }
        else
        {
            musicBtn.image.color = musicBtn.colors.normalColor;
        }
    }

    public void ToggleSFX()
    {
        AudioManager.Instance.ToggleSFX();
        AudioManager.Instance.PlaySFX("Select");

        if (sfxBtn.image.color  == sfxBtn.colors.normalColor)
        {
            sfxBtn.image.color = sfxBtn.colors.disabledColor;
        }
        else
        {
            sfxBtn.image.color = sfxBtn.colors.normalColor;
        }
    }

    public void MusicVolume()
    {
        AudioManager.Instance.MusicVolume(_musicSlider.value);
    }

    public void SFXVolume()
    {
        AudioManager.Instance.SFXVolume(_sfxSlider.value);
    }
}

