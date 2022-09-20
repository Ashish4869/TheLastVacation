using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Deals with changing the settings and applying them
/// </summary>


public class SettingManager : MonoBehaviour
{
    [SerializeField] TMP_Dropdown ChangeTextSize;
    [SerializeField] TMP_Dropdown ChangeTextSpeed;
    [SerializeField] TextMeshProUGUI _sampleText;
    [SerializeField] GameObject _tick;
    [SerializeField] GameObject _settingsOK;

    int _fontsize;
    float _speed;
    bool _isScreenShake = true;
     
    // Start is called before the first frame update
    void Start()
    {
        //These are delegates for the onchange event on the drop down
        ChangeTextSize.onValueChanged.AddListener(delegate 
        {
            OnChangeTextSize(ChangeTextSize);
        });

        ChangeTextSpeed.onValueChanged.AddListener(delegate 
        {
            OnChangeTextSpeed(ChangeTextSpeed);
        });

        //Getting the from the file
        SettingData data = SaveSystem.LoadSettingData();

        if(data != null) //Data from the file
        {
            _isScreenShake = data._screenShakes;
            _fontsize = data._fontSize;
            _speed = data._textSpeed;

            if (_fontsize == 50) ChangeTextSize.value = 0;
            if (_fontsize == 40) ChangeTextSize.value = 1;
            if (_fontsize == 30) ChangeTextSize.value = 2;

            if (_speed == 0.03f) ChangeTextSpeed.value = 0;
            if (_speed == 0.07f) ChangeTextSpeed.value = 1;

            _tick.SetActive(_isScreenShake);
        }
        else //default values
        {
            _isScreenShake = true;
            _fontsize = 50;
            _speed = 0.03f;

            ChangeTextSize.value = 0;
            ChangeTextSpeed.value = 0;

            _tick.SetActive(_isScreenShake);

        }
          

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
            _fontsize = 50;
        }

        if(change.value == 1)
        {
            _sampleText.fontSize = 40;
            _fontsize = 40;
        }

        if(change.value == 2)
        {
            _sampleText.fontSize = 30;
            _fontsize = 30;
        }
    }

    public void OnChangeTextSpeed(TMP_Dropdown speed)
    {
        if (speed.value == 0)
        {
            _speed = 0.03f;
        }

        if (speed.value == 1)
        {
            _speed = 0.07f;
        }
    }


    public void SaveSettings() //Saves the settings in to a file
    {
        Debug.Log("Saved");
        SaveSystem.SaveSettingsData(this);
        SaveData.Instance.SettingsChanged();
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        _settingsOK.SetActive(true);
    }

    public bool GetScreenShake() => _isScreenShake;

    public float GetSpeed() => _speed;

    public int GetSize() => _fontsize;

    public void DisableSetting() //Closes the popup for saving settings
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        _settingsOK.SetActive(false);
    }
    
}
