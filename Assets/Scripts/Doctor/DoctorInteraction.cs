using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoctorInteraction : MainBehaviour
{
    public DoctorCtrl doctorCtrl;


    public GameObject canvas;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadDoctorCtrl();
    }

    // Load PeopleCtrl trên inspector
    protected virtual void LoadDoctorCtrl()
    {
        if (doctorCtrl != null) return;
        doctorCtrl = GetComponent<DoctorCtrl>();
        Debug.Log(transform.name + ": LoadDoctorCtrl");
    }

    protected override void Update()
    {
        //Vector3 pos= Camera.main.WorldToScreenPoint(this.transform.position);
        //canvas.transform.position = pos;
    }
    protected override void FixedUpdate()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        RaycastHit hit;
        Vector3 hitPos = transform.position + new Vector3(0f, 1.5f, 0f); // Vị trí bắt đầu RayCast

        if (Physics.Raycast(hitPos, transform.TransformDirection(Vector3.forward), out hit, 2f, layerMask))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PeopleCtrl peopleCtrl = hit.collider.GetComponent<PeopleCtrl>();
                if (peopleCtrl != null)
                {
                    if (peopleCtrl.peopleHealthInfo.GetBeTreated()) return;
                    int medicineIndex = (int)peopleCtrl.peopleHealthInfo.VirusName;
                    if (this.doctorCtrl.doctorHealing.GetMedicineInfo(medicineIndex).quantily > 0)
                    {
                        peopleCtrl.peopleTreated.BeTreated(); // Set BeingTreated == true cho people
                        this.doctorCtrl.doctorHealing.AddQuantily(medicineIndex, -1); // Cập nhật số lượng thuốc
                    }
                        
                }
            }
            Debug.DrawRay(hitPos, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
        }
        else
        {
            Debug.DrawRay(hitPos, transform.TransformDirection(Vector3.forward) * 2f, Color.white);
        }
    }
}
