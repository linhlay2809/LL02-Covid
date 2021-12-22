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
    
    // Load PeopleCtrl trên inspector
    protected virtual void LoadPeopleCtrl()
    {
        if (peopleCtrl != null) return;
        peopleCtrl = GetComponent<PeopleCtrl>();
        Debug.Log(transform.name + ": LoadPeopleCtrl");
    }

    // Đang bị object bị nhiễm lây bệnh
    public void Infected(float infectionRate, VirusName virusName)
    {
        float ratio = Random.Range(0f, 100f);
        if (ratio < infectionRate) 
        {
            this.peopleCtrl.peopleHealthInfo.virusInfo.virusName = virusName;
            this.peopleCtrl.peopleHealthInfo.SetMaxInfectionRate((int)virusName);
            this.peopleCtrl.peopleHealthInfo.SetIsInfected(true);
            Debug.Log(transform.name + " is infected virus "+ virusName);
            Debug.LogWarning(ratio);
        }
    }
}
