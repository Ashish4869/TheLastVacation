using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void SceneDialougeExhausted();
    public static event SceneDialougeExhausted OnSceneDialougeExhausted;

    public void OnSceneDialougeExhaustedEvent()
    {
        if (OnSceneDialougeExhausted != null)
        {
            OnSceneDialougeExhausted();
        }
    }
}
