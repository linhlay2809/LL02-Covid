using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InfoPeopleUI : MainBehaviour
{
    [SerializeField] protected Image avatar;
    [SerializeField] protected List<Text> textList;

    [SerializeField] protected PeopleCtrl peopleCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadAvatar();
        LoadTextList();
    }

    // Gán peopleCtrl khi interact
    public void SetNewInfoPeople(PeopleCtrl peopleCtrl)
    {
        this.peopleCtrl = peopleCtrl;
    }

    protected override void Update()
    {
        if (this.peopleCtrl == null) return;
        DisplayInformation(peopleCtrl);
    }

    // Load Image avatar
    protected void LoadAvatar()
    {
        if (avatar != null) return;
        avatar = gameObject.transform.GetChild(0).GetComponent<Image>();
    }

    // Load textList trên inspector
    protected void LoadTextList()
    {
        if (textList.Count != 0) return;
        Text[] getChildText = gameObject.transform.GetChild(2).GetComponentsInChildren<Text>();
        foreach (Text text in getChildText)
        {
            textList.Add(text);
        }
        Debug.Log(transform.name + ": LoadTextList");
    }

    // Bật InfoUI
    public void TurnOnDisplayPeople(PeopleCtrl peopleCtrl)
    {
        SetNewInfoPeople(peopleCtrl);

        this.transform.localScale = Vector2.one;
        this.gameObject.SetActive(true);
        DOTween.Kill(this,gameObject);
        this.transform.DOLocalMoveX(727, 0.5f).From(new Vector2(1167, 0));
    }

    // Tắt InfoUI
    public void TurnOffDisplayPeople()
    {
        this.transform.DOLocalMoveX(1167, 0.3f).OnComplete(DisableInfoUI);
    }

    void DisableInfoUI()
    {
        this.gameObject.SetActive(false);
        if (this.peopleCtrl == null) return;
        this.peopleCtrl = null;
    }

    // Load số liệu của people được test
    public void DisplayInformation(PeopleCtrl peopleCtrl)
    {
        avatar.sprite = peopleCtrl.peopleInfo.GetAvatarPeople();
        textList[0].text = GameManager.Instance.strCovidList[(int)peopleCtrl.peopleHealthInfo.VirusName];
        textList[1].text = peopleCtrl.peopleHealthInfo.InfectionRate.ToString();
        textList[2].text = peopleCtrl.peopleHealthInfo.GetDeathRate().ToString();
        textList[3].text = peopleCtrl.peopleHealthInfo.GetBeTreated() ? "Yes" : "No";
        textList[4].text = GameManager.Instance.strVaccineList[(int)peopleCtrl.peopleTreated.Vaccine];
        textList[5].text = GameManager.Instance.strDoseList[(int)peopleCtrl.peopleHealthInfo.NumberOfDoses];
        textList[6].text = peopleCtrl.peopleInfo.GetIDPeople().ToString();
    }
}
