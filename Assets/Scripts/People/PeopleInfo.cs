using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class PeopleInfo : MainBehaviour
{
    [SerializeField] protected PeopleCtrl peopleCtrl;
    [SerializeField] protected Transform exclamationIcon;
    [SerializeField] protected Transform cameraMain;

    [SerializeField] protected Sprite avatar;
    [SerializeField] protected int id;
    [SerializeField] protected bool isTested = false;

    protected override void Awake()
    {
        cameraMain = GameObject.FindWithTag("MainCamera").transform;
    }
    protected override void FixedUpdate()
    {

        exclamationIcon.forward = cameraMain.forward; 
    }

    void LoadPeopleCtrl()
    {
        if (peopleCtrl != null) return;
        this.peopleCtrl = GetComponent<PeopleCtrl>();
        Debug.Log(transform.name + ": LoadPeopleCtrl");
    }

    public Sprite GetAvatarPeople()
    {
        return this.avatar;
    }

    public int GetIDPeople()
    {
        return this.id;
    }
    // Lấy và gán giá trị isTested
    public bool IsTested
    {
        get { return this.isTested; }
        set 
        { 
            this.isTested = value;
            ShowExclamationIcon();
        }
    }

    protected void ShowExclamationIcon()
    {
        this.exclamationIcon.gameObject.SetActive(true); // Hiển thị dấu chấm than
        this.exclamationIcon.transform.DOScale(Vector2.one, 0.8f).SetEase(Ease.OutBack).From(Vector2.zero);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPeopleCtrl();
    }
    protected void Start()
    {
        id = Random.Range(0, 1000000);
    }

    void OnMouseDown()
    {
        if (!this.isTested) return;
        MainUISetting.Instance.infoPeopleUI.TurnOnDisplayPeople(this.peopleCtrl);
    }
}
