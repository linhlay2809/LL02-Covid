using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleCtrl : MainBehaviour
{
    public PeopleHealthInfo peopleHealthInfo;
    public PeopleInfection peopleInfection;
    public PeopleInfected peopleInfeted;
    public PeopleTreated peopleTreated;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPeopleHealthInfo();
        LoadPeopleInfection();
        LoadPeopleInfected();
        LoadPeopleTreated();
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
    protected virtual void LoadPeopleInfected()
    {
        if (peopleInfeted != null) return;
        this.peopleInfeted = GetComponent<PeopleInfected>();
        Debug.Log(transform.name + ": LoadPeopleInfected");
    }
    protected virtual void LoadPeopleTreated()
    {
        if (peopleTreated != null) return;
        this.peopleTreated = GetComponent<PeopleTreated>();
        Debug.Log(transform.name + ": LoadPeopleTreated");
    }
}
