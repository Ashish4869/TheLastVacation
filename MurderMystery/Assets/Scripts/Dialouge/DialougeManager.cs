using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DialougeManager : MonoBehaviour
{
    [SerializeField]
    SceneDataSO CurrentScene;
    [SerializeField]
    TextMeshProUGUI DialougeText;
    [SerializeField]
    TextMeshProUGUI SpeakerText;

    Continue _continue;
    private Queue<string> _dialouges;
    string _currentDialouge;
    int _currentDialougeCounter = -1;
    Dialouge[] _currentSceneDialouges;
    bool IsDialougeAnimating = false;


    // Start is called before the first frame update
    void Start()
    {
        _dialouges = new Queue<string>();
        _continue = GetComponent<Continue>();
        GetDialouges();
    }

    void GetDialouges()
    {
        _currentSceneDialouges = CurrentScene.GetCurrentSceneDialouges();

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
            _currentDialougeCounter++;
            _currentDialouge = _dialouges.Dequeue();
            StartCoroutine(AnimateText());
        }
        else
        {
            Debug.Log("Exhausted all dialouges");
        }
    }

    private void HandleSpeaker()
    {
        if (_currentDialougeCounter >= 0)
        {
            SpeakerText.text = CurrentScene.GetCurrentSceneDialouges()[_currentDialougeCounter]._speaker;
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
}
