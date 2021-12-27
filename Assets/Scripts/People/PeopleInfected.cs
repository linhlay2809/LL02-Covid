using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleInfected : MainBehaviour
{
    public PeopleCtrl peopleCtrl;
    [Tooltip("Tỷ lệ lây nhiễm tối đa")]
    [SerializeField] protected float maxInfectionRate;
    [Tooltip("Tỷ lệ giảm khi bị nhiễm")]
    [SerializeField] protected float reduceInfectionRate = 0;


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

    public float GetMaxInfectionRate()
    {
        return this.maxInfectionRate;
    }

    public void SetReduceInfectionRate(float value)
    {
        this.reduceInfectionRate = value;
    }

    // Gán giá trị truyền vào cho maxInfectionRate
    public void SetMaxInfectionRate(float index)
    {
        this.maxInfectionRate = index - reduceInfectionRate * (int)peopleCtrl.peopleHealthInfo.NumberOfDoses;
    }

    // Đang bị object bị nhiễm lây bệnh
    public void Infected(float infectionRate, VirusName virusName)
    {
        float ratio = Random.Range(0f, 100f);
        if (ratio < infectionRate) 
        {
            this.peopleCtrl.peopleHealthInfo.VirusName = virusName;

            SetMaxInfectionRate(PeopleManager.Instance.GetMaxIR((int)virusName) );

            Debug.LogWarning(transform.name + " is infected virus "+ virusName);
        }
    }
}
