using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        anim = transition.GetComponent<Animator>();
    }

    public void Transition()
    {
        
        TextBoxUI.SetActive(false);
        CharactersParent.SetActive(false);
        DialougeManager.SetActive(false);
        StartCoroutine(ShowElements());
    }

    public void FadeIn()
    {
        ClickScreen.SetActive(false);
        anim.SetTrigger("FadeIn");
    }

    public void FadeOut()
    {   
        anim.SetTrigger("FadeOut");
    }

    IEnumerator ShowElements()
    {
        DialougeManager.SetActive(true);
        yield return new WaitForSeconds(1f);
        CharactersParent.SetActive(true);
        yield return new WaitForSeconds(1f);
        TextBoxUI.SetActive(true);

        
        ClickScreen.SetActive(true);

    }

    private void OnDestroy()
    {
        EventManager.OnSceneDialougeExhausted -= Transition;
    }
}
