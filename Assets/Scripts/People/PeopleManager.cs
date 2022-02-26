using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleManager : MainBehaviour
{
    private static PeopleManager instance;
    public static PeopleManager Instance => instance;

    [SerializeField] protected List<PeopleCtrl> peopleCtrls;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null) return;
        instance = this;
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPeopleCtrls();
    }

    // LoadPeopleCtrls trên inspector
    protected void LoadPeopleCtrls()
    {
        if (peopleCtrls.Count != 0) return;
        PeopleCtrl[] getChild = transform.GetComponentsInChildren<PeopleCtrl>();
        foreach (PeopleCtrl child in getChild)
        {
            peopleCtrls.Add(child);
        }
        Debug.Log(transform.name + ": LoadPeoples");
    }

    // Xử lý phép tính tổng tỷ lệ lây nhiễm của thành phố và trả về kết quả
    public float GetAllInfectionRate()
    {
        
        float allIR = 0f; 
        foreach (PeopleCtrl peopleCtrl in peopleCtrls)
        {
            float maxIR = peopleCtrl.peopleInfected.GetMaxInfectionRate();
            if (maxIR == 0) continue;
            else
                allIR += (peopleCtrl.peopleHealthInfo.InfectionRate / maxIR);
            
        }
        return allIR * 100 / peopleCtrls.Count;
        
    }

    public int GetAllPeople()
    {
        return peopleCtrls.Count;
    }

    public void AddPepleToList(GameObject newPeople)
    {
        peopleCtrls.Add(newPeople.GetComponent<PeopleCtrl>());
    }
}



