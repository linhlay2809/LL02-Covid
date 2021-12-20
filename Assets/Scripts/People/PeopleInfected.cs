using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleInfected : MainBehaviour
{
    public PeopleCtrl peopleCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPeopleCtrl();
    }
    // Load PeopleCtrl in inspector
    protected virtual void LoadPeopleCtrl()
    {
        if (peopleCtrl != null) return;
        peopleCtrl = GetComponent<PeopleCtrl>();
        Debug.Log(transform.name + ": LoadPeopleCtrl");
    }
    public void Infected(float infectionRate, VirusName virusName)
    {
        float ratio = Random.Range(0f, 100f);
        if (ratio < infectionRate)
        {
            this.peopleCtrl.peopleHealthInfo.virusInfo.virusName = virusName;
            Debug.Log(transform.name + " is infected virus "+ virusName);
            Debug.LogWarning(ratio);
        }
    }
}
