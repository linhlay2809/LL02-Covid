using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Tutorial", menuName = "Scriptable Object/Tutorial")]
public class TutorialSO : ScriptableObject
{
    public TutorialName tutorialName;
    public Sprite tutorialImg;
    public string tittle;
    [TextArea()]
    public string content;
    [Tooltip("Da xuat hien")]
    public bool appeared;

    public TutorialSO nextTutorial;
}

