using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreNPC : NPCFuntionBase
{
    public override void ToggleFuntion()
    {
        MainUISetting.Instance.medicineStoreUI.OpenUI();
    }
}
