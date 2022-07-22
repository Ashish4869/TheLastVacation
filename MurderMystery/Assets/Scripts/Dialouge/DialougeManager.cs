using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DialougeManager : MonoBehaviour
{

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
    [SerializeField] float _screenShakeAmountMeduim;

    Continue _continue;
    EventManager _eventManager;
    bool _isAnimatingHTMLTag;
    private Queue<string> _dialouges;
    string _currentDialouge;
    Dialouge[] _currentSceneDialouges;
    bool IsDialougeAnimating = false;

    void Start()
    {
        _eventManager = FindObjectOfType<EventManager>();
        EventManager.OnSceneDialougeExhausted += PrepareNextScene;

        _dialouges = new Queue<string>();
        _continue = GetComponent<Continue>();
        GetDialouges();

    }

    //Gets the current scene from Game Manager
    //Fills the dialouges of the current scene into a Queue
    //Displays first Dialouge
    void GetDialouges() 
    {
        _currentScene = GameManager.Instance.GetCurrentScene();

        _currentSceneDialouges = _currentScene.GetCurrentSceneDialouges();

        foreach (Dialouge dialouge in _currentSceneDialouges)
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

            //if(_currentScene.GetCurrentSceneDialouges()[GameManager.Instance.GetCurrentDialougeCounter()]._ShouldShakeScreen)
            {
                
            }

            _currentDialouge = _dialouges.Dequeue();
            StartCoroutine(AnimateText());
        }
        else
        {
            GameManager.Instance.LoadNextScene();
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

        foreach(char character in _currentDialouge.ToCharArray())
        {
            DialougeText.text += character;

            if (character == '<')
            {
                _isAnimatingHTMLTag = true;
            }

            if(character == '>')
            {
                _isAnimatingHTMLTag = false;
            }

            if(_isAnimatingHTMLTag)
            {
                continue;
            }

                

            if(character == '.' || character == '?' || character == '!')
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

    private void PrepareNextScene() //Gets the dialouge of the NextScene
    {
        GetDialouges();
    }

    private void OnDestroy()
    {
        EventManager.OnSceneDialougeExhausted -= PrepareNextScene; 
    }
}
