using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MainBehaviour
{
    [HideInInspector] public List<string> strCovidList = new List<string>() { "No Virus", "Delta Virus", "Alpha Virus", "Beta Virus", "Gamma Virus" };
    [HideInInspector] public List<string> strVaccineList = new List<string>() { "No Vaccine", "Pfizer", "Astra Zeneca", "Vero Cell" };
    [HideInInspector] public List<string> strDoseList = new List<string>() { "Zero Dose", "One Dose", "Two Dose" };

    [SerializeField] protected bool tutorial;
    [SerializeField] protected List<VirusInfo> virusInfos;

    private static GameManager instance;
    public static GameManager Instance => instance;
    protected override void Awake()
    {
        base.Awake();
        if (instance != null) 
        {
            Debug.Log("Dont destroy");
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadVirusInfos();
    }
    
    // Lấy giá trị maxInfectionRate
    public float GetMaxIR(int index)
    {
        return virusInfos[index].maxInfectionRate;
    }

    // Lấy giá trị bool tutorial
    public bool GetTutorial()
    {
        return this.tutorial;
    }

    // Load VirusInfo trên inspector
    protected void LoadVirusInfos()
    {
        if (virusInfos.Count != 0) return;
        for (int i = 0; i < Enum.GetNames(typeof(VirusName)).Length; i++)
        {
            VirusInfo info = new VirusInfo() { virusName= (VirusName)Enum.ToObject(typeof(VirusName), i)};
            virusInfos.Add(info);
        }
    }

    public void SetTutorial(bool value)
    {
        this.tutorial = value;
    }

}
