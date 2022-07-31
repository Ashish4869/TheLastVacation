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
    CharacterDataSO _character;
    int _currentScene;
    GameStates _currentState;
    bool _IspreviousStatebranch;

    public void SetName(string Name) => _playerName = Name;

    public void SetCharacter(CharacterDataSO chara) => _character = chara;

    public CharacterDataSO GetCharacter() => _character;

    public string GetPlayerName() => _playerName;
}
