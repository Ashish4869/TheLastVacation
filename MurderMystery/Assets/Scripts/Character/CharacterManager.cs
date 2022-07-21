using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] SceneDataSO _currentScene;
    [SerializeField] GameObject _charactersInSceneGameObject;
    [SerializeField] GameObject _characterUITemplatePrefab;

    Sprite[] _charactersInScene;
    Dictionary<string , GameObject> _charactersDict;

    int i;

    // Start is called before the first frame update
    void Start()
    {
        _charactersDict = new Dictionary<string, GameObject>();
        GetCharacters();
        SetCharactersInUI();
    }

    private void SetCharactersInUI()
    {
        RectTransform rectTransform;

        for(int i = 0; i < _charactersInScene.Length; i++)
        {
            GameObject child = Instantiate(_characterUITemplatePrefab);
            child.transform.SetParent(_charactersInSceneGameObject.transform);
            child.transform.localScale = new Vector3(1, 1, 1);
            rectTransform = child.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(500, 1000); //later we have to change and make it so that each character gets their own height and expression
            child.GetComponent<Image>().sprite = _charactersInScene[i];
            _charactersDict.Add(_charactersInScene[i].name, child); //Center Center or In Place and all
        }
    }

    private void GetCharacters()
    {
        _charactersInScene = _currentScene.GetCurrentSceneCharacters();
    }

    public void RenderCharacters()
    {
        ClearAllCharacters();

        foreach(string charName in _charactersDict.Keys)
        {
            if(charName == _currentScene.GetCurrentSceneDialouges()[i]._speaker)
            {
                _charactersDict[_currentScene.GetCurrentSceneDialouges()[i]._speaker].SetActive(true);
            }
        }

        i++;
    }

    private void ClearAllCharacters()
    {
        foreach (string charName in _charactersDict.Keys)
        {
            _charactersDict[charName].SetActive(false);
        }
    }
}
