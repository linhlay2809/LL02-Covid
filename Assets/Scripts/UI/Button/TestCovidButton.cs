using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCovidButton : ButtonDoctorBase
{
    protected PeopleCtrl peopleCtrl;

    protected override void UseFuntion()
    {
        TestCovid(this.peopleCtrl);
    }

    // Test Covid cho benh nhan
    protected void TestCovid(PeopleCtrl peopleCtrl)
    {
        SoundManager.Instance.Play("Interact");

        MainUISetting.Instance.infoPeopleUI.TurnOnDisplayPeople(peopleCtrl);

        DoctorCtrl.Instance.doctorInteraction.EnableInteractUI(peopleCtrl);

        if (peopleCtrl.peopleInfo.IsTested) return;
        peopleCtrl.peopleInfo.IsTested = true;
        MainUISetting.Instance.playerStatsUI.ReduceEnergyStat(10);
    }
    public override void SetDataPeoCtrl(PeopleCtrl peopleCtrl)
    {
        this.peopleCtrl = peopleCtrl;
    }
}
