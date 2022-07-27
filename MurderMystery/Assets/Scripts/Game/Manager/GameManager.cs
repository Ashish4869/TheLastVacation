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
    public List<SceneDataSO> _mainBranchScenes; //Stores all the scene data for the main branch
    public List<SceneDataSO> _sceneA; //Stores all the scene data for the A branch
    public List<SceneDataSO> _sceneB; //Stores all the scene data for B branch
    private int _dialougeCounter = -1; //Counter that keeps track of the dialogue to be displayed
    private int _currentScene = 0; //Counter that keeps track of the scene we are currently in
    private int _branchCounter = 0; //Counter that keeps track of the branch we are in
    private int _EndbranchCounter = 0;

    private string _yourName; //contains the name that the player has inputed
    private CharacterDataSO _yourCharacter; //contains the character that the player has selected

    StateManager _stateManager;
    TransitionManager _transitionManager;

    [SerializeField]
    GameObject _options;

    [SerializeField]
    TextMeshProUGUI _option1;

    [SerializeField]
    TextMeshProUGUI _option2;


  
    private void Start() //Cache references to required classes
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

        //Getting values saved in the SaveData Class
        _yourName = SaveData.Instance.GetPlayerName();
        _yourCharacter = SaveData.Instance.GetCharacter();
    }

    //Setters / Updaters
    public void UpdateCurrentDialougeCounter() => _dialougeCounter++;
    
    //Process the next scene based on which state we are currently in
    //if we are in any branch state then we return to the main branch and process the next scene
    //if the scene as branching , we show the options , else process next scene
    public void ProcessNextScene() 
    {
        if(_stateManager.GetCurrentGameState() == GameStates.SceneA || _stateManager.GetCurrentGameState() == GameStates.SceneB) //If we are in a branch scene
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

    public void HideOptions() //Hides the options
    {
        _options.SetActive(false);
        SetUpNextScene();
    }

    void SetUpNextScene() //Setsup the next scene
    {
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
    public int GetCurrentDialougeCounter() => _dialougeCounter;
    

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

    public GameStates GetGameState() => _stateManager.GetCurrentGameState();

    public CharacterDataSO GetCharacter() => _yourCharacter;

    public string GetName() => _yourName;

}
