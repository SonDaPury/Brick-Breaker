using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource backgroundMusic;
    public AudioSource[] soundEffects;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayBackgroundMusic()
    {
        if (!backgroundMusic.isPlaying)
        {
            backgroundMusic.Play();
        }
    }

    public void StopBackgroundMusic()
    {
        if (backgroundMusic.isPlaying)
        {
            backgroundMusic.Stop();
        }
    }

    public void PlaySoundEffect(int index)
    {
        if (index >= 0 && index < soundEffects.Length)
        {
            soundEffects[index].Play();
        }
        else
        {
            Debug.LogError("Sound effect index is out of range!");
        }
    }

    public void MuteAudio(bool mute)
    {
        AudioListener.pause = mute;
    }

    public void SetBackgroundMusicVolume(float volume)
    {
        backgroundMusic.volume = volume;
    }

    public void SetSoundEffectVolume(float volume)
    {
        foreach (var sound in soundEffects)
        {
            sound.volume = volume;
        }
    }
}
