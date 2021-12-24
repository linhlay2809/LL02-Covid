using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleTreated : MainBehaviour
{
    public PeopleCtrl peopleCtrl;


    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPeopleCtrl();
    }
    // Load PeopleCtrl trên inspector
    protected virtual void LoadPeopleCtrl()
    {
        if (peopleCtrl != null) return;
        peopleCtrl = GetComponent<PeopleCtrl>();
        Debug.Log(transform.name + ": LoadPeopleCtrl");
    }

    public void BeTreated()
    {
        if (peopleCtrl.peopleHealthInfo.VirusName == VirusName.noVirus) return;
        this.peopleCtrl.peopleHealthInfo.SetBeingTreated(true);
    }
}
