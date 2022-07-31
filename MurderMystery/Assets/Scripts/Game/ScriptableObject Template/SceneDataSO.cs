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
    [SerializeField]
    private Sprite SceneBG;

    [SerializeField]
    private List<CharacterDataSO> characters;

    [SerializeField]
    bool _hasBranching;

    [SerializeField]
    bool _characterChange;

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
