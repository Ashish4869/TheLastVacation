using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "Characters")]
public class CharacterDataSO : ScriptableObject
{

    int CharacterWidth = 500;
    int CharacterHeight = 1000;

    [SerializeField]
    Sprite _characterNormal;

    [SerializeField]
    Sprite _characterSad;

    [SerializeField]
    Sprite _characterAngry;

    [SerializeField]
    Sprite _characterScared;

    [SerializeField]
    Sprite _characterHappy;


    [SerializeField]
    Sprite _characterShocked;

    [SerializeField]
    Sprite _characterSpeakCrop;


    public Sprite GetCharacterSpriteAsPerEmotion(Emotion emotion)
    {
        switch(emotion)
        {
            case Emotion.Normal:
                return _characterNormal;
                

            case Emotion.Sad:
                return _characterSad;
                

            case Emotion.Angry:
                return _characterAngry;
                

            case Emotion.Scared:
                return _characterScared; ;
                

            case Emotion.Happy:
                return _characterHappy;
               

            case Emotion.Shocked:
                return _characterShocked;
                

            case Emotion.SpeakCrop:
                return _characterSpeakCrop;
               

            default:
                return null;
        }
    }
  

    public int GetCharacterHeight()
    {
        return CharacterHeight;
    }

    public int GetCharacterWidth()
    {
        return CharacterWidth;
    }


}
