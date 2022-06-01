using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonDoctorBase : MainBehaviour
{
    [SerializeField] protected Button btn;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        btn = GetComponent<Button>();
    }
    protected override void Awake()
    {
        btn.onClick.AddListener(() => { UseFuntion(); });
    }

    protected virtual void UseFuntion()
    {
        // Override there
    }
    public virtual void SetDataPeoCtrl(PeopleCtrl peopleCtrl)
    {
        // Override there
    }
    public virtual void SetDataVaccAndPeoCtrl(VaccineInfo vaccineInfo, PeopleCtrl peopleCtrl)
    {
        // Override there
    }

}
