using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Store", menuName = "Scriptable Object/Item Store")]
public class ItemStoreSO : ScriptableObject
{
    public Sprite icon;
    public string typeName;
    public int price;

    [Header("Type Group")]
    public bool isMedicine;
    public bool isVaccine;
    public bool isPotion;
    public int type;
}
