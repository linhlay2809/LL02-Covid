using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleCtrl : MainBehaviour
{
    public PeopleHealthInfo peopleHealthInfo;
    public PeopleInfection peopleInfection;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPeopleHealthInfo();
        LoadPeopleInfection();
    }

    protected virtual void LoadPeopleHealthInfo()
    {
        if (peopleHealthInfo != null) return;
        this.peopleHealthInfo = GetComponent<PeopleHealthInfo>();
        Debug.Log(transform.name + ": LoadPeopleHealthInfo");
    }
    protected virtual void LoadPeopleInfection()
    {
        if(peopleInfection != null) return;
        this.peopleInfection = GetComponent<PeopleInfection>();
        Debug.Log(transform.name + ": LoadPeopleInfection");
    }
}
