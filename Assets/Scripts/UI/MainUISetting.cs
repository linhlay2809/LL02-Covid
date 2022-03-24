using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUISetting : MonoBehaviour
{
    private static MainUISetting instance;
    public static MainUISetting Instance => instance;

    protected void Awake()
    {
        if (instance != null) return;
        instance = this;
    }

    public InfoPeopleUI infoPeopleUI;
    public InventoryUI inventoryUI;
    public ResourceUI resourceUI;
    public PlayerStatsUI playerStatsUI;
    public NotifyUI notifyUI;
    public TutorialUI tutorialUI;
    public MedicineStoreUI medicineStoreUI;
}
