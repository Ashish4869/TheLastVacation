using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// The Game Manager keeps tabs of all data on the game and we have made use of the singleton pattern to make this class a global static class accessible by all other classes
/// </summary>
public class GameManager : MonoBehaviour
{
    private EventManager _eventManager;
    public SceneDataSO[] _mainBranchScenes; //Stores all the scene data for the main branch
    public SceneDataSO[] _sceneA; //Stores all the scene data for the A branch
    public SceneDataSO[] _sceneB; //Stores all the scene data for B branch
    private int _dialougeCounter = -1; //Counter that keeps track of the dialogue to be displayed
    private int _currentScene = 0; //Counter that keeps track of the scene we are currently in
    private int _branchCounter = 0; //Counter that keeps track of the branch we are in
    private int _EndbranchCounter = 0;

    StateManager _stateManager;
    TransitionManager _transitionManager;

    [SerializeField]
    GameObject _options;

    [SerializeField]
    TextMeshProUGUI _option1;

    [SerializeField]
    TextMeshProUGUI _option2;


  
    private void Start()
    {
        _eventManager = FindObjectOfType<EventManager>();
        _stateManager = FindObjectOfType<StateManager>();
        _transitionManager = FindObjectOfType<TransitionManager>();
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

        _eventManager = FindObjectOfType<EventManager>();
        _stateManager = FindObjectOfType<StateManager>();
    }

    //Setters / Updaters
    public void UpdateCurrentDialougeCounter()
    {
        _dialougeCounter++;
    }

    public void ProcessNextScene()
    {
        if(_stateManager.GetCurrentGameState() == GameStates.SceneA || _stateManager.GetCurrentGameState() == GameStates.SceneB)
        {
            _stateManager.ReturnToMain();
            LoadNextScene();
            return;
        }

        if(_mainBranchScenes[_currentScene].HasBranching())
        {
            ShowOptions();
        }
        else
        {
            LoadNextScene();
        }
    }

    private void ShowOptions() //shows the options 
    {
        _options.SetActive(true);
        _option1.text = _sceneA[_branchCounter].GetCurrentSceneDialouges()[0]._dialouge;
        _option2.text = _sceneB[_branchCounter].GetCurrentSceneDialouges()[0]._dialouge;
    }

    public void HideOptions()
    {
        _options.SetActive(false);
        _dialougeCounter = -1;
        _eventManager.OnSceneDialougeExhaustedEvent(); // call an event to setup the next scene
        _branchCounter++;
    }

    void LoadNextScene()
    {
        StartCoroutine(Transition());
    }

    IEnumerator Transition()
    {
        _transitionManager.FadeIn();
        yield return new WaitForSeconds(1f);
        // call an event to setup the next scene
        _transitionManager.Transition();
        _transitionManager.FadeOut();
        _currentScene++;
        _dialougeCounter = -1;
        _eventManager.OnSceneDialougeExhaustedEvent();
        
    }

    //Getters
    public int GetCurrentDialougeCounter()
    {
        return _dialougeCounter;
    }

    public SceneDataSO GetCurrentScene() //Returns scene data depending on the state that we are in , defined by the state manager
    {
        
        if(_stateManager.GetCurrentGameState() == GameStates.Scene)
        {
            return _mainBranchScenes[_currentScene];
        }

        if(_stateManager.GetCurrentGameState() == GameStates.SceneA)
        {
            
            return _sceneA[_branchCounter];
        }

        if (_stateManager.GetCurrentGameState() == GameStates.SceneB)
        {
            
            return _sceneB[_branchCounter];
        }

        
        return null;

    }

    public GameStates GetGameState()
    {
        return _stateManager.GetCurrentGameState();
    }

}
