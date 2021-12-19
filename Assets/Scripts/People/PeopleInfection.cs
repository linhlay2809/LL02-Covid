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
        base.Update();
        //this.Infection();
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPeopleCtrl();
    }
    protected virtual void LoadPeopleCtrl()
    {
        if (peopleCtrl != null) return;
        peopleCtrl = GetComponent<PeopleCtrl>();
        Debug.Log(transform.name + ": LoadPeopleCtrl");
    }
    //float ift = 100f;

    //void Infection()
    //{
    //    if (this.peopleCtrl.peopleHealthInfo.virusInfo.virusName == VirusName.noVirus) return;
    //    var objs = Physics.OverlapSphere(transform.position + new Vector3(0f, 1.5f, 0f), infectionRadius, WhatIsInfection);
    //    foreach (Collider col in objs)
    //    {
    //        if (col.gameObject.Equals(this.gameObject)) return;
            
    //        if (Time.time > currentDelayTime)
    //        {
    //            currentDelayTime = Time.time + infectionDelayTime;
                
    //            ift = Random.Range(0, 100f);
                
    //        }
    //        PeopleCtrl peopleCtrls = col.GetComponent<PeopleCtrl>();
    //        if (peopleCtrls != null)
    //        {
    //            if (peopleCtrls.peopleHealthInfo.virusInfo.virusName != VirusName.noVirus) return;
    //            if (ift < this.peopleCtrl.peopleHealthInfo.virusInfo.infectionRate)
    //            {
    //                peopleCtrls.peopleHealthInfo.virusInfo.virusName = this.peopleCtrl.peopleHealthInfo.virusInfo.virusName;
    //                Debug.Log(peopleCtrls.transform.name + " is infected virus "+ peopleCtrls.peopleHealthInfo.virusInfo.virusName);
    //            }


    //        }
    //    }
        
    //}
    //void Infected(float infectionRate)
    //{
    //    if (infectionRate < peopleCtrl.peopleHealthInfo.virusInfo.infectionRate)
    //    {

    //    }
    //}

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position + new Vector3(0f, 1.5f, 0f), infectionRadius);
    }
}
