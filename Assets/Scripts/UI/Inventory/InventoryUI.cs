using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MainBehaviour
{
    public List<Item> items;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadItemList();
    }

    protected void Start()
    {
        DisplayIventory();
    }

    // Load items trên inspector
    protected void LoadItemList()
    {
        if (items.Count != 0) return;
        Item[] childItem = gameObject.transform.GetComponentsInChildren<Item>();
        foreach (Item item in childItem)
        {
            items.Add(item);
        }
        Debug.Log(transform.name + ": LoadItemList");
    }

    // Hiển thị số lượng thuốc trên inventory
    public void DisplayIventory() 
    {
        GameManager gameManager = GameManager.Instance;
        items[0].DisplayItem(gameManager.GetMedicineInfo(0).quantily);
        items[1].DisplayItem(gameManager.GetMedicineInfo(1).quantily);
        items[2].DisplayItem(gameManager.GetMedicineInfo(2).quantily);
        items[3].DisplayItem(gameManager.GetMedicineInfo(3).quantily);
        items[4].DisplayItem(gameManager.GetVaccineInfo(1).quantily);
        items[5].DisplayItem(gameManager.GetVaccineInfo(2).quantily);
        items[6].DisplayItem(gameManager.GetVaccineInfo(3).quantily);
    }

}
