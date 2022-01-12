using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUISetting : MainBehaviour
{
    [SerializeField] protected GameObject infoUI;
    [SerializeField] protected List<Text> textList;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadTextList();
        LoadUI();
    }

    private void LoadUI()
    {
        if (infoUI != null) return;
        infoUI = transform.GetChild(0).gameObject;
        Debug.Log(transform.name + ": LoadUI");
    }

    protected void LoadTextList()
    {
        if (textList.Count != 0) return;
        Text[] getChildText = gameObject.transform.GetChild(0).GetChild(2).GetComponentsInChildren<Text>();
        foreach (Text text in getChildText)
        {
            textList.Add(text);
        }
        Debug.Log(transform.name + ": LoadTextList");
    }

    public void ShowInfoDisplay(PeopleCtrl peopleCtrl)
    {
        infoUI.SetActive(true);
        textList[0].text = peopleCtrl.peopleHealthInfo.VirusName.ToString();
        textList[1].text = peopleCtrl.peopleHealthInfo.InfectionRate.ToString();
        textList[2].text = peopleCtrl.peopleHealthInfo.GetDeathRate().ToString();
        textList[3].text = peopleCtrl.peopleHealthInfo.GetBeTreated().ToString();
        textList[4].text = peopleCtrl.peopleTreated.Vaccine.ToString();
        textList[5].text = peopleCtrl.peopleHealthInfo.NumberOfDoses.ToString();
    }
}
