using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleCtrl : MainBehaviour
{
    public PeopleHealthInfo peopleHealthInfo;
    public PeopleInfection peopleInfection;
    public PeopleInfected peopleInfected;
    public PeopleTreated peopleTreated;
    public PeopleInfo peopleInfo;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPeopleHealthInfo();
        LoadPeopleInfection();
        LoadPeopleInfected();
        LoadPeopleTreated();
        LoadPeopleInfo();
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
        if (peopleInfected != null) return;
        this.peopleInfected = GetComponent<PeopleInfected>();
        Debug.Log(transform.name + ": LoadPeopleInfected");
    }
    protected virtual void LoadPeopleTreated()
    {
        if (peopleTreated != null) return;
        this.peopleTreated = GetComponent<PeopleTreated>();
        Debug.Log(transform.name + ": LoadPeopleTreated");
    }
    protected virtual void LoadPeopleInfo()
    {
        if (peopleInfo != null) return;
        this.peopleInfo = GetComponent<PeopleInfo>();
        Debug.Log(transform.name + ": LoadPeopleInfo");
    }
}
