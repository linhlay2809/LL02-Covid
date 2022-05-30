using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctorCtrl : MainBehaviour
{
    public PlayerController controller;
    public DoctorHealing doctorHealing;
    public DoctorInteraction doctorInteraction;

    private static DoctorCtrl instance;
    public static DoctorCtrl Instance => instance;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null) return;
        instance = this;
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadController();
        LoadDoctorHealing();
        LoadDoctorInteraction();
    }

    // Load LoadController tr�n inspector
    protected void LoadController()
    {
        if (controller != null) return;
        this.controller = GetComponent<PlayerController>();
        Debug.Log(transform.name + ": LoadPlayerController");
    }

    // Load LoadDoctorHealing tr�n inspector
    protected void LoadDoctorHealing()
    {
        if (doctorHealing != null) return;
        this.doctorHealing = GetComponent<DoctorHealing>();
        Debug.Log(transform.name + ": LoadDoctorHealing");
    }

    // LoadDoctorInteract tr�n inspector
    protected void LoadDoctorInteraction()
    {
        if (doctorInteraction != null) return;
        this.doctorInteraction = GetComponent<DoctorInteraction>();
        Debug.Log(transform.name + ": LoadDoctorInteraction");
    }


}
