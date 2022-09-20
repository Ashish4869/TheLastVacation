using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class which holds the data that needs to be saved between sessions
/// </summary>
[System.Serializable]
public class GameData
{
    public string _playerName;
    public bool _IsMalecharacter;
    public int _currentScene;
    public int _branchcounter;
    public int _currentState;
    public bool _IspreviousStatebranch;
    public bool _canLoad;
    public int _divergenceMeter;
    public bool[] _acheivments;
    public GameData(SaveData saveData)
    {
        _playerName = saveData.GetPlayerName();
        _IsMalecharacter = saveData.GetPlayerGender();
        _currentScene = saveData.GetCurrentScene();
        _branchcounter = saveData.GetBranchCounter();
        _currentState = saveData.GetCurrentStateinInt();
        _IspreviousStatebranch = saveData.GetIsPreviousStateBranch();
        _canLoad = saveData.CanLoad();
        _divergenceMeter = saveData.GetDivergence();
        _acheivments = saveData.GetAchievment();
       
    }
}
