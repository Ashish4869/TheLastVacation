using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the Achievemnets that we obtains after reaching a particular ending
/// </summary>
public class AchivementManager : MonoBehaviour
{
    [SerializeField] GameObject Lock1;
    [SerializeField] GameObject Lock2;
    [SerializeField] GameObject Lock3;
    [SerializeField] GameObject Achive1UI;
    [SerializeField] GameObject Achive2UI;
    [SerializeField] GameObject Achive3UI;




    public void ProcessAchviements(bool[] achivements) //Update the ui as per the data
    {
        if (achivements.Length == 0) return;
        SetAllGameObjectInactive();

       if(achivements[0] == true)
       {
            Achive1UI.SetActive(true);
       }
       else
       {
            Lock1.SetActive(true);
       }

        if (achivements[1] == true)
        {
            Achive2UI.SetActive(true);
        }
        else
        {
            Lock2.SetActive(true);
        }

        if (achivements[2] == true)
        {
            Achive3UI.SetActive(true);
        }
        else
        {
            Lock3.SetActive(true);
        }

    }

    private void SetAllGameObjectInactive()
    {
        Lock1.SetActive(false);
        Lock2.SetActive(false);
        Lock3.SetActive(false);
        Achive1UI.SetActive(false);
        Achive2UI.SetActive(false);
        Achive3UI.SetActive(false);
    }
}
