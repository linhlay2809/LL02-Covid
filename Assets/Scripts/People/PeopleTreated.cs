using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleTreated : MainBehaviour
{
    public PeopleCtrl peopleCtrl;
    [SerializeField] protected VaccineName vaccine;
    [Tooltip("Tỷ lệ giảm khi bị nhiễm")]
    [SerializeField] protected float reduceInfectionRate = 0;
    [Tooltip("Thời gian tử vong")]
    [SerializeField] protected float timeToDeath;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPeopleCtrl();
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

    public void SetTimeToDeath(float value)
    {
        this.timeToDeath = value;
    }
    public void SetReduceInfectionRate(float value)
    {
        this.reduceInfectionRate = value;
    }
    public float GetReduceInfectionRate()
    {
        return this.reduceInfectionRate;
    }

    // Được chưa trị 
    public void BeTreated(DoctorCtrl doctorCtrl)
    {
        if (peopleCtrl.peopleHealthInfo.GetBeTreated()) return;
        if (peopleCtrl.peopleHealthInfo.VirusName == VirusName.noVirus) return;
        int medicineIndex = (int)peopleCtrl.peopleHealthInfo.VirusName - 1; // Lấy index của virusname -1
                                                                            // 
        if (doctorCtrl.doctorHealing.GetMedicineInfo(medicineIndex).quantily <= 0) return;

        this.peopleCtrl.peopleHealthInfo.SetBeingTreated(true);

        doctorCtrl.doctorHealing.AddQuantily(medicineIndex, -1); // Cập nhật số lượng thuốc
    }

    // Được tiêm vaccine
    public void Vaccination(VaccineInfo vaccineInfo)
    {
        if ((int)this.peopleCtrl.peopleHealthInfo.NumberOfDoses == 2) return;
        if (this.peopleCtrl.peopleHealthInfo.VirusName != VirusName.noVirus) return;
        if (this.Vaccine == VaccineName.noVaccine || this.Vaccine == vaccineInfo.vaccineName)
        {
            this.Vaccine = vaccineInfo.vaccineName;
            this.SetReduceInfectionRate(vaccineInfo.protectionRate);

            this.peopleCtrl.peopleHealthInfo.NumberOfDoses += 1;
            vaccineInfo.quantily -= 1;
        }
    }

}
