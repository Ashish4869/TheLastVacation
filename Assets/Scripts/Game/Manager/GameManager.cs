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

    public List<SceneDataSO> _EndA; //Stores all the scene data for the ENDA branch
    public List<SceneDataSO> _EndABranchA; //Stores all the scene data for the ENDA branch A
    public List<SceneDataSO> _EndABranchB; //Stores all the scene data for ENDA branch B

    public List<SceneDataSO> _EndB; //Stores all the scene data for the ENDB branch
    public List<SceneDataSO> _EndBBranchA; //Stores all the scene data for the ENDB branch A
    public List<SceneDataSO> _EndBBranchB; //Stores all the scene data for ENDB branch B

    public List<SceneDataSO> _EndC; //Stores all the scene data for the ENDC branch
    public List<SceneDataSO> _EndCBranchA; //Stores all the scene data for the ENDC branch A
    public List<SceneDataSO> _EndCBranchB; //Stores all the scene data for ENDC branch B

    private int _dialougeCounter = -1; //Counter that keeps track of the dialogue to be displayed
    private int _currentScene = 0; //Counter that keeps track of the scene we are currently in
    private int _branchCounter = 0; //Counter that keeps track of the branch we are in

    private string _yourName; //contains the name that the player has inputed
    private CharacterDataSO _yourCharacter; //contains the character that the player has selected

    bool _previousStateBranch; //bool is to store whether the prev state was branch state or not
    bool _isCharacterMale;

    bool[] _acheivments;

    bool _isflashBack;

    public int _divergenceMeter = 20;

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

    [SerializeField] GameObject _theend;

    [SerializeField] TextMeshProUGUI _text;


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
        if (SaveData.Instance.IsFromLoad())
        {
            GameData data = SaveSystem.LoadGameData();
            
            if(data != null) SetValuesInSaveDataFromGameData(data); //if we are loading for first time , then pull data and store in save data class

            _currentScene = SaveData.Instance.GetCurrentScene();
           

            _divergenceMeter = SaveData.Instance.GetDivergence();

            if (SaveData.Instance.GetCurrentStateinInt() == 0 || SaveData.Instance.GetCurrentStateinInt() == 3 || SaveData.Instance.GetCurrentStateinInt() == 6 || SaveData.Instance.GetCurrentStateinInt() == 9) //if we in scene state
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

        _acheivments = SaveData.Instance.GetAchievment();

        if (_acheivments.Length == 0)
        {
            Debug.Log("Acheviement data not present");
        }
        else
        {
            Debug.Log(_acheivments[0]);
            Debug.Log(_acheivments[1]);
            Debug.Log(_acheivments[2]);
        }

    }

    //Setters / Updaters
    public void UpdateCurrentDialougeCounter() => _dialougeCounter++;

    private List<SceneDataSO> GetMainbranchesArray()
    {
        if (_stateManager.GetCurrentGameState() == GameStates.Scene) return _mainBranchScenes;
        if (_stateManager.GetCurrentGameState() == GameStates.EndA) return _EndA;
        if (_stateManager.GetCurrentGameState() == GameStates.EndB) return _EndB;
        if (_stateManager.GetCurrentGameState() == GameStates.EndC) return _EndC;

        return null;
    }


    private bool IfInBranchState() => _stateManager.GetCurrentGameState() == GameStates.SceneA ||
                                      _stateManager.GetCurrentGameState() == GameStates.SceneB ||
                                      _stateManager.GetCurrentGameState() == GameStates.EndASceneA ||
                                      _stateManager.GetCurrentGameState() == GameStates.EndASceneB ||
                                      _stateManager.GetCurrentGameState() == GameStates.EndBSceneA ||
                                      _stateManager.GetCurrentGameState() == GameStates.EndBSceneB ||
                                      _stateManager.GetCurrentGameState() == GameStates.EndCSceneA ||
                                      _stateManager.GetCurrentGameState() == GameStates.EndCSceneB;

    public bool IfReachedEnd()
    {
        if (_stateManager.GetCurrentGameState() == GameStates.EndA) return (_currentScene == _EndA.Count - 1) ? true : false;
        if (_stateManager.GetCurrentGameState() == GameStates.EndB) return (_currentScene == _EndB.Count - 1) ? true : false;
        if (_stateManager.GetCurrentGameState() == GameStates.EndC) return (_currentScene == _EndC.Count - 1) ? true : false;

        return false;
    }

    //Process the next scene based on which state we are currently in
    //if we are in any branch state then we return to the main branch and process the next scene
    //if the scene as branching , we show the options , else process next scene
    public void ProcessNextScene() 
    {
        if(_currentScene == _mainBranchScenes.Count - 1 && _stateManager.GetCurrentGameState() == GameStates.Scene)
        {
            FindObjectOfType<PostProcessHandler>().OutBlackAndWhite();
            //_theend.SetActive(true);

            _currentScene = -1;
            _branchCounter = 0;
            _stateManager.EvaluateEndingBranch(_divergenceMeter);
            return;
        }

        if(IfReachedEnd())
        {
            _theend.SetActive(true);

            if (_acheivments.Length == 0) _acheivments = new bool[3]; //if null , make a new one

            if (_stateManager.GetCurrentGameState() == GameStates.EndA)
            {
                _acheivments[1] = true;
<<<<<<< HEAD:Assets/Scripts/Game/Manager/GameManager.cs
                Debug.Log("Unlocked Acheivement 2");
=======
                Debug.Log("Unlocked Acheivement 1");
>>>>>>> e346a96cd3767c0d11c366effb082efdb53b809c:MurderMystery/Assets/Scripts/Game/Manager/GameManager.cs
            }

            if (_stateManager.GetCurrentGameState() == GameStates.EndB)
            {
                _acheivments[2] = true;
<<<<<<< HEAD:Assets/Scripts/Game/Manager/GameManager.cs
                Debug.Log("Unlocked Acheivement 3");
=======
                Debug.Log("Unlocked Acheivement 2");
>>>>>>> e346a96cd3767c0d11c366effb082efdb53b809c:MurderMystery/Assets/Scripts/Game/Manager/GameManager.cs
            }

            if (_stateManager.GetCurrentGameState() == GameStates.EndC) 
            {
                _acheivments[0] = true;
<<<<<<< HEAD:Assets/Scripts/Game/Manager/GameManager.cs
                Debug.Log("Unlocked Acheivement 1");
=======
                Debug.Log("Unlocked Acheivement 3");
>>>>>>> e346a96cd3767c0d11c366effb082efdb53b809c:MurderMystery/Assets/Scripts/Game/Manager/GameManager.cs
            }

            SetValuesInSaveData();
            SaveSystem.SaveGameData(SaveData.Instance);
            Debug.Log("Game Saved");
            return;
        }


        if (IfInBranchState()) //If we are in a branch scene
        {
            _stateManager.ReturnToMain();
            LoadNextSceneWithoutTransition();
            return;
        }
        
        
       if(_currentScene != -1)
       {
            if (GetMainbranchesArray()[_currentScene].HasCharacterSwitch()) //if we have to switch characters then we do this
            {
                LoadNextSceneWithCharacterTransition();
                return;
            }


            if (GetMainbranchesArray()[_currentScene].HasBranching()) //if we have to branch then
            {
                ShowOptions();
                return;
            }
       }
           
        
       LoadNextSceneWithTransition();
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

    private void LoadNextSceneWithoutTransition() //Loads next scene without any transition
    {
        _currentScene++;
        _dialougeCounter = -1;
        _eventManager.OnSceneDialougeExhaustedEvent();
    }

    private void ShowOptions() //shows the options 
    {
        _options.SetActive(true);

        if(_stateManager.GetCurrentGameState() == GameStates.Scene)
        {
            _option1.text = _sceneA[_branchCounter].GetCurrentSceneDialouges()[0]._dialouge;
            _option2.text = _sceneB[_branchCounter].GetCurrentSceneDialouges()[0]._dialouge;
        }
        else if(_stateManager.GetCurrentGameState() == GameStates.EndA)
        {
            _option1.text = _EndABranchA[_branchCounter].GetCurrentSceneDialouges()[0]._dialouge;
            _option2.text = _EndABranchB[_branchCounter].GetCurrentSceneDialouges()[0]._dialouge;
        }
        else if(_stateManager.GetCurrentGameState() == GameStates.EndB)
        {
            _option1.text = _EndBBranchA[_branchCounter].GetCurrentSceneDialouges()[0]._dialouge;
            _option2.text = _EndBBranchB[_branchCounter].GetCurrentSceneDialouges()[0]._dialouge;
        }
        else
        {
            _option1.text = _EndCBranchA[_branchCounter].GetCurrentSceneDialouges()[0]._dialouge;
            _option2.text = _EndCBranchB[_branchCounter].GetCurrentSceneDialouges()[0]._dialouge;
        }
        
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
        SaveData.Instance.SetDivergencemeter(_divergenceMeter);
        SaveData.Instance.SetAcheivement(_acheivments);
    }

    public void SetValuesInSaveDataFromGameData(GameData data) //Does the same as above but from save file
    {
        SaveData.Instance.SetName(data._playerName);
        SaveData.Instance.SetBranchCounter(data._branchcounter);
        SaveData.Instance.SetCurrentScene(data._currentScene);
        GameStates state = GetStateFromInt(data._currentState);
        SaveData.Instance.SetCurrentState(state);
        SaveData.Instance.SetCharacterGender(data._IsMalecharacter);
        SaveData.Instance.SetIspreviousStateBranch(data._IspreviousStatebranch);
        SaveData.Instance.SetCanLoad(true);
        SaveData.Instance.SetDivergencemeter(data._divergenceMeter);
        SaveData.Instance.SetAcheivement(data._acheivments);
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
        if (_stateManager.GetCurrentGameState() == GameStates.Scene) return _mainBranchScenes[_currentScene];

        if (_stateManager.GetCurrentGameState() == GameStates.EndA) return _EndA[_currentScene];
        if (_stateManager.GetCurrentGameState() == GameStates.EndB) return _EndB[_currentScene];
        if (_stateManager.GetCurrentGameState() == GameStates.EndC) return _EndC[_currentScene];

       

        if(_stateManager.GetCurrentGameState() == GameStates.SceneA) return _sceneA[_branchCounter];
        if (_stateManager.GetCurrentGameState() == GameStates.SceneB) return _sceneB[_branchCounter];

        if (_stateManager.GetCurrentGameState() == GameStates.EndASceneA) return _EndABranchA[_branchCounter];
        if (_stateManager.GetCurrentGameState() == GameStates.EndASceneB) return _EndABranchB[_branchCounter];

        if (_stateManager.GetCurrentGameState() == GameStates.EndBSceneA) return _EndBBranchA[_branchCounter];
        if (_stateManager.GetCurrentGameState() == GameStates.EndBSceneB) return _EndBBranchB[_branchCounter];

        if (_stateManager.GetCurrentGameState() == GameStates.EndCSceneA) return _EndCBranchA[_branchCounter];
        if (_stateManager.GetCurrentGameState() == GameStates.EndCSceneB) return _EndCBranchB[_branchCounter];



        return null;
    }

    public GameStates GetGameState() => _stateManager.GetCurrentGameState();

    public CharacterDataSO GetCharacter() => _yourCharacter;

    public string GetName() => _yourName;

    public bool IsCurrentSceneFlashback()
    {
        return _mainBranchScenes[_currentScene]._isFlashBack();
    }

    public void UpdateDivergence(int divergence)
    {
        _divergenceMeter += divergence;
    }

}
