using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleHealthInfo : MainBehaviour
{
    [HideInInspector]
    public PeopleCtrl peopleCtrl;

    [SerializeField] protected VirusName virusName;
    [SerializeField] protected Dose numberOfDoses;
    [Tooltip("Tỷ lệ lây nhiễm")]
    [SerializeField] protected float infectionRate;

    [Header("Death Rate Details")] [Tooltip("Tỷ lệ tử vong")]
    [SerializeField] protected float deathRate;
    [SerializeField] protected float waitingTime;[Space]
    [Tooltip("Đang chữa trị")]
    [SerializeField] protected bool beingTreated = false;
    protected float rateToDeath;
    protected float currentDelayTime = 0;

    protected override void LoadComponents()
    {
        base.LoadComponents();
    }

    // Load PeopleCtrl trên inspector
    protected virtual void LoadPeopleCtrl()
    {
        if (peopleCtrl != null) return;
        peopleCtrl = GetComponent<PeopleCtrl>();
        Debug.Log(transform.name + ": LoadPeopleCtrl");
    }

    public VirusName VirusName
    {
        get { return virusName; }
        set { virusName = value; }
    }
    public Dose NumberOfDoses
    {
        get { return numberOfDoses; }
        set { numberOfDoses = value; }
    }
    public float InfectionRate
    {
        get { return infectionRate; }
        set { infectionRate = value; }
    }
    // Lấy giá trị bool beingTreated
    public bool GetBeTreated()
    {
        return this.beingTreated;
    }

    // Lấy giá trị DeathRate
    public float GetDeathRate()
    {
        return this.deathRate;
    }
    protected override void Awake()
    {
        
    }

    protected void Start()
    {
        infectionRate = GameManager.Instance.GetMaxIR((int)VirusName);
        peopleCtrl.peopleInfected.SetMaxInfectionRate(infectionRate);
    }
    protected override void Update()
    {
        if (this.VirusName == VirusName.noVirus) return;
        if (!beingTreated)
        {
            this.IncreasedInfectionRate();
            this.IncreasedDeathRate();
        }
        else
        {
            this.ReduceInfectionRate();
        }

    }

    
    // Gán giá trị bool cho BeingTreated
    public void SetBeingTreated(bool value)
    {
        this.beingTreated = value;
    }

    // Tăng tỷ lệ lây nhiễm theo thời gian
    protected void IncreasedInfectionRate()
    {
        if (InfectionRate >= this.peopleCtrl.peopleInfected.GetMaxInfectionRate()) return;
        this.InfectionRate += 0.0001f;
    }

    // Giảm tỷ lệ lây nhiễm theo thời gian
    protected void ReduceInfectionRate()
    {
        this.InfectionRate -= 0.0001f;
        if (this.InfectionRate <= 0)
        {
            VirusName = VirusName.noVirus;
            SetBeingTreated(false);
            this.InfectionRate = 0f;
            this.deathRate = 0f;
            peopleCtrl.peopleTreated.ResetTimeToDeath();
        }
    }

    // Tăng tỷ lệ tử vong theo thời gian (1s)
    protected void IncreasedDeathRate()
    {
        if (Time.time > this.currentDelayTime)
        {
            this.currentDelayTime = Time.time + this.waitingTime;
            AddDeathRate(100f / (peopleCtrl.peopleTreated.GetTimeToDeath() * 60f));
        }
    }

    // Thêm tỷ lệ tử vong truyền vào value
    protected void AddDeathRate(float value)
    {
        this.deathRate += value;
        if (deathRate > 100f)
        {
            Destroy(this.gameObject);
        }
    }


}
