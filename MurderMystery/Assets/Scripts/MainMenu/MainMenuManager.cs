using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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
    [SerializeField] Button _load;
    [SerializeField] GameObject _validName;

    bool _IsenterNameActive;
    bool _IsmainMenuActive;
    bool _IschooseCharacterActive;
    bool _isExtrasActive;
    bool _isSettingsActive;


    private void Start()
    {
        AchivementManager achivementManager = FindObjectOfType<AchivementManager>();
        _IsenterNameActive = false;
        _IsmainMenuActive = true;
        _IschooseCharacterActive = false;
        _isExtrasActive = false;
        _isSettingsActive = false;

        Debug.Log(Application.persistentDataPath);

        //Loading the save file to check if we have data to load the game
        GameData data = SaveSystem.LoadGameData();

        if(data != null) //if data present , load game
        {
            _load.interactable = data._canLoad;

            bool[] achivement = data._acheivments;
           
            achivementManager.ProcessAchviements(achivement);
        }
        else //if no data present , no load game
        {
            _load.interactable = false;
        }

        UpdateGameObjects();
    }

    public void EnterName() //opens the ui for player to enter name
    {
        SaveData.Instance.SetIsfromLoad(false);
        _IsenterNameActive = true;
        _IsmainMenuActive = false;
        UpdateGameObjects();
    }

    public void ViewExtras() //opens the ui for Extras page
    {
        _IsmainMenuActive = false;
        _isExtrasActive = true;
        UpdateGameObjects();
    }

    public void ViewSettings()  //opens the ui for Settings page
    {
        _IsmainMenuActive = false;
        _isSettingsActive = true;
        UpdateGameObjects();
    }

    public void Back() //opens Main UI
    {
        
        _isExtrasActive = false;
        _isSettingsActive = false;
        _IsmainMenuActive = true;
        _IsenterNameActive = false;
        _validName.SetActive(false);
        UpdateGameObjects();
    }

    public void ChooseCharacter() //sets up the ui for the player to choose a character
    {
        
        name = _InputField.text;

        if(name == "")
        {
            _validName.SetActive(true);
            _IsenterNameActive = false;
            UpdateGameObjects();
            return;
        }

        _IsenterNameActive = false;
        _IschooseCharacterActive = true;

        Debug.Log(name);
        SaveData.Instance.SetName(name);    
        UpdateGameObjects();
       
    }

    

    public void OkonValidName()
    {
        _validName.SetActive(false);
        _IsenterNameActive = true;
        UpdateGameObjects();
    }

    public void ChooseCharacter(bool isMale) //saves the character chosen in Save data class and loads the next scene
    {
        SaveData.Instance.SetCharacter(isMale);
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

    public void LoadData() //Loads the data from the file and starts the game
    { 
        GameData data = SaveSystem.LoadGameData();
        if (data == null)
        {
            return;
        }
            SaveData.Instance.SetIsfromLoad(true);
            LoadGameScene();
    }

    void LoadGameScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //Loads the next scene
}
