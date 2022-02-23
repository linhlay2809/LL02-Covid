using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    private static GameEvents instance;
    public static GameEvents Instance => instance;
    private void Awake()
    {
        if (instance != null) return;
            instance = this;
    }
    public event Action<PeopleCtrl> testCovid;

    public void UseTestCovidEvent(PeopleCtrl peopleCtrl)
    {
        if (testCovid != null)
        {
            testCovid(peopleCtrl);
        }
    }

}
