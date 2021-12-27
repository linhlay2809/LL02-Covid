using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleTreated : MainBehaviour
{
    public PeopleCtrl peopleCtrl;
    [SerializeField] protected VaccineName vaccine;


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

    // Được chưa trị 
    public void BeTreated()
    {
        if (peopleCtrl.peopleHealthInfo.VirusName == VirusName.noVirus) return;
        this.peopleCtrl.peopleHealthInfo.SetBeingTreated(true);
    }

    public void Vaccination(VaccineInfo vaccineInfo)
    {
        if ((int)this.peopleCtrl.peopleHealthInfo.NumberOfDoses == 2) return;
        if (this.peopleCtrl.peopleHealthInfo.VirusName != VirusName.noVirus) return;
        if (this.Vaccine == VaccineName.noVaccine || this.vaccine == vaccineInfo.vaccineName)
        {
            this.Vaccine = vaccineInfo.vaccineName;
            this.peopleCtrl.peopleInfected.SetReduceInfectionRate(vaccineInfo.protectionRate);

            this.peopleCtrl.peopleHealthInfo.NumberOfDoses += 1;
        }

    }

}
