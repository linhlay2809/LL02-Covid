using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleManager : MainBehaviour
{
    [SerializeField] protected List<VirusInfo> virusInfos;
    private static PeopleManager instance;

    public static PeopleManager Instance => instance;
    protected override void Awake()
    {
        base.Awake();
        if (instance != null) return;
        instance = this;
    }

    // Lấy giá trị maxInfectionRate
    public float GetMaxIR(int index)
    {
        return virusInfos[index].maxInfectionRate;
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadVirusInfos();
    }

    // Load VirusInfo trên inspector
    protected void LoadVirusInfos()
    {
        for (int i = 0; i < Enum.GetNames(typeof(VirusName)).Length; i++)
        {
            VirusInfo info = new VirusInfo();
            info.virusName = (VirusName)Enum.ToObject(typeof(VirusName), i);
            virusInfos.Add(info);

        }
    }
}
