using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

/// <summary>
/// Changes the Saturation value of the color grading to give black and white effect
/// </summary>

public class PostProcessHandler : MonoBehaviour
{
    public PostProcessVolume volume;
    ColorGrading _colorGrading;

   //Gets the color grading in the post processing volume
    void Awake()
    {
        volume.profile.TryGetSettings(out _colorGrading);
        _colorGrading.postExposure.value = 0f;
        _colorGrading.saturation.value = 0;
    }

    //Sets the saturation to -100 for black and white effect
    public void GOBlackAndWhite()
    {
        _colorGrading.saturation.value = -100;
    }

    //Sets the saturation to 0 , to remove black and white effect
    public void OutBlackAndWhite()
    {
        _colorGrading.saturation.value = 0;
    }

}

