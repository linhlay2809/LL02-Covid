using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleInfection : MainBehaviour
{
    public PeopleCtrl peopleCtrl;
    [SerializeField] protected float infectionRadius;
    [SerializeField] protected LayerMask WhatIsInfection;
    [SerializeField] protected float infectionDelayTime;
    protected float currentDelayTime;
    protected override void Update()
    {
        this.Infection();
    }
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

    protected void Infection()
    {
        // return khi this object không bị nhiễm virus
        if (this.peopleCtrl.peopleHealthInfo.VirusName == VirusName.noVirus) return;

        // lấy tất cả các object có layer = WhatIsInfection đang bên trong OverlapSphere
        Collider[] objs = Physics.OverlapSphere(transform.position + new Vector3(0f, 1.5f, 0f), infectionRadius, WhatIsInfection);
        
        if (objs.Length <= 1) return; // trả về nếu không có đối tượng trigger
        
        if (Time.time > currentDelayTime)
        {
            currentDelayTime = Time.time + infectionDelayTime;

            InfectionToPeople(objs);
        }

    }
    // Lây nhiễm cho object khác
    protected void InfectionToPeople(Collider[] objs)
    {
        foreach (Collider col1 in objs)
        {
            PeopleCtrl anotherPeopleCtrl = col1.GetComponent<PeopleCtrl>();
            if (anotherPeopleCtrl == null) return;
            
            if (anotherPeopleCtrl.peopleHealthInfo.VirusName == VirusName.noVirus)
            {
                PeopleHealthInfo healthInfo = this.peopleCtrl.peopleHealthInfo; // Get PeopleHealthInfo của this object

                anotherPeopleCtrl.peopleInfected.Infected(healthInfo.InfectionRate, healthInfo.VirusName);
            }
        }
    }

    // Debug phạm vi lay nhiễm
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position + new Vector3(0f, 1.5f, 0f), infectionRadius);
    }
}
