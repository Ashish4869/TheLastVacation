using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Shakes the Screen by randomly moving the object  to which this script is attached. The movement is randomized within a Unit Sphere
/// </summary>
public class ScreenShaker : MonoBehaviour
{
    [SerializeField]
    float _shakeAmount = 10f;

    [SerializeField]
    float _shakeTime = 0.0f;

    Vector3 _initialPosition;
    Vector3 _permanentPosition;
    bool _isScreenShaking = false;


    private void Start()
    {
        EventManager.OnShakeScreen += ScreenShakeForTime;
        _permanentPosition = transform.position;
    }

    //Shakes The screen untill the shaketime is below 0
    private void Update()
    {
        if(!GameManager.Instance.GetIfScreenShake()) //If we have set to no screen shake in the settings then we shall not screen shake
        {
            return;
        }
        
        if (_shakeTime > 0)
        {
            transform.position = Random.insideUnitSphere * _shakeAmount + _initialPosition;
            _shakeTime -= Time.deltaTime;
        }
        else if (_isScreenShaking)
        {
            _isScreenShaking = false;
            _shakeTime = 0.0f;
            transform.position = _initialPosition;
        }
    }

    public void ScreenShakeForTime(float time, float Intensity) //Function to call ScreenShake
    {
        if (time == 0 || Intensity == 0) ResetPosition();

        _initialPosition = transform.position;
        _shakeTime = time;
        _shakeAmount = Intensity;
        _isScreenShaking = true;
    }

    public void ResetPosition() //Resets the position when there is no shake
    {
        transform.position = _permanentPosition;
    }

    private void OnDestroy()
    {
        EventManager.OnShakeScreen -= ScreenShakeForTime;
    }

}
