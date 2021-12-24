using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctorCtrl : MainBehaviour
{
    public DoctorHealing doctorHealing;
    public DoctorInteraction doctorInteraction;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadDoctorHealing();
        DoctorInteraction();
    }

    protected void LoadDoctorHealing()
    {
        if (doctorHealing != null) return;
        this.doctorHealing = GetComponent<DoctorHealing>();
        Debug.Log(transform.name + ": LoadDoctorHealing");
    }

    protected void DoctorInteraction()
    {
        if (doctorInteraction != null) return;
        this.doctorInteraction = GetComponent<DoctorInteraction>();
        Debug.Log(transform.name + ": LoadDoctorInteraction");
    }

}
