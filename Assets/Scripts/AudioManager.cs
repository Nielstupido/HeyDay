using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private Sound[] musicSounds, sfxSounds, musicEffectSounds;
    [SerializeField] private AudioSource musicSource, sfxSource, soundEffectsSource;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound not Found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }


    public void StopMusic()
    {
        if (musicSource.clip)
        {
            musicSource.Stop();
        }
    }


    public void PlayMusicEffect(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound not Found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }


    public void PlayMusicEffect(AudioClip audioClip)
    {
        if (audioClip == null)
        {
            Debug.Log("Sound not Found");
        }
        else
        {
            soundEffectsSource.clip = audioClip;
            soundEffectsSource.Play();
        }
    }


    public void StopMusicEffect()
    {
        if (soundEffectsSource.clip)
        {
            soundEffectsSource.Stop();
        }
    }


    public void PlaySFX(string name)
    {
       Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null) 
        {
            Debug.Log("Sound not Found");
        }
        else
        {
            sfxSource.clip = s.clip;
            sfxSource.Play();
        } 
    }


    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
        soundEffectsSource.mute = !soundEffectsSource.mute;
    }

    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }


    public void MusicVolume(float volume)
    {
        musicSource.volume = volume; 
        soundEffectsSource.volume = volume;
    }

    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume; 
    }

    public void StopSFX()
    {
        if (sfxSource.clip)
        {
            sfxSource.Stop();
        }
    }
}