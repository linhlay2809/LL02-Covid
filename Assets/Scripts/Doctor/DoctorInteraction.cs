using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DoctorInteraction : MainBehaviour
{
    public DoctorCtrl doctorCtrl;


    public GameObject interactUI;
    [SerializeField] protected List<Button> buttons;
    [SerializeField] protected List<Button> vaccineButtons;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadDoctorCtrl();
        LoadInteractUI();
        LoadButtons();
        LoadVaccineButtons();
    }

    // Load PeopleCtrl trên inspector
    protected virtual void LoadDoctorCtrl()
    {
        if (this.doctorCtrl != null) return;
        this.doctorCtrl = GetComponent<DoctorCtrl>();
        Debug.Log(transform.name + ": LoadDoctorCtrl");
    }

    protected void LoadInteractUI()
    {
        if (interactUI != null) return;
        interactUI = GameObject.Find("InteractUI");
        Debug.Log(transform.name + ": LoadInteractUI");
    }

    // Load Buttons trên inspector
    protected void LoadButtons()
    {
        if (buttons.Count != 0) return;
        Button[] child =interactUI.transform.GetChild(0).GetComponentsInChildren<Button>();
        foreach (Button bt in child)
        {
            buttons.Add(bt);
        }
        Debug.Log(transform.name + ": LoadButtons");
    }

    // Load Buttons trên inspector
    protected void LoadVaccineButtons()
    {
        if (vaccineButtons.Count != 0) return;
        Button[] child = interactUI.transform.GetChild(1).GetComponentsInChildren<Button>();
        foreach (Button bt in child)
        {
            vaccineButtons.Add(bt);
        }
        Debug.Log(transform.name + ": LoadVaccineButtons");
    }

    protected override void FixedUpdate()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(this.transform.position);
        interactUI.transform.position = pos;
    }
    protected override void Update()
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
            //if (Input.GetKeyDown(KeyCode.F))
            //{
            //    PeopleCtrl peopleCtrl = hit.collider.GetComponent<PeopleCtrl>();
            //    if (peopleCtrl == null) return;
            //    VaccineToPeople(this.doctorCtrl.doctorHealing.GetVaccineInfo(0), peopleCtrl);
            //}
            if (Input.GetKeyDown(KeyCode.E))
            {
                PeopleCtrl peopleCtrl = hit.collider.GetComponent<PeopleCtrl>();
                if (peopleCtrl == null) return;
                EnableInteractUI(this.doctorCtrl, peopleCtrl);
            }

            Debug.DrawRay(hitPos, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
        }
        else
        {
            Debug.DrawRay(hitPos, transform.TransformDirection(Vector3.forward) * 2f, Color.white);
        }


    }

    protected void EnableInteractUI(DoctorCtrl doctorCtrl, PeopleCtrl peopleCtrl)
    {
        interactUI.SetActive(!interactUI.activeInHierarchy);
        if (interactUI.activeInHierarchy)
        {
            Debug.Log("Add listen");
            buttons[1].onClick.AddListener(() => TreatToPeople(doctorCtrl, peopleCtrl));
            vaccineButtons[0].onClick.AddListener(() => VaccineToPeople(doctorCtrl.doctorHealing.GetVaccineInfo(0), peopleCtrl));
            vaccineButtons[1].onClick.AddListener(() => VaccineToPeople(doctorCtrl.doctorHealing.GetVaccineInfo(1), peopleCtrl));
            vaccineButtons[2].onClick.AddListener(() => VaccineToPeople(doctorCtrl.doctorHealing.GetVaccineInfo(2), peopleCtrl));
        }
        else
        {
            Debug.Log("Remove");
            buttons[1].onClick.RemoveAllListeners();
            vaccineButtons[0].onClick.RemoveAllListeners();
            vaccineButtons[1].onClick.RemoveAllListeners();
            vaccineButtons[2].onClick.RemoveAllListeners();
        }
    }
    public void OnOffVaccineInteract()
    {
        GameObject vaccineIR = interactUI.transform.GetChild(1).gameObject;
        vaccineIR.SetActive(!vaccineIR.activeInHierarchy);
    }

    public void VaccineToPeople(VaccineInfo vaccineInfo, PeopleCtrl peopleCtrl)
    {
        EnableInteractUI(null, null);

        if (vaccineInfo.quantily <= 0) return;

        peopleCtrl.peopleTreated.Vaccination(vaccineInfo);
    }

    public void TreatToPeople(DoctorCtrl doctorCtrl, PeopleCtrl peopleCtrl)
    {
        EnableInteractUI(null, null);

        peopleCtrl.peopleTreated.BeTreated(doctorCtrl);
    }
}
