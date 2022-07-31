using UnityEngine.Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Managers the system that plays audio in the scene
/// </summary>

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public string currentTheme; //Stores the name of the current BGM

    //Singleton Pattern
    public static AudioManager Instance;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }


        DontDestroyOnLoad(this);

        foreach(Sound s in sounds) //Instantiate and all attach all the audio in the audio manager so that it is ready to play any kind of sound at any given time
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s._audioClip;
            s.source.volume = s._volume;
            s.source.pitch = s._pitch;
            s.source.loop = s._loop;
        }

        Play("Rain"); //Plays the rain sound
    }

    public void Play(string soundName) //This function plays an audio associated with the string passed as parameter
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);

        if(s == null)
        {
            Debug.LogWarning("THIS SOUND WITH THIS NAME IS NOT FOUND");
            return;
        }

        s.source.volume = s._volume;
        s.source.pitch = s._pitch;
        s.source.Play();
        
    }

    public void StopPlaying(string sound) //This function stops an audio associated with the string passed as parameter
    {
        Sound s = Array.Find(sounds, item => item.name == sound);

        if (s == null)
        {
            Debug.LogWarning("Sound: not found!");
            return;
        }

        s.source.volume = s._volume;
        s.source.pitch = s._pitch;
        s.source.Stop();
    }

    public void FadeMusicMethod(FadeMusic type , string soundName) //This function FadesIn/FadesOut an audio passed as parameter
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);

        if (s == null)
        {
            Debug.LogWarning("Sound:  not found!");
            return;
        }

        if(type == FadeMusic.FadeOut)
        {
            StopAllCoroutines();
            s.source.volume = s._volume;
            StartCoroutine(FadeOutMusic(s));
        }
        else
        {
            StopAllCoroutines();
            s.source.volume = 0;
            StartCoroutine(FadeInMusic(s));
        }
    }

    IEnumerator FadeOutMusic(Sound s) //Fadeout - decrease the music volume till we hit zero
    {
        while(s.source.volume > 0)
        {
            yield return new WaitForSeconds(0.1f);
            s.source.volume -= (float)0.02;
        }
    }

    IEnumerator FadeInMusic(Sound s) //FadeInt - Increase the music volume till we hit volume specified in class
    {
        while (s.source.volume < s._volume)
        {
            yield return new WaitForSeconds(0.1f);
            s.source.volume += (float)0.02;
        }
    }


    public void SwitchTheme(string Theme) //Switches the theme of the music that is currently playing
    {
        if (Theme == "" || Theme == currentTheme) return; //if theme is same the theme that is already being played or null , return
        StopPlaying(currentTheme);
        currentTheme = Theme;
        Play(Theme);
        FadeMusicMethod(FadeMusic.FadeIn, Theme);
    }
}
