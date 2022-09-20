using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles the transtion between scenes in the game
/// </summary>
public class TransitionManager : MonoBehaviour
{
    [SerializeField] GameObject TextBoxUI;
    [SerializeField] GameObject CharactersParent;
    [SerializeField] GameObject DialougeManager;
    [SerializeField] GameObject transition;
    [SerializeField] GameObject ClickScreen;

    Animator anim;

    private void Start()
    {
        anim = transition.GetComponent<Animator>(); //getting the animator component
    }

    public void Transition() //Sets the objects which are affected transition off and starts the transition animation
    {
        TextBoxUI.SetActive(false);
        CharactersParent.SetActive(false);
        DialougeManager.SetActive(false);
        StartCoroutine(ShowElements());
    }

    public void FadeIn() //Trigger to Fade in
    {
        ClickScreen.SetActive(false); //Turns off the click screen so that the player cannot interact while the animation happens
        anim.SetTrigger("FadeIn");
    }

    public void FadeOut() //Triggers the fade out animation
    {   
        anim.SetTrigger("FadeOut");
    }

    //Shows the elements
    //First the background
    //Then the characters
    //Then the Textbox UI
    //Activates all the gameobjects
    IEnumerator ShowElements()
    {
        DialougeManager.SetActive(true);
        yield return new WaitForSeconds(1f);
        CharactersParent.SetActive(true);
        yield return new WaitForSeconds(1f);
        TextBoxUI.SetActive(true);
        ClickScreen.SetActive(true);

    }

    public void TransitionCharacters()
    {
        StartCoroutine(TransitionChar());
    }

    IEnumerator TransitionChar() //we are getting each child , then getting animator component and then making each of them run FadeOut animation
    {
        ClickScreen.SetActive(false);

        Animator[] anims = CharactersParent.GetComponentsInChildren<Animator>();
        foreach (Animator ani in anims)
        {
            ani.speed = 2;
        }

        foreach (Transform trans in CharactersParent.transform)
        {
            trans.gameObject.GetComponent<Animator>().SetTrigger("fadeout");
        }

        yield return new WaitForSeconds(1f);
        ClickScreen.SetActive(true);
    }

    public void JustFadeIn() //used to move from game to main menu
    {
        anim.SetTrigger("FadeIn");
    }

    private void OnDestroy()
    {
        EventManager.OnSceneDialougeExhausted -= Transition;
    }
}
