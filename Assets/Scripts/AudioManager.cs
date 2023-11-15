using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private GameObject music_controlBtn;
    [SerializeField] private GameObject sfx_controlBtn;
    
    public static AudioManager Instance;

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;


    private void Awake()
    {
        if(Instance==null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void Start()
    {
        PlayMusic("Theme2");
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


    public void PlaySFX(string name)
    {
       Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null) 
        {
            Debug.Log("Sound not Found");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        } 
    }


    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }


    public void MusicVolume(float volume)
    {
        musicSource.volume = volume; 
    }

    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume; 
    }
}