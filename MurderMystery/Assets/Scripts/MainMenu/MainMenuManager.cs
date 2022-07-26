using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuManager: MonoBehaviour
{
    [SerializeField] GameObject _enterName;
    [SerializeField] GameObject _mainMenu;
    [SerializeField] GameObject _chooseCharacter;
    [SerializeField] TMP_InputField _InputField;

    bool _IsenterNameActive;
    bool _IsmainMenuActive;
    bool _IschooseCharacterActive;


    private void Start()
    {
        _IsenterNameActive = false;
        _IsmainMenuActive = true;
        _IschooseCharacterActive = false;
        UpdateGameObjects();
    }

    public void EnterName()
    {
        _IsenterNameActive = true;
        _IsmainMenuActive = false;
        UpdateGameObjects();
    }

    public void ChooseCharacter()
    {
        _IsenterNameActive = false;
        _IschooseCharacterActive = true;
        name = _InputField.text;
        Debug.Log(name);
        SaveData.Instance.SetName(name);
        UpdateGameObjects();
    }

    public void ChooseMale(CharacterDataSO maleData)
    {
        SaveData.Instance.SetCharacter(maleData);
        LoadGameScene();
    }


    public void ChooseFemale(CharacterDataSO femaleData)
    {
        SaveData.Instance.SetCharacter(femaleData);
        LoadGameScene();
    }

    void UpdateGameObjects()
    {
        _enterName.SetActive(_IsenterNameActive);
        _mainMenu.SetActive(_IsmainMenuActive);
        _chooseCharacter.SetActive(_IschooseCharacterActive);
    }

   

    void LoadGameScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //Loads the next scene
    }
}
