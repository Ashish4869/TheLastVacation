using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The Character Manager Script is used to render character on the screen based on the data obtained from the SceneData Scriptable Object
/// </summary>
public class CharacterManager : MonoBehaviour
{
    SceneDataSO _currentScene;
    [SerializeField] GameObject _charactersInSceneGameObject;
    [SerializeField] GameObject _characterUITemplatePrefab;

    Sprite[] _charactersInScene;
    Dictionary<string , GameObject> _charactersDict;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.OnSceneDialougeExhausted += PrepareNextScene;
        _charactersDict = new Dictionary<string, GameObject>();
        GetCharacters();
        SetCharactersInUI();
    }

    private void SetCharactersInUI() //Creates a game object for each character and makes it a child of Character gameobject
    {
        RectTransform rectTransform;

        for(int i = 0; i < _charactersInScene.Length; i++)
        {
            GameObject child = Instantiate(_characterUITemplatePrefab);
            child.transform.SetParent(_charactersInSceneGameObject.transform);
            child.transform.localScale = new Vector3(1, 1, 1);
            rectTransform = child.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(500, 1000); //later we have to change and make it so that each character gets their own height , width and expression
            child.GetComponent<Image>().sprite = _charactersInScene[i];
            _charactersDict.Add(_charactersInScene[i].name, child); //Place All characters
        }
    }

    private void GetCharacters() //Gets all the characters data from the Game Manager and sets up it in the current scene
    {
        _currentScene = GameManager.Instance.GetCurrentScene();
        _charactersInScene = _currentScene.GetCurrentSceneCharacters();
    }

    public void RenderCharacters()
    {
        
    }

    private void ClearAllCharacters() //Deletes all child elements that were created for this scene
    {
        foreach (string charName in _charactersDict.Keys)
        {
            Destroy(_charactersDict[charName]);
        }

        _charactersDict.Clear();
    }

    private void PrepareNextScene() //Gets all revelant information for the next scene
    {
        ClearAllCharacters();
        GetCharacters();
        SetCharactersInUI();
    }

    private void OnDestroy()
    {
        EventManager.OnSceneDialougeExhausted -= PrepareNextScene;
    }
}
