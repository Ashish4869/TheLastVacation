using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

/// <summary>
/// This is a class that holds speaker along wiht his/her dialouge
/// </summary>
/// 
[System.Serializable]
public class Dialouge 
{
    [Tooltip("Enter the name of the Speaker speaking the dialouge")]
    public string _speaker; //Person saying the dialouge

    [TextArea(0,5)]
    [Tooltip("Enter the dialouge that the speaker says")]
    public string _dialouge; //Dialouge said by the person

    [Tooltip("The emotion with which the dialouge is conveyed")]
    public Emotion emotion; //Emotion with which the speaker says his/her dialogue   

    [Tooltip("The intensity of screenshake")]
    public ScreenShakes _ShakeScreenType; //Indicates which screen shake u want

    [Tooltip("String of the theme you want to switch to.")]
    public string switchTheme;

    [Tooltip("String of the sound effect you wanna play for this dialogue")]
    public string SoundEffect;
}
