using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Funtion : MainBehaviour
{
    
    public virtual void ToggleFuntion()
    {
        MainUISetting.Instance.medicineStoreUI.OpenUI();
    }
}
