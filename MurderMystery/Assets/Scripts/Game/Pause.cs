using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;


/// <summary>
/// Pauses the game and manages the resume game also
/// </summary>
/// 
public class Pause : MonoBehaviour
{
    [SerializeField] GameObject _pauseScreen;
    [SerializeField] GameObject _mainMenu;
    [SerializeField] GameObject _saving;
    [SerializeField] GameObject _gameSaved;
    [SerializeField] GameObject _ok;
    [SerializeField] GameObject _savingAnim;

    TransitionManager _transition;

    bool _pauseScreenBool = false;
    bool _mainMenuBool = false;
    bool _isSaving = false;
    bool _isGameSaved = false;
    bool _Isok = false;

    private void Start()
    {
        _transition = FindObjectOfType<TransitionManager>();
        _pauseScreenBool = false;
         _mainMenuBool = false;
        _isSaving = false;
        _isGameSaved = false;
        _Isok = false;
        UpdateGameObjects();
    }

    public void PauseGame() //Pause the game and halts all operations
   {
        if(GameManager.Instance.IsCurrentSceneFlashback())
        {
            FindObjectOfType<PostProcessHandler>().OutBlackAndWhite();
        }
        _pauseScreenBool = true;
        Time.timeScale = 0; //Stops time
        UpdateGameObjects();
   }

    public void ResumeGame() //Resumes the game
    {
        if (GameManager.Instance.IsCurrentSceneFlashback())
        {
            FindObjectOfType<PostProcessHandler>().GOBlackAndWhite();
        }

        Time.timeScale = 1;
        _pauseScreenBool = false;
        UpdateGameObjects();
    }

    public void LeaveMainMenu() //Shows the UI for main menu
    {
        _mainMenuBool = true;
        UpdateGameObjects();
    }

    public void LeaveTOMainMenu() //Leaves to the main menu
    {
        Time.timeScale = 1;
        GameManager.Instance.FadeOutMusic();
        _transition.JustFadeIn();
        StartCoroutine(TransitionToMainMenu());
    }

    IEnumerator TransitionToMainMenu() //Transion to Main Menu
    {
        yield return new WaitForSeconds(1f);
        FindObjectOfType<CharacterManager>().ClearAllCharacters();
        FindObjectOfType<AudioManager>().Play("Rain");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1); // go the prev scene  
    }

    public void BacktoPauseMenu() //Closes the Main menu warning
    {
        _mainMenuBool = false;
        UpdateGameObjects();
    }

    public void SaveGameUI() //Shows the UI for saving game
    {
        GameManager.Instance.SetValuesInSaveData();
        SaveSystem.SaveGameData(SaveData.Instance);
        _isSaving = true;
        UpdateGameObjects();
        StartCoroutine(SaveGameAnimation());
    }

    IEnumerator SaveGameAnimation()
    {
        _savingAnim.SetActive(true); //runn the save animation
        Time.timeScale = 1; //set the time to one , so that we can show the animation
        int waitime = Random.Range(2, 3); //wait for 2-3 seconds
        yield return new WaitForSeconds(waitime);
        Time.timeScale = 0; //set the time back to one
        _savingAnim.SetActive(false); //close the animation
        _isGameSaved = true;
        _Isok = true;

        UpdateGameObjects();
    }

    public void Ok()
    {
        _isSaving = false;
        _isGameSaved = false;
        _Isok = false;
        UpdateGameObjects();
    }

    void UpdateGameObjects()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        _pauseScreen.SetActive(_pauseScreenBool);
        _mainMenu.SetActive(_mainMenuBool);
        _saving.SetActive(_isSaving);
        _gameSaved.SetActive(_isGameSaved);
        _ok.SetActive(_Isok);
    }


}
