using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingManager : MonoBehaviour
{
    [SerializeField] TMP_Dropdown ChangeTextSize;
    [SerializeField] TMP_Dropdown ChangeTextSpeed;
    [SerializeField] TextMeshProUGUI _sampleText;
    [SerializeField] GameObject _tick;

    int _fontsize;
    int speed;
    bool _isScreenShake;
     
    // Start is called before the first frame update
    void Start()
    {
        ChangeTextSize.onValueChanged.AddListener(delegate 
        {
            OnChangeTextSize(ChangeTextSize);
        });

        ChangeTextSpeed.onValueChanged.AddListener(delegate 
        {
            OnChangeTextSpeed(ChangeTextSpeed);
        });

    }

    public void ToggleScreenShakes()
    {
        _isScreenShake = !_isScreenShake;
        Debug.Log(_isScreenShake);

        _tick.SetActive(_isScreenShake);
    }

    public void OnChangeTextSize(TMP_Dropdown change)
    {
        if(change.value == 0)
        {
            _sampleText.fontSize = 50;
        }

        if(change.value == 1)
        {
            _sampleText.fontSize = 40;
        }

        if(change.value == 2)
        {
            _sampleText.fontSize = 30;
        }
    }

    public void OnChangeTextSpeed(TMP_Dropdown speed)
    {
        if (speed.value == 0)
        {
            //set speed to fast text
        }

        if (speed.value == 1)
        {
           //set speed to slow text
        }
    }


    public void SaveSettings()
    {
        //Save the settings into a file later
    }
}
