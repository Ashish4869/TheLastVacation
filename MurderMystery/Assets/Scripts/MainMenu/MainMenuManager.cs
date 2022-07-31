using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles all MainMenu UI elements
/// </summary>
public class MainMenuManager: MonoBehaviour
{
    [SerializeField] GameObject _enterName;
    [SerializeField] GameObject _mainMenu;
    [SerializeField] GameObject _chooseCharacter;
    [SerializeField] TMP_InputField _InputField;
    [SerializeField] GameObject _Extras;
    [SerializeField] GameObject _settings;

    bool _IsenterNameActive;
    bool _IsmainMenuActive;
    bool _IschooseCharacterActive;
    bool _isExtrasActive;
    bool _isSettingsActive;


    private void Start()
    {
        _IsenterNameActive = false;
        _IsmainMenuActive = true;
        _IschooseCharacterActive = false;
        _isExtrasActive = false;
        _isSettingsActive = false;
        UpdateGameObjects();
    }

    public void EnterName() //opens the ui for player to enter name
    {
        
        _IsenterNameActive = true;
        _IsmainMenuActive = false;
        UpdateGameObjects();
    }

    public void ViewExtras()
    {
        _IsmainMenuActive = false;
        _isExtrasActive = true;
        UpdateGameObjects();
    }

    public void ViewSettings()
    {
        _IsmainMenuActive = false;
        _isSettingsActive = true;
        UpdateGameObjects();
    }

    public void Back()
    {
        
        _isExtrasActive = false;
        _isSettingsActive = false;
        _IsmainMenuActive = true;
        UpdateGameObjects();
    }

    public void ChooseCharacter() //sets up the ui for the player to choose a character
    {
        _IsenterNameActive = false;
        _IschooseCharacterActive = true;
        name = _InputField.text;
        Debug.Log(name);
        SaveData.Instance.SetName(name);
        UpdateGameObjects();
       
    }

    public void ChooseCharacter(CharacterDataSO charData) //saves the character chosen in Save data class and loads the next scene
    {
        SaveData.Instance.SetCharacter(charData);
        FindObjectOfType<AudioManager>().FadeMusicMethod(FadeMusic.FadeOut, "Rain");
        LoadGameScene();
    }

    void UpdateGameObjects() //updates the game objects based on the bool
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        _enterName.SetActive(_IsenterNameActive);
        _mainMenu.SetActive(_IsmainMenuActive);
        _chooseCharacter.SetActive(_IschooseCharacterActive);
        _Extras.SetActive(_isExtrasActive);
        _settings.SetActive(_isSettingsActive);
    }

    void LoadGameScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //Loads the next scene
}
