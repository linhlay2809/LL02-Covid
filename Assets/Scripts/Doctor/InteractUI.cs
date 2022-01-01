using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractUI : MainBehaviour
{
    [SerializeField] protected List<Button> buttons;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadButtons();
    }

    // Load
    protected void LoadButtons()
    {
        Button[] child = transform.GetChild(0).GetComponentsInChildren<Button>();
        foreach (Button button in child)
        {
            buttons.Add(button);
        }
    }
}
