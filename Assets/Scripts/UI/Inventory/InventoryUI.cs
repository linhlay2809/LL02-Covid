using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MainBehaviour
{
    [SerializeField] protected List<Item> items;
    [SerializeField] protected List<Item> potionItems;

    [SerializeField] protected List<MedicineInfo> medicineInfos;
    [SerializeField] protected List<PotionInfos> potionInfos;
    [SerializeField] protected List<VaccineInfo> vaccineInfos;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadItemList();
        LoadPotionItemList();
        LoadMedicineInfos();
        LoadPotionInfos();
        LoadVaccineInfos();
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

    // Load PotionInfo trên inspecter
    protected void LoadPotionInfos()
    {
        if (potionInfos.Count != 0) return;
        for (int i = 0; i < Enum.GetNames(typeof(PotionName)).Length; i++)
        {
            PotionInfos info = new PotionInfos()
            {
                potionName = (PotionName)Enum.ToObject(typeof(PotionName), i)
            };
            potionInfos.Add(info);
        }
    }
    // Load items trên inspector
    protected void LoadItemList()
    {
        if (items.Count != 0) return;
        Item[] childItem = gameObject.transform.GetChild(0).GetComponentsInChildren<Item>();
        foreach (Item item in childItem)
        {
            items.Add(item);
        }
        Debug.Log(transform.name + ": LoadItemList");
    }

    // Load potionItems trên inspector
    protected void LoadPotionItemList()
    {
        if (potionItems.Count != 0) return;
        Item[] childItem = gameObject.transform.GetChild(1).GetComponentsInChildren<Item>();
        foreach (Item item in childItem)
        {
            potionItems.Add(item);
        }
        Debug.Log(transform.name + ": LoadPotionItemList");
    }
    /*---------------------------------------------------
      -----------     Get set các biến      ----------------- 
      ---------------------------------------------------*/


    // Get MedicineInfo
    public PotionInfos GetPotionInfo(int potionIndex)
    {
        return potionInfos[potionIndex];
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

    /*---------------------------------------------------
      -----------     Thao tác hàm       ----------------- 
      ---------------------------------------------------*/

    // Thêm số lượng thuốc
    public void AddMedicineQuantily(int medicineIndex, int value)
    {
        this.medicineInfos[medicineIndex].quantily += value;
        DisplayIventory();
    }

    // Thêm số lượng vaccine
    public void AddVaccineQuantily(int vaccinendex, int value)
    {
        this.vaccineInfos[vaccinendex].quantily += value;
        DisplayIventory();
    }
    // Thêm số lượng lọ thuốc
    public void AddPotionQuantily(int potionIndex, int value)
    {
        this.potionInfos[potionIndex].quantily += value;
        DisplayPotionItem();
    }

    protected void Start()
    {
        DisplayIventory();
        DisplayPotionItem();
    }

    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            PotionInfos moralePotion = this.potionInfos[0];
            if (moralePotion.quantily == 0)
            {
                MainUISetting.Instance.notifyUI.FindAndShowNotify(NotifyName.notEnough);
                return;
            }
            if (MainUISetting.Instance.playerStatsUI.IsMaxMorale())
            {
                MainUISetting.Instance.notifyUI.FindAndShowNotify(NotifyName.maxMorale);
                return;
            }

            MainUISetting.Instance.playerStatsUI.IncreaseMoraleStat(moralePotion.value); // Tăng cường tinh thần
            AddPotionQuantily(0, -1); // Giảm số lượng thuốc tinh thần trên inventory

        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            PotionInfos energyPotion = this.potionInfos[1];
            if (energyPotion.quantily == 0)
            {
                MainUISetting.Instance.notifyUI.FindAndShowNotify(NotifyName.notEnough);
                return;
            }
                
            if (MainUISetting.Instance.playerStatsUI.IsMaxEnergy())
            {
                MainUISetting.Instance.notifyUI.FindAndShowNotify(NotifyName.maxEnergy);
                return;
            }
                

            MainUISetting.Instance.playerStatsUI.IncreaseEnergyStat(energyPotion.value); // Tăng cường năng lượng
            AddPotionQuantily(1, -1); // Giảm số lượng thuốc năng lượng trên inventory
        }
    }

    // Hiển thị số lượng thuốc trên inventory
    public void DisplayIventory() 
    {
        // Display Medicine Item
        items[0].DisplayItem(GetMedicineInfo(0).quantily);
        items[1].DisplayItem(GetMedicineInfo(1).quantily);
        items[2].DisplayItem(GetMedicineInfo(2).quantily);
        items[3].DisplayItem(GetMedicineInfo(3).quantily);
        // Display Vaccine Item
        items[4].DisplayItem(GetVaccineInfo(1).quantily);
        items[5].DisplayItem(GetVaccineInfo(2).quantily);
        items[6].DisplayItem(GetVaccineInfo(3).quantily);
    }

    // Hiển thị số lượng lọ thuốc trên inventory
    public void DisplayPotionItem()
    {
        // Display Potion Item
        potionItems[0].DisplayItem(GetPotionInfo(0).quantily);
        potionItems[1].DisplayItem(GetPotionInfo(1).quantily);
    }
}
