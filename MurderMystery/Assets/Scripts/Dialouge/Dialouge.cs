using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a class that holds speaker along wiht his/her dialouge
/// </summary>
/// 
[System.Serializable]
public class Dialouge 
{
    public string _speaker; //Person saying the dialouge

    [TextArea(0,5)]
    public string _dialouge; //Dialouge said by the person
   
}
