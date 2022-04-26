using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.SceneManagement;

public class MenuUI : MainBehaviour
{
    [SerializeField] protected MainMenuUI mainMenuUI;
    [SerializeField] protected float loadingDelayTime = 1f;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMainMenu();
    }

    private void LoadMainMenu()
    {
        if (mainMenuUI != null) return;
        mainMenuUI = transform.parent.GetComponent<MainMenuUI>();
        Debug.Log(transform.name + " Load MainMenu");
    }

    public void PlayGame()
    {
        SwitchScreen.Instance.OpenSwitchScreen();
        GameManager.Instance.SetTutorial(mainMenuUI.optionUI.toggleTutorial.isOn);
        
        StartCoroutine(LoadGame());
    }
    IEnumerator LoadGame()
    {
        yield return new WaitForSeconds(loadingDelayTime);
        SceneManager.LoadScene("MainGame");
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
