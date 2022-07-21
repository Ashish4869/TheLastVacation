using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is responsible for showing the animation that indicates that the player can they can go to the next dialouge when they are done
/// </summary>
public class Continue : MonoBehaviour
{
    [SerializeField]
    GameObject _continueImage;
    public void HandleContinueButton(bool condition) //function that handles the animation based on parameter passsed
    {
        if(condition)
        {
            _continueImage.SetActive(false); //Setting gameobeject inactive
        }
        else
        {
            _continueImage.SetActive(true); //Setting gameobecjt active
        }
    }
}
