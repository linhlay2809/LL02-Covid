using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
    public bool IsTested()
    {
        return this.isTested;
    }

    // Gan gia tri vao bien isTested
    public void SetIsTested(bool value)
    {
        this.isTested = value;
        this.exclamationIcon.gameObject.SetActive(value); // Hiển thị dấu chấm than
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
