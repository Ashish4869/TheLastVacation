using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound 
{
    public string name;

    public AudioClip _audioClip;

    [Range(0f,1f)]
    public float _volume;

    [Range(0.1f,3f)]
    public float _pitch;

    public bool _loop;

    [HideInInspector]
    public AudioSource source;
    
}
