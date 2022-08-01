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

    bool _previousStateBranch; //bool is to store whether the prev state was branch state or not
    bool _isCharacterMale;

    bool _isflashBack;

    StateManager _stateManager;
    TransitionManager _transitionManager;

    [SerializeField]
    GameObject _options;

    [SerializeField]
    TextMeshProUGUI _option1;

    [SerializeField]
    TextMeshProUGUI _option2;

    [SerializeField]
    CharacterDataSO Male;

    [SerializeField]
    CharacterDataSO Female;


    //Setting values
    bool _isScreenShakes;
    int _fontSize;
    float _textSpeed;
  
    private void Start() //Cache references to required classes
    {
        _eventManager = FindObjectOfType<EventManager>();
        _stateManager = FindObjectOfType<StateManager>();
        _transitionManager = FindObjectOfType<TransitionManager>();
        FindObjectOfType<AudioManager>().StopPlaying("Rain");
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

        //Getting values saved in the Save File
        if(SaveData.Instance.IsFromLoad())
        {
            GameData data = SaveSystem.LoadGameData();
            
            if(data != null) SetValuesInSaveDataFromGameData(data); //if we are loading for first time , then pull data and store in save data class

            _currentScene = SaveData.Instance.GetCurrentScene();

            if(SaveData.Instance.GetCurrentStateinInt() == 0) //if we in scene state
            {
                _branchCounter =  SaveData.Instance.GetBranchCounter();
                _previousStateBranch = SaveData.Instance.GetIsPreviousStateBranch();
            }
            else
            {
                _branchCounter =  SaveData.Instance.GetBranchCounter() - 1;
                InBranchState();
            }

            _stateManager.SetState(SaveData.Instance.GetCurrentStateinInt());
        }
        else
        {
            //load nothing extra
        }

        //we have to load this anyway
        _yourName = SaveData.Instance.GetPlayerName();

        if (SaveData.Instance.GetPlayerGender())
        {
            _yourCharacter = Male;
            _isCharacterMale = true;
        }
        else
        {
            _yourCharacter = Female;
            _isCharacterMale = false;
        }

        //Configure Settings
        SettingData settingdata = SaveSystem.LoadSettingData();

        if (settingdata != null)
        {
            _isScreenShakes = settingdata._screenShakes;
            _fontSize = settingdata._fontSize;
            _textSpeed = settingdata._textSpeed;
        }
        else //default values
        {
            _isScreenShakes = true;
            _fontSize = 50;
            _textSpeed = 0.03f;
        }

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
            LoadNextSceneWithoutTransition();
            return;
        }
        
        
        if(_mainBranchScenes[_currentScene].HasCharacterSwitch()) //if we have to switch characters then we do this
        {
            LoadNextSceneWithCharacterTransition();
            return;
        }
        

        if (_mainBranchScenes[_currentScene].HasBranching()) //if we have to branch then
        {
            ShowOptions();
        }
        else
        {
            LoadNextSceneWithTransition();
        }
    }

    private void LoadNextSceneWithCharacterTransition()
    {
        StartCoroutine(TransitionCharacters());
    }

    IEnumerator TransitionCharacters()
    {
        _transitionManager.TransitionCharacters();
        yield return new WaitForSeconds(1f);
        _currentScene++;
        _dialougeCounter = -1;
        _eventManager.OnSceneDialougeExhaustedEvent();
       
    }

    private void LoadNextSceneWithoutTransition() //Loads next scene without any transition
    {
        _currentScene++;
        _dialougeCounter = -1;
        _eventManager.OnSceneDialougeExhaustedEvent();
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

    void LoadNextSceneWithTransition()
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


    public void InBranchState() //Storing information that we are in branch state
    {
        _previousStateBranch = true;
    }

    public void InMainState() //storing information that we are in main state
    {
        _previousStateBranch = false;
    }

    public bool WasPreviousStateBranch() //return information as to which state we are in
    {
        return _previousStateBranch;
    }

    public void SetValuesInSaveData() //Saves the values in gamemanager to the Save data class 
    {
        SaveData.Instance.SetBranchCounter(_branchCounter);
        SaveData.Instance.SetCurrentScene(_currentScene);
        SaveData.Instance.SetCurrentState(_stateManager.GetCurrentGameState());
        SaveData.Instance.SetCharacterGender(_isCharacterMale);
        SaveData.Instance.SetIspreviousStateBranch(_previousStateBranch);
        SaveData.Instance.SetCanLoad(true);
    }

    public void SetValuesInSaveDataFromGameData(GameData data) //Does the same as above but from save file
    {
        SaveData.Instance.SetBranchCounter(data._branchcounter);
        SaveData.Instance.SetCurrentScene(data._currentScene);
        GameStates state = GetStateFromInt(data._currentState);
        SaveData.Instance.SetCurrentState(state);
        SaveData.Instance.SetCharacterGender(data._IsMalecharacter);
        SaveData.Instance.SetIspreviousStateBranch(data._IspreviousStatebranch);
        SaveData.Instance.SetCanLoad(true);
    }

    GameStates GetStateFromInt(int value) //Getting a state from the int value
    {
        switch (value)
        {
            case 0: return GameStates.Scene; 
            case 1: return GameStates.SceneA;
            case 2: return GameStates.SceneB; 
            case 3: return GameStates.EndA; 
            case 4: return GameStates.EndASceneA;
            case 5: return GameStates.EndASceneB; 
            case 6: return GameStates.EndB;
            case 7: return  GameStates.EndBSceneA; 
            case 8: return GameStates.EndBSceneB;
            case 9: return GameStates.EndC; 
            case 10: return GameStates.EndCSceneA; 
            case 11: return GameStates.EndCSceneA;

            default: return GameStates.Scene;
        }
    }
     

public void FadeOutMusic()
   {
        FindObjectOfType<AudioManager>().FadeMusicMethod(FadeMusic.FadeOut, FindObjectOfType<AudioManager>().GetCurrentTheme());

        FindObjectOfType<AudioManager>().removeCurrentTheme();
   }
    //Getters
    public int GetCurrentDialougeCounter() => _dialougeCounter;

    public bool GetIfScreenShake() => _isScreenShakes;
    public int GetFontSize() => _fontSize;
    public float GetTextSpeed() => _textSpeed;

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

    public bool IsCurrentSceneFlashback()
    {
        return _mainBranchScenes[_currentScene]._isFlashBack();
    }

}
