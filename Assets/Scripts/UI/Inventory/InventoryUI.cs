using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MainBehaviour
{
    [SerializeField] protected List<Item> items;
    [SerializeField] protected List<Item> potionItems;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadItemList();
        LoadPotionItemList();
    }

    protected void Start()
    {
        DisplayIventory();
        DisplayPotionItem();
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

    // Hiển thị số lượng thuốc trên inventory
    public void DisplayIventory() 
    {
        GameManager gameManager = GameManager.Instance;
        // Display Medicine Item
        items[0].DisplayItem(gameManager.GetMedicineInfo(0).quantily);
        items[1].DisplayItem(gameManager.GetMedicineInfo(1).quantily);
        items[2].DisplayItem(gameManager.GetMedicineInfo(2).quantily);
        items[3].DisplayItem(gameManager.GetMedicineInfo(3).quantily);
        // Display Vaccine Item
        items[4].DisplayItem(gameManager.GetVaccineInfo(1).quantily);
        items[5].DisplayItem(gameManager.GetVaccineInfo(2).quantily);
        items[6].DisplayItem(gameManager.GetVaccineInfo(3).quantily);
        
    }

    // Hiển thị số lượng lọ thuốc trên inventory
    public void DisplayPotionItem()
    {
        // Display Potion Item
        potionItems[0].DisplayItem(GameManager.Instance.GetPotionInfo(0).quantily);
        potionItems[1].DisplayItem(GameManager.Instance.GetPotionInfo(1).quantily);
    }
}
