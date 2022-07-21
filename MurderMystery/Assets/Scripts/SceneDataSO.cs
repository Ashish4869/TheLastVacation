using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This is the templat of the scriptable object which we will use to store revelant data for a particular scene
/// </summary>

[CreateAssetMenu(fileName ="NewScene" , menuName ="Scenes")]
public class SceneDataSO : ScriptableObject
{
    [SerializeField]
    private Sprite SceneBG;

    [SerializeField]
    private Sprite[] Characters;

    [SerializeField]
    private Dialouge[] SceneDialouges;

    public Dialouge[] GetCurrentSceneDialouges()
    {
        return SceneDialouges;
    }

    public Sprite[] GetCurrentSceneCharacters()
    {
        return Characters;
    }
}
