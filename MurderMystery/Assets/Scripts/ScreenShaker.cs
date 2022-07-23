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
    bool _isScreenShaking = false;

    private void Start()
    {
        EventManager.OnShakeScreen += ScreenShakeForTime;
    }

    //Shakes The screen untill the shaketime is below 0
    private void Update()
    {
        
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
        _initialPosition = transform.position;
        _shakeTime = time;
        _shakeAmount = Intensity;
        _isScreenShaking = true;
    }

}
