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
    public int _achieve1;
    public int _achieve2;
    public int _achieve3;

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
        _achieve1 = saveData.GetAchievements1();
        _achieve2 = saveData.GetAchievements2();
        _achieve3 = saveData.GetAchievements3();
    }
}
