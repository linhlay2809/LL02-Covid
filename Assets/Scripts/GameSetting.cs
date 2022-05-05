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
        SoundManager.Instance.LoadVolume();
        MainUISetting.Instance.tutorialUI.FindAndShowTutorial(TutorialName.mission);
        SoundManager.Instance.Play("ChillMusic");
        Invoke("ShowInteractTutorial", 100f);
        Invoke("ShowPlayerStatsTutorial", 20f);
    }

    protected void ShowInteractTutorial()
    {
        MainUISetting.Instance.tutorialUI.FindAndShowTutorial(TutorialName.interact);
    }
    protected void ShowPlayerStatsTutorial()
    {
        MainUISetting.Instance.tutorialUI.FindAndShowTutorial(TutorialName.playerStats);
    }
}
