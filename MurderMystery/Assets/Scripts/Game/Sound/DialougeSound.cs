using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialougeSound : MonoBehaviour
{
    AudioManager _audioManager;
    private void Start()
    {
       
    }

    public void PlayTypingSound()
    {
        int key = Random.Range(0, 5);

       switch(key)
       {
            case 0: FindObjectOfType<AudioManager>().Play("Key1"); break;
            case 1: FindObjectOfType<AudioManager>().Play("Key2"); break;
            case 2: FindObjectOfType<AudioManager>().Play("Key3"); break;
            case 3: FindObjectOfType<AudioManager>().Play("Key4"); break;
       }
    }

    public void SoundEffects(ScreenShakes shake)
    {
        if(shake == ScreenShakes.Meduim)
        {
            FindObjectOfType<AudioManager>().Play("Hit");
        }
    }
}
