using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Game Manager keeps tabs of all data on the game and we have made use of the singleton pattern to make this class a, global static class
/// </summary>
public class GameManager : MonoBehaviour
{
    private EventManager _eventManager;
    public SceneDataSO[] GameScenes;
    private int _dialougeCounter = -1;
    private int _currentScene = 0;


    private void Start()
    {
        _eventManager = FindObjectOfType<EventManager>();
    }

    //Implementation of Singleton Pattern
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();

                if (_instance == null)
                {
                    _instance = new GameObject().AddComponent<GameManager>();
                }
            }

            return _instance;
        }

    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this);
            DontDestroyOnLoad(this);
        }
    }

    //Setters / Updaters
    public void UpdateCurrentDialougeCounter()
    {
        _dialougeCounter++;
    }

    public void LoadNextScene()
    {
        _currentScene++;
        _dialougeCounter = -1;
        _eventManager.OnSceneDialougeExhaustedEvent(); // call an event to setup the next scene
    }

    //Getters
    public int GetCurrentDialougeCounter()
    {
        return _dialougeCounter;
    }

    public SceneDataSO GetCurrentScene()
    {
        return GameScenes[_currentScene];
    }

}
