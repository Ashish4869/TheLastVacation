using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
/// <summary>
/// Template of the scriptable object which we will use to store revelant data for a particular scene
/// </summary>

[CreateAssetMenu(fileName ="NewScene" , menuName ="Scenes")]
public class SceneDataSO : ScriptableObject
{
    [Tooltip("The Background for the scene")]
    [SerializeField]
    private Sprite SceneBG;

    [Tooltip("The list of characters in the scene")]
    [SerializeField]
    private List<CharacterDataSO> characters;

    [Tooltip("Whether this scene ends to leading a branch?")]
    [SerializeField]
    bool _hasBranching;

    [Tooltip("Whether the next scene is the same as this , but with differetn characters")]
    [SerializeField]
    bool _characterChange;

    [Tooltip("The dialouges in the scene")]
    [SerializeField]
    private List<Dialouge> SceneDialouges;


    //Getters
    public List<Dialouge> GetCurrentSceneDialouges() => SceneDialouges;

    public List<CharacterDataSO> GetCurrentSceneCharacters() => characters;

    public int GetDialougeAmount() => SceneDialouges.Count;
   
    public Sprite GetCurrentSceneBG() => SceneBG;

    //Returns a bool for whether the current scene has branching
    public bool HasBranching() => _hasBranching;
    public bool HasCharacterSwitch() => _characterChange;
}
