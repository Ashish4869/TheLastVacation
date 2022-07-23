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
    [SerializeField] Image _speakerImage;

    CharacterDataSO[] _charactersInScene;
    Dictionary<string , GameObject> _charactersDict;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.OnSceneDialougeExhausted += PrepareNextScene;
        _charactersDict = new Dictionary<string, GameObject>();
        GetCharacters();
        SetCharactersInUI();
    }

    private void SetCharactersInUI() //Creates a game object for each character and makes it a child of CharacterInTheScene gameobject
    {
        RectTransform rectTransform;

        for(int i = 0; i < _charactersInScene.Length; i++)
        {
            GameObject child = Instantiate(_characterUITemplatePrefab);
            child.transform.SetParent(_charactersInSceneGameObject.transform); //Setting the parent
            child.transform.localScale = new Vector3(1, 1, 1); //reseting the scale to prevent unexpected scaling
            rectTransform = child.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(_charactersInScene[i].GetCharacterWidth(), _charactersInScene[i].GetCharacterHeight()); //Setting the width and height of the character
            child.GetComponent<Image>().sprite = _charactersInScene[i].GetCharacterSpriteAsPerEmotion(Emotion.Normal); //Setting the sprite with normal emotion
            _charactersDict.Add(_charactersInScene[i].name, child); //Place All characters
        }
    }

    private void GetCharacters() //Gets all the characters data from the Game Manager and sets up it in the current scene
    {
        _currentScene = GameManager.Instance.GetCurrentScene();
        _charactersInScene = _currentScene.GetCurrentSceneCharacters();
    }

   public void SetCharacterExpressionAndSpeaker() //Function is called when we click on the screen
   {
        string currentSpeaker = _currentScene.GetCurrentSceneDialouges()[GameManager.Instance.GetCurrentDialougeCounter()]._speaker;
        Emotion currentEmotion = _currentScene.GetCurrentSceneDialouges()[GameManager.Instance.GetCurrentDialougeCounter()].emotion;

        if (IsNotCharacter())
        {
            _speakerImage.color = new Color(1, 1, 1, 0);
            _speakerImage.sprite = null;
            return;
        }

        int i = 0;
        foreach (string charName in _charactersDict.Keys)
        {
            if (charName == _currentScene.GetCurrentSceneDialouges()[GameManager.Instance.GetCurrentDialougeCounter()]._speaker)
            {
                Image charImage = _charactersDict[currentSpeaker].GetComponent<Image>(); //getting image component
                charImage.sprite = _charactersInScene[i].GetCharacterSpriteAsPerEmotion(currentEmotion); //Setting sprite as per emotion
                _speakerImage.color = new Color(1, 1, 1, 1); //Making the sprite visible
                _speakerImage.sprite = _charactersInScene[i].GetCharacterSpriteAsPerEmotion(Emotion.SpeakCrop); //Setting the speaker text
            }
            i++;
        }
    }

    private bool IsNotCharacter() => _currentScene.GetCurrentSceneDialouges()[GameManager.Instance.GetCurrentDialougeCounter()]._speaker == "You" || _currentScene.GetCurrentSceneDialouges()[GameManager.Instance.GetCurrentDialougeCounter()]._speaker == "Narrator" || _currentScene.GetCurrentSceneDialouges()[GameManager.Instance.GetCurrentDialougeCounter()]._speaker == "???";
    

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
