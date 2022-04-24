using System.Collections;
using System.Collections.Generic;
using UnityEngine;
# if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    [SerializeField] protected float loadingDelayTime = 1f;
    public void PlayGame()
    {
        SwitchScreen.Instance.OpenSwitchScreen();
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
