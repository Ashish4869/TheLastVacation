using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchivementManager : MonoBehaviour
{
    [SerializeField] GameObject Lock1;
    [SerializeField] GameObject Lock2;
    [SerializeField] GameObject Lock3;
    [SerializeField] GameObject Achive1UI;
    [SerializeField] GameObject Achive2UI;
    [SerializeField] GameObject Achive3UI;

    //later i will be saving this in a file , have to fetch from there and then check
    public bool _reachedEnding1 = false;
    public bool _reachedEnding2 = false;
    public bool _reachedEnding3 = false;



    public void ProcessAchviements()
    {
        SetAllGameObjectInactive();

       if(_reachedEnding1)
       {
            Achive1UI.SetActive(true);
       }
       else
       {
            Lock1.SetActive(true);
       }

        if (_reachedEnding2)
        {
            Achive2UI.SetActive(true);
        }
        else
        {
            Lock2.SetActive(true);
        }

        if (_reachedEnding3)
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
