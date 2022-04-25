using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MainBehaviour
{
    public Toggle toggleTutorial;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadToggleTu();
    }

    private void LoadToggleTu()
    {
        if (toggleTutorial != null) return;
        this.toggleTutorial = transform.Find("ToggleTutorial").GetComponent<Toggle>();
        Debug.Log(transform.name + " Load Toggle");
    }
}
