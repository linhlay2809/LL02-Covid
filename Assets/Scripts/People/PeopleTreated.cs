﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleTreated : MainBehaviour
{
    [HideInInspector]
    public PeopleCtrl peopleCtrl;
    [SerializeField] protected VaccineName vaccine;
    [Tooltip("Tỷ lệ giảm khi bị nhiễm")]
    [SerializeField] protected float reduceInfectionRate = 0;
    [Header("Time To Death Details")]
    [Tooltip("Thời gian tử vong")]
    [SerializeField] protected float timeToDeath;
    [SerializeField] protected float defaultTTDeath;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPeopleCtrl();
    }

    protected override void Awake()
    {
        ResetTimeToDeath();
    }

    public VaccineName Vaccine
    {
        get { return vaccine; }
        set { vaccine = value; }
    }

    // Load PeopleCtrl trên inspector
    protected virtual void LoadPeopleCtrl()
    {
        if (peopleCtrl != null) return;
        peopleCtrl = GetComponent<PeopleCtrl>();
        Debug.Log(transform.name + ": LoadPeopleCtrl");
    }

    public float GetTimeToDeath()
    {
        return this.timeToDeath;
    }

    // Set thời gian tử vong
    public void ResetTimeToDeath()
    {
        this.timeToDeath = this.defaultTTDeath;
    }

    public void AddTimeToDeath(float value)
    {
        this.timeToDeath += value;
    }

    // Set giảm tỷ lệ lây nhiễm khi tiêm vaccine
    public void SetReduceInfectionRate(float value)
    {
        this.reduceInfectionRate = value;
    }

    // Get tỷ lệ giảm
    public float GetReduceInfectionRate()
    {
        return this.reduceInfectionRate;
    }

    // Được chưa trị 
    public void BeTreated(MainUISetting mainUI)
    {

        if (peopleCtrl.peopleHealthInfo.GetBeTreated()) return;
        if (peopleCtrl.peopleHealthInfo.VirusName == VirusName.noVirus) return;
        int medicineIndex = (int) peopleCtrl.peopleHealthInfo.VirusName - 1; // Lấy index của virusname -1
        // Trả về khi sô lượng thuốc <= 0
        if (GameManager.Instance.GetMedicineInfo((int)peopleCtrl.peopleHealthInfo.VirusName - 1).quantily <= 0) return;

        this.peopleCtrl.peopleHealthInfo.SetBeingTreated(true);

        GameManager.Instance.AddMedicineQuantily(medicineIndex, -1); // Cập nhật số lượng thuốc

        mainUI.inventoryUI.DisplayIventory();
    }

    // Được tiêm vaccine
    public void Vaccination(VaccineInfo vaccineInfo, MainUISetting mainUI)
    {
        if ((int)this.peopleCtrl.peopleHealthInfo.NumberOfDoses == 2) return; // Trả về khi tiêm đủ 2 mũi
        if (this.peopleCtrl.peopleHealthInfo.VirusName != VirusName.noVirus) return; // Trả về khi đã bị nhiễm bệnh
        
        if (this.Vaccine == VaccineName.noVaccine || this.Vaccine == vaccineInfo.vaccineName)
        {
            this.Vaccine = vaccineInfo.vaccineName;
            this.SetReduceInfectionRate(vaccineInfo.protectionRate);

            this.peopleCtrl.peopleHealthInfo.NumberOfDoses += 1; // Thêm số mũi tiêm 
            GameManager.Instance.AddVaccineQuantily((int)vaccineInfo.vaccineName, -1); // Cập nhật số lượng vaccine

            mainUI.inventoryUI.DisplayIventory();
            //vaccineInfo.quantily -= 1;
        }
    }

}
