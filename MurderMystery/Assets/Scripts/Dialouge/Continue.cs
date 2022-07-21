using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Continue : MonoBehaviour
{
    [SerializeField]
    GameObject _continueImage;
    public void HandleContinueButton(bool condition)
    {
        if(condition)
        {
            _continueImage.SetActive(false);
        }
        else
        {
            _continueImage.SetActive(true);
        }
    }
}
