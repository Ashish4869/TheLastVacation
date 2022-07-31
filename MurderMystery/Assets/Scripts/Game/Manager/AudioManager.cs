using UnityEngine.Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

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

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s._audioClip;
            s.source.volume = s._volume;
            s.source.pitch = s._pitch;
            s.source.loop = s._loop;
        }

        Play("Rain");
    }

    public void Play(string soundName)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);

        if(s == null)
        {
            Debug.LogError("THIS SOUND WITH THIS NAME " + s.name + "IS NOT FOUND");
            return;
        }

        s.source.volume = s._volume;
        s.source.pitch = s._pitch;
        s.source.Play();
        
    }

    public void StopPlaying(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.volume = s._volume;
        s.source.pitch = s._pitch;
        s.source.Stop();
    }

    public void FadeMusicMethod(FadeMusic type , string soundName)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);

        if (s == null)
        {
            Debug.LogError("Sound: " + soundName + " not found!");
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

    IEnumerator FadeOutMusic(Sound s)
    {
        while(s.source.volume > 0)
        {
            yield return new WaitForSeconds(0.1f);
            s.source.volume -= (float)0.02;
        }
    }

    IEnumerator FadeInMusic(Sound s)
    {
        while (s.source.volume < s._volume)
        {
            yield return new WaitForSeconds(0.1f);
            s.source.volume += (float)0.02;
        }
    }

   
}
