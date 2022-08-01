using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script is responsible for setting the background as the games demands
/// </summary>

public class BackGroundManager : MonoBehaviour
{
    [SerializeField] Image BackGround;
    SceneDataSO _currentScene;

    private void Start()
    {
        EventManager.OnSceneDialougeExhausted += ProcessBackGround; //Function that is run when the event is handled
        ProcessBackGround();
    }

    private void ProcessBackGround() // gets the current scene and sets the BG as per that
    {
        _currentScene = GameManager.Instance.GetCurrentScene();
        BackGround.sprite = _currentScene.GetCurrentSceneBG();

        if(_currentScene._isFlashBack()) //Sets flash back scene dependin on the bool
        {
            FindObjectOfType<PostProcessHandler>().GOBlackAndWhite();
        }
        else
        {
            if(FindObjectOfType<PostProcessHandler>() != null)
            {
                FindObjectOfType<PostProcessHandler>().OutBlackAndWhite();
            }
            
        }
    }

    private void OnDestroy()
    {
        EventManager.OnSceneDialougeExhausted -= ProcessBackGround;
    }

}
