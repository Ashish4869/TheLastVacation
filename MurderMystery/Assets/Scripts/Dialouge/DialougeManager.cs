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

    Continue _continue;
    private Queue<string> _dialouges;
    string _currentDialouge;
    Dialouge[] _currentSceneDialouges;
    bool IsDialougeAnimating = false;


    // Start is called before the first frame update
    void Start()
    {
        EventManager.OnSceneDialougeExhausted += PrepareNextScene;

        _dialouges = new Queue<string>();
        _continue = GetComponent<Continue>();
        GetDialouges();

    }

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

    public void DisplayNextText()
    {
        HandleDialouge();
        HandleSpeaker();
        _continue.HandleContinueButton(IsDialougeAnimating);
    }

    private void HandleDialouge()
    {
        DialougeText.text = "";
        StopAllCoroutines();

        if (IsDialougeAnimating)
        {
            DialougeText.text = _currentDialouge;
            IsDialougeAnimating = false;
            _continue.HandleContinueButton(IsDialougeAnimating);
            return;
        }

        if (_dialouges.Count != 0)
        {
            GameManager.Instance.UpdateCurrentDialougeCounter();
            _currentDialouge = _dialouges.Dequeue();
            StartCoroutine(AnimateText());
        }
        else
        {
            GameManager.Instance.LoadNextScene();
        }
    }

    private void HandleSpeaker()
    {
        if (GameManager.Instance.GetCurrentDialougeCounter() >= 0)
        {
            SpeakerText.text = _currentScene.GetCurrentSceneDialouges()[GameManager.Instance.GetCurrentDialougeCounter()]._speaker;
        }
    }

    IEnumerator AnimateText()
    {
        IsDialougeAnimating = true;

        foreach(char character in _currentDialouge.ToCharArray())
        {
            DialougeText.text += character;
            yield return new WaitForSeconds(0.03f);
        }

        IsDialougeAnimating = false;
        _continue.HandleContinueButton(IsDialougeAnimating);
    }

    private void PrepareNextScene()
    {
        GetDialouges();
    }

    private void OnDestroy()
    {
        EventManager.OnSceneDialougeExhausted -= PrepareNextScene; 
    }
}
