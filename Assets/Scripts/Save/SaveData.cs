using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to cache data that will stored in file when the player saves the game
/// </summary>
public class SaveData : MonoBehaviour
{
    //Implementation of Singleton Pattern
    private static SaveData _instance;
    public static SaveData Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SaveData>();

                if (_instance == null)
                {
                    _instance = new GameObject().AddComponent<SaveData>();
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
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
    }


    //Data to save when the player saves the game
    string _playerName;
    bool __isMalecharacter; 
    CharacterDataSO _character;
    int _currentScene;
    int _branchcounter;
    GameStates _currentState = GameStates.Scene;
    bool _IspreviousStatebranch;
    bool _canLoad = false;
    int _divergenceMeter;
    public bool[] _acheivements = new bool[3];

    bool _isFromLoad;

    bool _isSettingsChanged;

    public void SettingsChanged() => _isSettingsChanged = true;
    public bool GetSettingsStatus() => _isSettingsChanged;

    //Setters
    public void SetName(string Name) => _playerName = Name;

    public void SetCharacter(bool chara) => __isMalecharacter = chara;

    public void SetCurrentState(GameStates currentState) => _currentState = currentState;

    public void SetBranchCounter(int branchCounter) => _branchcounter = branchCounter;

    public void SetCharacterGender(bool chara) => __isMalecharacter = chara;

    public void SetIspreviousStateBranch(bool state) => _IspreviousStatebranch = state;

    public void SetCurrentScene(int scenecoutner) => _currentScene = scenecoutner;

    public void SetCanLoad(bool canload) => _canLoad = canload;

    public void SetIsfromLoad(bool load) => _isFromLoad = load;

    public void SetDivergencemeter(int divergence) => _divergenceMeter = divergence;

    public void SetAcheivement(bool[] achivement) => _acheivements = achivement;


    //Getters
    public CharacterDataSO GetCharacter() => _character;

    public string GetPlayerName() => _playerName;

    public int GetCurrentStateinInt()
    {
        switch(_currentState)
        {
            case GameStates.Scene: return 0; 
            case GameStates.SceneA: return 1;
            case GameStates.SceneB: return 2;
            case GameStates.EndA: return 3;
            case GameStates.EndASceneA: return 4;
            case GameStates.EndASceneB: return 5;
            case GameStates.EndB: return 6;
            case GameStates.EndBSceneA: return 7;
            case GameStates.EndBSceneB: return 8;
            case GameStates.EndC: return 9;
            case GameStates.EndCSceneA: return 10;
            case GameStates.EndCSceneB: return 11;

            default: return -1;
        }
    }


    public bool GetPlayerGender() => __isMalecharacter;

    public int GetBranchCounter() => _branchcounter;

    public int GetCurrentScene() => _currentScene;

    public bool GetIsPreviousStateBranch() => _IspreviousStatebranch;

    public bool CanLoad() => _canLoad;

    public bool IsFromLoad() => _isFromLoad;

    public int GetDivergence() => _divergenceMeter;

    public bool[] GetAchievment() => _acheivements;

}
