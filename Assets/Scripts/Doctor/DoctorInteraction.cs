using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class DoctorInteraction : MainBehaviour
{
    [HideInInspector]
    public DoctorCtrl doctorCtrl;
    [SerializeField] protected LayerMask whatIsInteract;
    [SerializeField] protected Transform canvas;
    [SerializeField] protected GameObject interactUI;
    [SerializeField] protected GameObject hintUI;
    [SerializeField] bool isInteract = false;

    [SerializeField] protected List<Button> buttons;
    [SerializeField] protected List<Button> vaccineButtons;

    GameObject vaccineIR;
    RaycastHit hit;

    // Button
    private ButtonDoctorBase btntestCV; 
    private ButtonDoctorBase btnTreat; 
    private ButtonDoctorBase btnVaccine1; 
    private ButtonDoctorBase btnVaccine2;
    private ButtonDoctorBase btnVaccine3;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadDoctorCtrl();
        LoadCanvas();
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

    /// <summary>
    /// Main code
    /// </summary>

    protected void Start()
    {
        btntestCV = buttons[0].GetComponent<ButtonDoctorBase>();
        btnTreat = buttons[1].GetComponent<ButtonDoctorBase>();
        btnVaccine1 = vaccineButtons[0].GetComponent<ButtonDoctorBase>();
        btnVaccine2 = vaccineButtons[1].GetComponent<ButtonDoctorBase>();
        btnVaccine3 = vaccineButtons[2].GetComponent<ButtonDoctorBase>();

        interactUI.SetActive(false);

        vaccineIR = interactUI.transform.GetChild(1).gameObject; // Gán object vaccineInteractUI vào inspector khi startGame
        buttons[2].onClick.AddListener(() => OnOffVaccineInteract());
    }
    protected override void FixedUpdate()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(this.transform.position);
        interactUI.transform.position = pos; // Cập nhật vị trí interactUI theo player
        hintUI.transform.position = pos;
    }
    protected override void Update()
    {
        Vector3 hitPos = transform.position + new Vector3(0f, 1.5f, 0f); // Vị trí bắt đầu RayCast

        if (Physics.Raycast(hitPos, transform.TransformDirection(Vector3.forward), out hit, 2f, whatIsInteract))
        {
            hintUI.SetActive(true);
            Debug.DrawRay(hitPos, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

            if (!Input.GetKeyDown(KeyCode.E)) return;
            InteractSound();
            if (hit.collider.CompareTag("NPC"))
            {
                NPCFuntionBase funtion = hit.collider.GetComponent<NPCFuntionBase>();
                if (funtion == null) return;
                funtion.ToggleFuntion();
                doctorCtrl.controller.SwitchIsMoving();
            }

            if (hit.collider.CompareTag("People"))
            {
                PeopleCtrl peopleCtrl = hit.collider.GetComponent<PeopleCtrl>();
                if (peopleCtrl == null) return;
                peopleCtrl.peopleNavCtrl.SetIsMoving(false);
                EnableInteractUI(peopleCtrl);
            }
        }
        else
        {
            hintUI.SetActive(false);
            Debug.DrawRay(hitPos, transform.TransformDirection(Vector3.forward) * 2f, Color.white);
        }
    }

    // Bật tắt cửa sổ InteractUI
    public void EnableInteractUI(PeopleCtrl peopleCtrl)
    {
        isInteract = !isInteract;
        vaccineIR.SetActive(false);
        if (isInteract)
        {
            interactUI.SetActive(true);
            DOTweenModuleUI.DOSizeDelta(interactUI.GetComponent<RectTransform>(), new Vector2(350, 350), 0.2f).From(new Vector2(350, 150));

            MainUISetting.Instance.infoPeopleUI.TurnOffDisplayPeople();
            doctorCtrl.controller.SwitchIsMoving();

            SetDataButton(peopleCtrl);
        }
        else
        {
            DOTweenModuleUI.DOSizeDelta(interactUI.GetComponent<RectTransform>(), new Vector2(350, 150), 0.1f);

            peopleCtrl.peopleNavCtrl.SetIsMoving(true);

            doctorCtrl.controller.SwitchIsMoving();
        }
    }

    // Set data cho cac doctor button
    protected void SetDataButton(PeopleCtrl peopleCtrl)
    {
        btntestCV.SetDataPeoCtrl(peopleCtrl);
        btnTreat.SetDataPeoCtrl(peopleCtrl);
        btnVaccine1.SetDataVaccAndPeoCtrl(MainUISetting.Instance.inventoryUI.GetVaccineInfo(1), peopleCtrl);
        btnVaccine2.SetDataVaccAndPeoCtrl(MainUISetting.Instance.inventoryUI.GetVaccineInfo(2), peopleCtrl);
        btnVaccine3.SetDataVaccAndPeoCtrl(MainUISetting.Instance.inventoryUI.GetVaccineInfo(3), peopleCtrl);
    }

    // Bật tắt VaccineUI
    public void OnOffVaccineInteract()
    {
        InteractSound();
        vaccineIR.SetActive(!vaccineIR.activeInHierarchy);
    }

    protected void InteractSound()
    {
        SoundManager.Instance.Play("Interact");
    }
}
