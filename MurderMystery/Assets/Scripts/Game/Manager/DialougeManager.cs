using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

/// <summary>
/// Gets the dialouge from Game Manager and animates the dialouges. It calls the event to start the next event , once the dialouges have been exhausted
/// </summary>
public class DialougeManager : MonoBehaviour
{
    string _yourName;

    SceneDataSO _currentScene;
    [SerializeField]
    TextMeshProUGUI DialougeText;
    [SerializeField]
    TextMeshProUGUI SpeakerText;


    [SerializeField] float _screenShakeIntensitySmall;
    [SerializeField] float _screenShakeAmountSmall;

    [SerializeField] float _screenShakeIntensityMeduim;
    [SerializeField] float _screenShakeAmountMeduim;

    [SerializeField] float _screenShakeIntensityHeavy;
    [SerializeField] float _screenShakeAmountHeavy;

    Continue _continue;
    DialougeSound _dialougeSound;
    EventManager _eventManager;
    bool _isAnimatingHTMLTag;
    private Queue<string> _dialouges;
    string _currentDialouge;
    List<Dialouge> _currentSceneDialouges;
    bool IsDialougeAnimating = false;

    void Start()
    {
        _eventManager = FindObjectOfType<EventManager>();
        EventManager.OnSceneDialougeExhausted += PrepareNextScene;
        _yourName = GameManager.Instance.GetName();
        _dialouges = new Queue<string>();
        _continue = GetComponent<Continue>();
        _dialougeSound = GetComponent<DialougeSound>();
        GetDialouges();
    }

    //Gets the current scene from Game Manager
    //Fills the dialouges of the current scene into a Queue
    //Displays first Dialouge
    void GetDialouges() 
    {
        _currentScene = GameManager.Instance.GetCurrentScene();
        _currentSceneDialouges = _currentScene.GetCurrentSceneDialouges();

        foreach (Dialouge dialouge in _currentSceneDialouges) //Loads all dialouges of the scene into a queue
        {
            _dialouges.Enqueue(dialouge._dialouge);
        }

        DisplayNextText();
    }


    //Displays the Speaker Text on the UI
    //Displays The Dialouge on the UI
    //Hides the Animation for Continue
    public void DisplayNextText()
    {
        HandleDialouge();
        HandleSpeaker(); 

        _continue.HandleContinueButton(IsDialougeAnimating);
    }

    //First Stop animation
    //Checks if the dialouge is animating
    //  if yes then fills the textbox with current dialouge
    //  else Animates the next dialouge
    //Loads NextScene if all dialouges is exhausted
    private void HandleDialouge()
    {
        DialougeText.text = "";
        StopAllCoroutines();

        if (IsDialougeAnimating)
        {
            DialougeText.text = _currentDialouge;
            IsDialougeAnimating = false;
            _eventManager.OnShakeScreenEvent(0, 0);
            _continue.HandleContinueButton(IsDialougeAnimating);
            return;
        }

        if (_dialouges.Count != 0)
        {
            GameManager.Instance.UpdateCurrentDialougeCounter();
            ProcessScreenShakes(); 
            _currentDialouge = _dialouges.Dequeue();
            _currentDialouge = _currentDialouge.Replace(":", _yourName); //Replacing the : with the player name as entered by the player
            StartCoroutine(AnimateText());
           
        }
        else
        {
            GameManager.Instance.ProcessNextScene();
        }
    }

    //Shakes the screen as the per the value of the enum in the dialouge object , event is called
    private void ProcessScreenShakes()
    {
        switch (_currentScene.GetCurrentSceneDialouges()[GameManager.Instance.GetCurrentDialougeCounter()]._ShakeScreenType)
        {
            case ScreenShakes.Small:
                _dialougeSound.SoundEffects(ScreenShakes.Small);
                _eventManager.OnShakeScreenEvent(_screenShakeAmountSmall, _screenShakeIntensitySmall);
                break;

            case ScreenShakes.Meduim:
                _dialougeSound.SoundEffects(ScreenShakes.Meduim);
                _eventManager.OnShakeScreenEvent(_screenShakeAmountMeduim, _screenShakeIntensityMeduim);
                break;

            case ScreenShakes.Heavy:
                _dialougeSound.SoundEffects(ScreenShakes.Heavy);
                _eventManager.OnShakeScreenEvent(_screenShakeAmountHeavy, _screenShakeIntensityHeavy);
                break;
        }

    }

    private void HandleSpeaker() //Gets the current speaker and displays on the UI
    {
        if (GameManager.Instance.GetCurrentDialougeCounter() >= 0)
        {
            SpeakerText.text = _currentScene.GetCurrentSceneDialouges()[GameManager.Instance.GetCurrentDialougeCounter()]._speaker;
        }
    }

    //Gets each character and displays it on the UI , the process is repeated with a little delay each time
    IEnumerator AnimateText()
    {
        IsDialougeAnimating = true;
        int _isplayed = 0;
        
        foreach(char character in _currentDialouge.ToCharArray())
        { 
            if(_isplayed == 0)
            {
                _dialougeSound.PlayTypingSound();
            }

            _isplayed = (_isplayed+1)%3;

            DialougeText.text += character;

            //Checks if the text encounrtered is a html tag or not
            if (character == '<') _isAnimatingHTMLTag = true;

            if(character == '>') _isAnimatingHTMLTag = false;

            if(_isAnimatingHTMLTag) continue;
            
            if(IsSentenceBreak(character)) //Gives a pause when we meet a sentence break character like . ? !
            {
                yield return new WaitForSeconds(0.5f);
            }
            else
            {
                yield return new WaitForSeconds(0.03f);
            }
            
        }

        IsDialougeAnimating = false;
        _continue.HandleContinueButton(IsDialougeAnimating);
    }

   
    private bool IsSentenceBreak(char character) =>  character == '.' || character == '?' || character == '!';

    private void PrepareNextScene() => GetDialouges(); //Gets the dialouge of the NextScene

    private void OnDestroy()
    {
        EventManager.OnSceneDialougeExhausted -= PrepareNextScene; 
    }
}
