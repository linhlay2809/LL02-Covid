using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetting : MainBehaviour
{
    protected override void Awake()
    {
        base.Awake();
        if(SwitchScreen.Instance != null)
            SwitchScreen.Instance.CloseSwitchScreen();
        MainUISetting.Instance.tutorialUI.ActiveTutorial(!GameManager.Instance.GetTutorial());
    }
    protected void Start()
    {
        MainUISetting.Instance.tutorialUI.FindAndShowTutorial(TutorialName.playerStats);
    }
}
