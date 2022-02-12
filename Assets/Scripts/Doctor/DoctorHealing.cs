using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctorHealing : MainBehaviour
{
    //[HideInInspector]
    //public DoctorCtrl doctorCtrl;

    

    protected override void LoadComponents()
    {
        base.LoadComponents();
        
        //LoadDoctorCtrl();
        
    }

    //// Load DoctorCtrl
    //protected void LoadDoctorCtrl()
    //{
    //    if (doctorCtrl != null) return;
    //    this.doctorCtrl = GetComponent<DoctorCtrl>();
    //    Debug.Log(transform.name + ": LoadDoctorCtrl");
    //}

    // Load VaccineInfos trên inspecter
    
    
}
