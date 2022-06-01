using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctorCtrl : MainBehaviour
{
    public PlayerController controller;
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
        LoadDoctorInteraction();
    }

    // Load LoadController trên inspector
    protected void LoadController()
    {
        if (controller != null) return;
        this.controller = GetComponent<PlayerController>();
        Debug.Log(transform.name + ": LoadPlayerController");
    }

    // LoadDoctorInteract trên inspector
    protected void LoadDoctorInteraction()
    {
        if (doctorInteraction != null) return;
        this.doctorInteraction = GetComponent<DoctorInteraction>();
        Debug.Log(transform.name + ": LoadDoctorInteraction");
    }


}
