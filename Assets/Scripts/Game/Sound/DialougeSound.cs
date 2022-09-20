using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Plays all sound originating from the dialouges
/// </summary>
public class DialougeSound : MonoBehaviour
{
    AudioManager _audioManager;
    
    public void PlayTypingSound() //Picks a random audio and plays it
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

    

    public void SwitchTheme(string theme) //Switches the theme of music
    {
        FindObjectOfType<AudioManager>().SwitchTheme(theme);
    }

    public void PlaySFX(string sfx)
    {
        FindObjectOfType<AudioManager>().Play(sfx);
    }
}
