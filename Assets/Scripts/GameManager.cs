using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MainBehaviour
{
     
    private static GameManager instance;

    public static GameManager Instance => instance;
    protected override void Awake()
    {
        base.Awake();
        if (instance != null) return;
        instance = this;
    }

}
