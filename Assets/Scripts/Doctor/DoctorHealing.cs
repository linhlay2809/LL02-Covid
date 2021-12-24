using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctorHealing : MainBehaviour
{
    public DoctorCtrl doctorCtrl;

    [SerializeField] protected List<MedicineInfo>  medicineInfos;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadMedicineInfos();
        LoadDoctorCtrl();
    }

    protected void LoadDoctorCtrl()
    {
        if (doctorCtrl != null) return;
        this.doctorCtrl = GetComponent<DoctorCtrl>();
        Debug.Log(transform.name + ": LoadDoctorCtrl");
    }

    // Load MedicineInfo trên inspecter
    protected void LoadMedicineInfos()
    {
        for (int i = 0; i < Enum.GetNames(typeof(MedicineName)).Length; i++)
        {
            MedicineInfo info = new MedicineInfo();
            info.medicineName = (MedicineName)Enum.ToObject(typeof(MedicineName), i+1);
            medicineInfos.Add(info);
        }
    }
    
    // Update is called once per frame
    protected override void Update()
    {
        
    }
}
