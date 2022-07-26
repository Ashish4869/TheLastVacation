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
    [SerializeField] Sprite _sillhoute;

    [SerializeField]
    List<CharacterDataSO> _charactersInScene;
    CharacterDataSO _yourCharacter;
    Dictionary<string, GameObject> _charactersDict;
    bool _characterInScene;
    int animSpeed = 2;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.OnSceneDialougeExhausted += PrepareNextScene;
        _charactersDict = new Dictionary<string, GameObject>();
        _yourCharacter = GameManager.Instance.GetCharacter();
        GetCharacters();
        SetCharactersInUI();
        //SetCharacterExpressionAndSpeaker();
    }

    bool IsFromMainBranches() => (GameManager.Instance.GetGameState() == GameStates.Scene || 
        GameManager.Instance.GetGameState() ==  GameStates.EndA || 
        GameManager.Instance.GetGameState() == GameStates.EndB || 
        GameManager.Instance.GetGameState() == GameStates.EndC);

    private void SetCharactersInUI() //Creates a game object for each character and makes it a child of CharacterInTheScene gameobject
    {
        if (IsFromMainBranches()) //animate the characters if we are moving between scenes
        {
            animSpeed = 2;
        }
        else //and not between states
        {
            animSpeed = 100;
        }

        if(IsFromMainBranches()) //if we coming from branch state , we dont want the character to animate
        {
            if (GameManager.Instance.WasPreviousStateBranch())
            {
                animSpeed = 100;
                GameManager.Instance.InMainState();
            }
        }
        


        RectTransform rectTransform;

        for (int i = 0; i < _charactersInScene.Count; i++)
        {
            GameObject child = Instantiate(_characterUITemplatePrefab);
            child.transform.SetParent(_charactersInSceneGameObject.transform); //Setting the parent
            child.transform.localScale = new Vector3(1, 1, 1); //reseting the scale to prevent unexpected scaling
            rectTransform = child.GetComponent<RectTransform>();

            Animator anim = child.GetComponent<Animator>();
            anim.speed = animSpeed;

            rectTransform.sizeDelta = new Vector2(_charactersInScene[i].GetCharacterWidth(), _charactersInScene[i].GetCharacterHeight()); //Setting the width and height of the character
            child.GetComponent<Image>().sprite = _charactersInScene[i].GetCharacterSpriteAsPerEmotion(Emotion.Normal); //Setting the sprite with normal emotion

            _charactersDict.Add(_charactersInScene[i].name, child); //Place All characters
        }


    }

    private void GetCharacters() //Gets all the characters data from the Game Manager and sets up it in the current scene
    {
        _currentScene = GameManager.Instance.GetCurrentScene();
       _charactersInScene = _currentScene.GetCurrentSceneCharacters();


        List<Dialouge> dialouges = _currentScene.GetCurrentSceneDialouges();

        foreach (Dialouge dia in dialouges) //Checking if the main character is present in the scene and we add him to the _characterInScene LIST
        {
            if (dia._speaker == "You")
            {
                _charactersInScene.Add(_yourCharacter);
                _characterInScene = true;
                break;
            }
        }
    }

    public void SetCharacterExpressionAndSpeaker() //Function is called when we click on the screen
    {
        string currentSpeaker = _currentScene.GetCurrentSceneDialouges()[GameManager.Instance.GetCurrentDialougeCounter() == -1 ? 0 : GameManager.Instance.GetCurrentDialougeCounter()]._speaker;
        Emotion currentEmotion = _currentScene.GetCurrentSceneDialouges()[GameManager.Instance.GetCurrentDialougeCounter() == -1 ? 0 : GameManager.Instance.GetCurrentDialougeCounter()].emotion;

        if (IsNotCharacter()) //If not character in the scene then add sillhoute
        {
            _speakerImage.sprite = _sillhoute;
            _speakerImage.color = new Color(1, 1, 1, 1);
            return;
        }
        else if(_currentScene.GetCurrentSceneDialouges()[GameManager.Instance.GetCurrentDialougeCounter() == -1 ? 0 : GameManager.Instance.GetCurrentDialougeCounter()]._speaker == "" || _currentScene.GetCurrentSceneDialouges()[GameManager.Instance.GetCurrentDialougeCounter() == -1 ? 0 : GameManager.Instance.GetCurrentDialougeCounter()]._speaker == "You ") //For scene intros
        {
            _speakerImage.sprite = null;
            _speakerImage.color = new Color(0,0, 0, 0);
            return;
        }

        int i = 0;
        foreach (string charName in _charactersDict.Keys)
        {
            if (currentSpeaker == "You") currentSpeaker = _yourCharacter.name; //This is because we have You(male) and You(female)
            if (charName == currentSpeaker)
            {
                Image charImage = _charactersDict[currentSpeaker].GetComponent<Image>(); //getting image component
                charImage.sprite = _charactersInScene[i].GetCharacterSpriteAsPerEmotion(currentEmotion); //Setting sprite as per emotion
                _speakerImage.color = new Color(1, 1, 1, 1); //Making the sprite visible
                _speakerImage.sprite = _charactersInScene[i].GetCharacterSpriteAsPerEmotion(Emotion.SpeakCrop); //Setting the speaker text
            }
           
            i++;
        }
    }

    //Show a Sillehoute when the person speaking is not in the scene , eg : Narrator
    private bool IsNotCharacter() => _currentScene.GetCurrentSceneDialouges()[GameManager.Instance.GetCurrentDialougeCounter() == -1 ? 0 : GameManager.Instance.GetCurrentDialougeCounter()]._speaker == "Narrator" || _currentScene.GetCurrentSceneDialouges()[GameManager.Instance.GetCurrentDialougeCounter() == -1 ? 0 : GameManager.Instance.GetCurrentDialougeCounter()]._speaker == "???" || _currentScene.GetCurrentSceneDialouges()[GameManager.Instance.GetCurrentDialougeCounter() == -1 ? 0 : GameManager.Instance.GetCurrentDialougeCounter()]._speaker == "Manager" || _currentScene.GetCurrentSceneDialouges()[GameManager.Instance.GetCurrentDialougeCounter() == -1 ? 0 : GameManager.Instance.GetCurrentDialougeCounter()]._speaker == "Boss" || _currentScene.GetCurrentSceneDialouges()[GameManager.Instance.GetCurrentDialougeCounter() == -1 ? 0 : GameManager.Instance.GetCurrentDialougeCounter()]._speaker == "Officer";


    public  void ClearAllCharacters() //Deletes all child elements that were created for this scene
    {
        foreach (string charName in _charactersDict.Keys)
        {
            Destroy(_charactersDict[charName]);
        }


        if(_characterInScene) //We are removing the character after using him in the scene , so that when the game is played again , the process can be repeated 
        { 
            _charactersInScene.Remove(_yourCharacter);
            _characterInScene = false;
        }

        _charactersDict.Clear();
    }

    private void PrepareNextScene() //Gets all revelant information for the next scene and sets it up so
    {
        ClearAllCharacters();
        GetCharacters();
        SetCharactersInUI();
        SetCharacterExpressionAndSpeaker();
    }

    private void OnDestroy()
    {
        EventManager.OnSceneDialougeExhausted -= PrepareNextScene;
    }
}
