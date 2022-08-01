using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class holds data of the game configuration
/// </summary>

[System.Serializable]
public class SettingData 
{
    public bool _screenShakes; //Whether to implement screenshakes
    public int _fontSize; // 0 - big  , 1 - meduim , 2 - small
    public float _textSpeed; // 0 - Fast , 1 - slow

    public SettingData(SettingManager settings)
    {
        _screenShakes = settings.GetScreenShake();
        _fontSize = settings.GetSize();
        _textSpeed = settings.GetSpeed();
    }

}
