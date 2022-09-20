using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This ScritableObject stores the all the expression that the character can make as well as the width and heigth
/// </summary>

[CreateAssetMenu(fileName = "NewCharacter", menuName = "Characters")]
public class CharacterDataSO : ScriptableObject
{
    [SerializeField] int CharacterWidth = 500;
    [SerializeField] int CharacterHeight = 1000;

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

    //Getters
    public Sprite GetCharacterSpriteAsPerEmotion(Emotion emotion) //Returns a sprite based the emotion passed of type EMOTION (enum)
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
