using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGroundManager : MonoBehaviour
{
    [SerializeField] Image BackGround;
    SceneDataSO _currentScene;


    private void Start()
    {
        EventManager.OnSceneDialougeExhausted += ProcessBackGround;
    }

    private void ProcessBackGround()
    {
        _currentScene = GameManager.Instance.GetCurrentScene();
        BackGround.sprite = _currentScene.GetCurrentSceneBG();
    }

}
