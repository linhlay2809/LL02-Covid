using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreatButton : ButtonDoctorBase
{
    protected PeopleCtrl peopleCtrl;

    protected override void UseFuntion()
    {
        TreatToPeople(this.peopleCtrl);
    }

    // Chua tri cho benh nhan
    protected void TreatToPeople(PeopleCtrl peopleCtrl)
    {
        SoundManager.Instance.Play("Interact");

        if (!peopleCtrl.peopleInfo.IsTested)
        {
            MainUISetting.Instance.notifyUI.FindAndShowNotify(NotifyName.notTestedVirus);
            return;
        }
        peopleCtrl.peopleTreated.BeTreated();

        DoctorCtrl.Instance.doctorInteraction.EnableInteractUI(peopleCtrl);

    }

    public override void SetDataPeoCtrl(PeopleCtrl peopleCtrl)
    {
        this.peopleCtrl = peopleCtrl;
    }
}
