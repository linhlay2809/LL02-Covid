using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScreen : MainBehaviour
{
    [SerializeField] protected Animator _anim;

    private static SwitchScreen instance;
    public static SwitchScreen Instance => instance;

    protected override void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAnimator();
    }

    protected void LoadAnimator()
    {
        if (_anim != null) return;
        _anim = GetComponent<Animator>();
        Debug.Log(transform.name + " Load Animator");
    }

    public void OpenSwitchScreen()
    {
        _anim.SetBool("IsOpen", true);
    }

    public void CloseSwitchScreen()
    {
        _anim.SetBool("IsOpen", false);
    }
}
