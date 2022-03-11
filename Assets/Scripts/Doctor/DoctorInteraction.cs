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
    [SerializeField] protected Transform canvas;
    [SerializeField] protected GameObject interactUI;
    [SerializeField] protected GameObject hintUI;
    [SerializeField] bool isInteract = false;

    [SerializeField] protected List<Button> buttons;
    [SerializeField] protected List<Button> vaccineButtons;

    GameObject vaccineIR;
    RaycastHit hit;
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
    protected void Start()
    {
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

        int layerMask = 1 << 8;

        layerMask = ~layerMask;
        
        Vector3 hitPos = transform.position + new Vector3(0f, 1.5f, 0f); // Vị trí bắt đầu RayCast

        if (Physics.Raycast(hitPos, transform.TransformDirection(Vector3.forward), out hit, 2f, layerMask))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PeopleCtrl peopleCtrl = hit.collider.GetComponent<PeopleCtrl>();
                if (peopleCtrl == null) return;
                peopleCtrl.peopleNavCtrl.SetIsMoving(false);
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
        isInteract = !isInteract;

        vaccineIR.SetActive(false);
        if (isInteract)
        {
            Debug.Log("Add listen");
            interactUI.SetActive(true);
            DOTweenModuleUI.DOSizeDelta(interactUI.GetComponent<RectTransform>(), new Vector2(350, 350), 0.2f);

            MainUISetting.Instance.infoPeopleUI.TurnOffDisplayPeople();
            doctorCtrl.controller.SwitchIsMoving();

            buttons[0].onClick.AddListener(() => TestCovid(peopleCtrl));
            buttons[1].onClick.AddListener(() => TreatToPeople(peopleCtrl));
            vaccineButtons[0].onClick.AddListener(() => VaccineToPeople(GameManager.Instance.GetVaccineInfo(1), peopleCtrl));
            vaccineButtons[1].onClick.AddListener(() => VaccineToPeople(GameManager.Instance.GetVaccineInfo(2), peopleCtrl));
            vaccineButtons[2].onClick.AddListener(() => VaccineToPeople(GameManager.Instance.GetVaccineInfo(3), peopleCtrl));
        }
        else
        {
            Debug.Log("Remove");
            DOTweenModuleUI.DOSizeDelta(interactUI.GetComponent<RectTransform>(), new Vector2(350, 150), 0.1f);

            peopleCtrl.peopleNavCtrl.SetIsMoving(true);

            doctorCtrl.controller.SwitchIsMoving();

            buttons[0].onClick.RemoveAllListeners();
            buttons[1].onClick.RemoveAllListeners();
            vaccineButtons[0].onClick.RemoveAllListeners();
            vaccineButtons[1].onClick.RemoveAllListeners();
            vaccineButtons[2].onClick.RemoveAllListeners();
        }
    }

    // Bật tắt VaccineUI
    public void OnOffVaccineInteract()
    {
        vaccineIR.SetActive(!vaccineIR.activeInHierarchy);
    }

    // Test Covid cho bệnh nhân
    protected void TestCovid(PeopleCtrl peopleCtrl)
    {
        MainUISetting.Instance.infoPeopleUI.TurnOnDisplayPeople(peopleCtrl);

        EnableInteractUI(peopleCtrl);

        if (peopleCtrl.peopleInfo.IsTested()) return;
        peopleCtrl.peopleInfo.SetIsTested(true);
        MainUISetting.Instance.playerStatsUI.ReduceEnergyStat(10);
    }

    // Tiêm vaccine cho bệnh nhân
    protected void VaccineToPeople(VaccineInfo vaccineInfo, PeopleCtrl peopleCtrl)
    {
        if (!peopleCtrl.peopleInfo.IsTested())
        {
            MainUISetting.Instance.notifyUI.FindAndShowNotify(NotifyName.notTestedVirus);
            return;
        }

        // Trả về khi số lượng vaccine <= 0
        if (vaccineInfo.quantily <= 0) return;

        peopleCtrl.peopleTreated.Vaccination(vaccineInfo);

        EnableInteractUI(peopleCtrl);
    }

    // Chữa trị cho bệnh nhân
    protected void TreatToPeople(PeopleCtrl peopleCtrl)
    {
        if (!peopleCtrl.peopleInfo.IsTested())
        {
            MainUISetting.Instance.notifyUI.FindAndShowNotify(NotifyName.notTestedVirus);
            return;
        }
        peopleCtrl.peopleTreated.BeTreated();

        EnableInteractUI(peopleCtrl);

        

    }
}
