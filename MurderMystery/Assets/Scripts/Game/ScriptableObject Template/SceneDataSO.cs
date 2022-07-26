using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
/// <summary>
/// This is the template of the scriptable object which we will use to store revelant data for a particular scene
/// </summary>

[CreateAssetMenu(fileName ="NewScene" , menuName ="Scenes")]
public class SceneDataSO : ScriptableObject
{
    [SerializeField]
    private Sprite SceneBG;

    [SerializeField]
    private List<CharacterDataSO> Characters;

    [SerializeField]
    private List<Dialouge> SceneDialouges;

    [SerializeField]
    bool _hasBranching;

    //Getters
    public List<Dialouge> GetCurrentSceneDialouges()
    {
        return SceneDialouges;
    }

    public List<CharacterDataSO> GetCurrentSceneCharacters()
    {
        List<CharacterDataSO> characters = Characters.ToList();
        return characters;
    }

    public int GetDialougeAmount()
    {
        return SceneDialouges.Count;
    }

    public Sprite GetCurrentSceneBG()
    {
        return SceneBG;
    }

    public bool HasBranching() => _hasBranching;
}
