using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Pauses the game and manages the resume game also
/// </summary>
/// 
public class Pause : MonoBehaviour
{
    [SerializeField] GameObject _pauseScreen;

    bool _pauseScreenBool = false;

    

   public void PauseGame()
   {
        _pauseScreenBool = true;
        Time.timeScale = 0; //Stops time
        UpdateGameObjects();
   }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        _pauseScreenBool = false;
        UpdateGameObjects();
    }

    void UpdateGameObjects()
    {
        _pauseScreen.SetActive(_pauseScreenBool);
    }
}
