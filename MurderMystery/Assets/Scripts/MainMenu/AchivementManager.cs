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




    public void ProcessAchviements(int achevie1 = 0 , int achieve2 = 0,  int achieve3 = 0) //Update he ui as per the data
    {
        SetAllGameObjectInactive();

       if(achevie1 != 0)
       {
            Achive1UI.SetActive(true);
       }
       else
       {
            Lock1.SetActive(true);
       }

        if (achieve2 != 0)
        {
            Achive2UI.SetActive(true);
        }
        else
        {
            Lock2.SetActive(true);
        }

        if (achieve3 != 0)
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
