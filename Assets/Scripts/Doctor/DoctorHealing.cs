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

    // Load DoctorCtrl
    protected void LoadDoctorCtrl()
    {
        if (doctorCtrl != null) return;
        this.doctorCtrl = GetComponent<DoctorCtrl>();
        Debug.Log(transform.name + ": LoadDoctorCtrl");
    }

    // Get MedicineInfo
    public MedicineInfo GetMedicineInfo(int medicineIndex)
    {
        return medicineInfos[medicineIndex];
    }

    // Load MedicineInfo trên inspecter
    protected void LoadMedicineInfos()
    {
        if (medicineInfos.Count != 0) return;
        for (int i = 0; i < Enum.GetNames(typeof(MedicineName)).Length; i++)
        {
            MedicineInfo info = new MedicineInfo();
            info.medicineName = (MedicineName)Enum.ToObject(typeof(MedicineName), i);
            medicineInfos.Add(info);
        }
    }
    
    // Thêm số lượng thuốc
    public void AddQuantily(int medicineIndex, int value)
    {
        this.medicineInfos[medicineIndex].quantily += value;
    }
    
}
