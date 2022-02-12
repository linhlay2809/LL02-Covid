using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DoctorInteraction : MainBehaviour
{
    [HideInInspector]
    public DoctorCtrl doctorCtrl;
    [SerializeField] protected Transform canvas;
    [SerializeField] protected MainUISetting mainUISetting;
    [SerializeField] protected GameObject interactUI;
    [SerializeField] protected GameObject hintUI;

    [SerializeField] protected List<Button> buttons;
    [SerializeField] protected List<Button> vaccineButtons;

    GameObject vaccineIR;
    RaycastHit hit;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadDoctorCtrl();
        LoadCanvas();
        LoadMainUISetting();
        LoadInteractUI();
        LoadHintUI();
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

    // Load canvas
    protected void LoadCanvas()
    {
        if(this.canvas != null) return;
        this.canvas = transform.GetChild(3);
    }

    protected void LoadMainUISetting()
    {
        if(mainUISetting != null) return;
        mainUISetting = GameObject.Find("MainUI").GetComponent<MainUISetting>();
    }
    // Load InteractUI trên inspector
    protected void LoadInteractUI()
    {
        if (interactUI != null) return;
        interactUI = canvas.GetChild(0).gameObject;
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

    // Load hintUI
    protected void LoadHintUI()
    {
        if(hintUI != null) return;
        hintUI = canvas.GetChild(1).gameObject;
        Debug.Log(transform.name + ": LoadHintUI");
    }

    // Load VaccineButtons trên inspector
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
    protected void Start()
    {
        vaccineIR = interactUI.transform.GetChild(1).gameObject; // Gán object vaccineInteractUI vào inspector khi startGame
    }
    protected override void FixedUpdate()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(this.transform.position);
        interactUI.transform.position = pos; // Cập nhật vị trí interactUI theo player
        hintUI.transform.position = pos;
    }
    protected override void Update()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;
        
        Vector3 hitPos = transform.position + new Vector3(0f, 1.5f, 0f); // Vị trí bắt đầu RayCast

        if (Physics.Raycast(hitPos, transform.TransformDirection(Vector3.forward), out hit, 2f, layerMask))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PeopleCtrl peopleCtrl = hit.collider.GetComponent<PeopleCtrl>();
                if (peopleCtrl == null) return;
                EnableInteractUI(peopleCtrl);
            }
            hintUI.SetActive(true);
            Debug.DrawRay(hitPos, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
        }
        else
        {
            hintUI.SetActive(false);
            Debug.DrawRay(hitPos, transform.TransformDirection(Vector3.forward) * 2f, Color.white);
        }


    }

    // Bật tắt cửa sổ InteractUI
    protected void EnableInteractUI(PeopleCtrl peopleCtrl)
    {
        interactUI.SetActive(!interactUI.activeInHierarchy);
        mainUISetting.infoPeopleUI.TurnOffDisplayPeople();
        vaccineIR.SetActive(false);
        if (interactUI.activeInHierarchy)
        {
            Debug.Log("Add listen");
            buttons[0].onClick.AddListener(() => TestCovid(peopleCtrl));
            buttons[1].onClick.AddListener(() => TreatToPeople(peopleCtrl));
            vaccineButtons[0].onClick.AddListener(() => VaccineToPeople(GameManager.Instance.GetVaccineInfo(0), peopleCtrl));
            vaccineButtons[1].onClick.AddListener(() => VaccineToPeople(GameManager.Instance.GetVaccineInfo(1), peopleCtrl));
            vaccineButtons[2].onClick.AddListener(() => VaccineToPeople(GameManager.Instance.GetVaccineInfo(2), peopleCtrl));
        }
        else
        {
            Debug.Log("Remove");
            buttons[0].onClick.RemoveAllListeners();
            buttons[1].onClick.RemoveAllListeners();
            vaccineButtons[0].onClick.RemoveAllListeners();
            vaccineButtons[1].onClick.RemoveAllListeners();
            vaccineButtons[2].onClick.RemoveAllListeners();
        }
    }

    // SetActive interactVaccineUI when vaccineButton is click
    public void OnOffVaccineInteract()
    {
        vaccineIR.SetActive(!vaccineIR.activeInHierarchy);
    }

    // Test Covid cho bệnh nhân
    protected void TestCovid(PeopleCtrl peopleCtrl)
    {
        EnableInteractUI(null);
        mainUISetting.infoPeopleUI.TurnOnDisplayPeople();
        mainUISetting.infoPeopleUI.SetInfoNewPeople(peopleCtrl);
        
    }

    // Tiêm vaccine cho bệnh nhân
    protected void VaccineToPeople(VaccineInfo vaccineInfo, PeopleCtrl peopleCtrl)
    {
        EnableInteractUI(null);

        // Trả về khi số lượng vaccine <= 0
        if (vaccineInfo.quantily <= 0) return;

        peopleCtrl.peopleTreated.Vaccination(vaccineInfo);
    }

    // Chữa trị cho bệnh nhân
    protected void TreatToPeople(PeopleCtrl peopleCtrl)
    {
        EnableInteractUI( null);

        peopleCtrl.peopleTreated.BeTreated();
    }
}
