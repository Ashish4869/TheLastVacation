using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessHandler : MonoBehaviour
{
    public PostProcessVolume volume;
    ColorGrading _colorGrading;

    

    // Start is called before the first frame update
    void Awake()
    {
        volume.profile.TryGetSettings(out _colorGrading);
        _colorGrading.postExposure.value = 0f;
        _colorGrading.saturation.value = 0;
    }


    public void GOBlackAndWhite()
    {
        _colorGrading.saturation.value = -100;
    }

    public void OutBlackAndWhite()
    {
        _colorGrading.saturation.value = 0;
    }

}

