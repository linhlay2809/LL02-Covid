using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaccineButton : ButtonDoctorBase
{
    protected VaccineInfo vaccineInfo;
    protected PeopleCtrl peopleCtrl;

    protected override void UseFuntion()
    {
        VaccineToPeople(this.vaccineInfo, this.peopleCtrl);
    }

    // Tiêm vaccine cho benh nhan
    protected void VaccineToPeople(VaccineInfo vaccineInfo, PeopleCtrl peopleCtrl)
    {
        SoundManager.Instance.Play("Interact");

        if (!peopleCtrl.peopleInfo.IsTested)
        {
            MainUISetting.Instance.notifyUI.FindAndShowNotify(NotifyName.notTestedVirus);
            return;
        }

        // Tra ve khi so luong vaccine <= 0
        if (vaccineInfo.quantily <= 0) return;

        peopleCtrl.peopleTreated.Vaccination(vaccineInfo);

        DoctorCtrl.Instance.doctorInteraction.EnableInteractUI(peopleCtrl);
    }

    public override void SetDataVaccAndPeoCtrl(VaccineInfo vaccineInfo, PeopleCtrl peopleCtrl)
    {
        this.vaccineInfo = vaccineInfo;
        this.peopleCtrl = peopleCtrl;
    }
}
