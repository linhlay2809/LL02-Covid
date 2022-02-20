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
    public List<Sprite> avatarPeople = new List<Sprite>();

    [SerializeField] protected List<VirusInfo> virusInfos;
    [SerializeField] protected List<MedicineInfo> medicineInfos;
    [SerializeField] protected List<VaccineInfo> vaccineInfos;

    private static GameManager instance;

    public static GameManager Instance => instance;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadAvatarPeople();
        LoadVirusInfos();
        LoadMedicineInfos();
        LoadVaccineInfos();
    }
    
    // Lấy giá trị maxInfectionRate
    public float GetMaxIR(int index)
    {
        return virusInfos[index].maxInfectionRate;
    }

    // Load VaccineInfos trên inspector
    protected void LoadVaccineInfos()
    {
        if (vaccineInfos.Count != 0) return;
        for (int i = 0; i < Enum.GetNames(typeof(VaccineName)).Length; i++)
        {
            VaccineInfo info = new VaccineInfo() 
            { 
                vaccineName = (VaccineName)Enum.ToObject(typeof(VaccineName), i) 
            };
            vaccineInfos.Add(info);
        }
    }

    // Load MedicineInfo trên inspecter
    protected void LoadMedicineInfos()
    {
        if (medicineInfos.Count != 0) return;
        for (int i = 0; i < Enum.GetNames(typeof(MedicineName)).Length; i++)
        {
            MedicineInfo info = new MedicineInfo() 
            { 
                medicineName = (MedicineName)Enum.ToObject(typeof(MedicineName), i) 
            };
            medicineInfos.Add(info);
        }
    }

    // Get MedicineInfo
    public MedicineInfo GetMedicineInfo(int medicineIndex)
    {
        return medicineInfos[medicineIndex];
    }

    // Get VaccineInfo
    public VaccineInfo GetVaccineInfo(int vaccineIndex)
    {
        return vaccineInfos[vaccineIndex];
    }

    // Get vaccineInfos với VaccineName truyền vào
    public VaccineInfo GetVaccineInfoByName(VaccineName name)
    {
        switch ((int)name)
        {
            case 0:
                return vaccineInfos[0];
            case 1:
                return vaccineInfos[1];
            case 2:
                return vaccineInfos[2];
            case 3:
                return vaccineInfos[3];
            default:
                return null;
        }
    }

    // Thêm số lượng thuốc
    public void AddMedicineQuantily(int medicineIndex, int value)
    {
        this.medicineInfos[medicineIndex].quantily += value;
    }

    // Thêm số lượng vaccine
    public void AddVaccineQuantily(int medicineIndex, int value)
    {
        this.vaccineInfos[medicineIndex].quantily += value;
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
    protected void LoadAvatarPeople()
    {
        if (avatarPeople.Count != 0) return;
        for (int i = 0; i < 10; i++)
        {
            Sprite sprite = Resources.Load<Sprite>("AvatarPeople/AvatarPeople" + i);
            if (sprite == null) break;
            avatarPeople.Add(sprite);
        }
    }

    protected override void Awake()
    {
        base.Awake();
        if (instance != null) return;
        instance = this;
    }

}
