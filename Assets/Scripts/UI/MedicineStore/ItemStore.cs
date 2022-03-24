using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemStore : MonoBehaviour
{
    [SerializeField] protected ItemStoreSO itemStore;
    [SerializeField] protected Image icon;
    [SerializeField] protected TextMeshProUGUI nameText;
    [SerializeField] protected TextMeshProUGUI priceText;
    [SerializeField] protected Button buyButton;
    void Awake()
    {
        DisplayInfo();
        buyButton.onClick.AddListener(() => ButtonEvent());
    }

    public void ButtonEvent()
    {
        if (MainUISetting.Instance.resourceUI.GetMoney() < itemStore.price)
        {
            MainUISetting.Instance.notifyUI.FindAndShowNotify(NotifyName.notEnoughMoney);
            return;
        }

        MainUISetting.Instance.resourceUI.AddMoney(-itemStore.price);
        if (itemStore.isMedicine)
        {
            MainUISetting.Instance.inventoryUI.AddMedicineQuantily(itemStore.type, 1);
        }
        else if (itemStore.isVaccine)
        {
            MainUISetting.Instance.inventoryUI.AddVaccineQuantily(itemStore.type, 1);
        }
        else
        {
            MainUISetting.Instance.inventoryUI.AddPotionQuantily(itemStore.type, 1);
        }
    }
    protected void DisplayInfo()
    {
        icon.sprite = itemStore.icon;
        nameText.text = itemStore.name;
        priceText.text = itemStore.price + "$";
    }
}
