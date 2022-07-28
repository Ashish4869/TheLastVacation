using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject _pauseScreen;

    bool _pauseScreenBool = false;

    private void Start()
    {
      
    }

   public void PauseGame()
   {
        _pauseScreenBool = true;
        Time.timeScale = 0;
        UpdateGameObjects();
   }

    public void ResumeGame()
    {
        Debug.Log("Hello");
        Time.timeScale = 1;
        _pauseScreenBool = false;
        UpdateGameObjects();
    }

    void UpdateGameObjects()
    {
        _pauseScreen.SetActive(_pauseScreenBool);
    }
}
