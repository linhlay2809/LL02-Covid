using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleInfected : MainBehaviour
{
    [HideInInspector]
    public PeopleCtrl peopleCtrl;
    [Tooltip("Tỷ lệ lây nhiễm tối đa")]
    [SerializeField] protected float maxInfectionRate;


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

    // Gán giá trị truyền vào cho maxInfectionRate
    public void SetMaxInfectionRate(float index)
    {
        this.maxInfectionRate = index - this.peopleCtrl.peopleTreated.GetReduceInfectionRate() * (int)peopleCtrl.peopleHealthInfo.NumberOfDoses;
    }

    

    // Đang bị object bị nhiễm lây bệnh
    public void Infected(float infectionRate, VirusName virusName)
    {
        float ratio = Random.Range(0f, 100f);
        if (ratio < infectionRate) 
        {
            this.peopleCtrl.peopleHealthInfo.VirusName = virusName;

            SetMaxInfectionRate(GameManager.Instance.GetMaxIR((int)virusName) );
            peopleCtrl.peopleTreated.AddTimeToDeath(this.peopleCtrl.peopleTreated.GetReduceInfectionRate() * (int)peopleCtrl.peopleHealthInfo.NumberOfDoses);

            Debug.LogWarning(transform.name + " is infected virus "+ virusName);
        }
    }
}
