using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Item : MainBehaviour
{
    [SerializeField] protected TMP_Text itemQuanlity;
    [SerializeField] protected GameObject emptyItemBG;
    
    public void DisplayItem(int value)
    {
        this.itemQuanlity.text = value.ToString();
        if (value == 0)
            emptyItemBG.SetActive(true);
        else
            emptyItemBG.SetActive(false);
    }


}
