using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctorHealing : MainBehaviour
{
    [HideInInspector]
    public DoctorCtrl doctorCtrl;

    [SerializeField] protected List<MedicineInfo>  medicineInfos;
    [SerializeField] protected List<VaccineInfo> vaccineInfos;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadMedicineInfos();
        LoadDoctorCtrl();
        LoadVaccineInfos();
    }

    // Load DoctorCtrl
    protected void LoadDoctorCtrl()
    {
        if (doctorCtrl != null) return;
        this.doctorCtrl = GetComponent<DoctorCtrl>();
        Debug.Log(transform.name + ": LoadDoctorCtrl");
    }

    // Load VaccineInfos trên inspecter
    protected void LoadVaccineInfos()
    {
        if (vaccineInfos.Count != 0) return;
        for (int i = 1; i < Enum.GetNames(typeof(VaccineName)).Length; i++)
        {
            VaccineInfo info = new VaccineInfo();
            info.vaccineName = (VaccineName)Enum.ToObject(typeof(VaccineName), i);
            vaccineInfos.Add(info);
        }
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

    // Get MedicineInfo
    public MedicineInfo GetMedicineInfo(int medicineIndex)
    {
        return medicineInfos[medicineIndex];
    }

    // Get VaccineInfo
    public VaccineInfo GetVaccineInfo(int vaccineIndex)
    {
        return vaccineInfos[vaccineIndex];
    }

    // Thêm số lượng thuốc
    public void AddQuantily(int medicineIndex, int value)
    {
        this.medicineInfos[medicineIndex].quantily += value;
    }
    
}
