using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Event Manager is used to delegate functions requried to run when a particular event is fired
/// </summary>
public class EventManager : MonoBehaviour
{
    //This event is called when we are done with a Scene and want to go the next scene
    //Elements Effected
    public delegate void SceneDialougeExhausted();
    public static event SceneDialougeExhausted OnSceneDialougeExhausted;

    public void OnSceneDialougeExhaustedEvent()
    {
        if (OnSceneDialougeExhausted != null)
        {
            OnSceneDialougeExhausted();
        }
    }


    //This event is called when we want to shake the screen
    //Elements effected
    public delegate void ShakeScreen(float time , float intensity);
    public static event ShakeScreen OnShakeScreen;

    public void OnShakeScreenEvent(float time , float intensity)
    {
        if (OnShakeScreen != null)
        {
            OnShakeScreen(time , intensity);
        }
    }
}
