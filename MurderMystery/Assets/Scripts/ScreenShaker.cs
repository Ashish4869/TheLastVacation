using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void ScreenShakeForTime(float time, float Intensity)
    {
        _initialPosition = transform.position;
        _shakeTime = time;
        _shakeAmount = Intensity;
        _isScreenShaking = true;
    }

}
