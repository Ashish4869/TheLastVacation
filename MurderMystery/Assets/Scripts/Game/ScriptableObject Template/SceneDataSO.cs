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
    private List<Dialouge> SceneDialouges;

    [SerializeField]
    bool _hasBranching;

    //Getters
    public List<Dialouge> GetCurrentSceneDialouges() => SceneDialouges;

    public List<CharacterDataSO> GetCurrentSceneCharacters() => characters;

    public int GetDialougeAmount() => SceneDialouges.Count;
   
    public Sprite GetCurrentSceneBG() => SceneBG;

    //Returns a bool for whether the current scene has branching
    public bool HasBranching() => _hasBranching;
}