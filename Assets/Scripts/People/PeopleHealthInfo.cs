using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleHealthInfo : MainBehaviour
{
    public PeopleCtrl peopleCtrl;

    [SerializeField] protected VirusName virusName;
    [SerializeField] protected Dose numberOfDoses;
    [Tooltip("Tỷ lệ lây nhiễm")]
    [SerializeField] protected float infectionRate;
    [Tooltip("Tỷ lệ tử vong")]
    [SerializeField] protected float deathRate;
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
    protected override void Awake()
    {
        SetRateToDeath(Random.Range(6, 10)); // Thời gian tử vong
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

    // Lấy giá trị bool beingTreated
    public bool GetBeTreated()
    {
        return this.beingTreated;
    }

    public void SetRateToDeath(float timeToDeath)
    {
        this.peopleCtrl.peopleTreated.SetTimeToDeath(timeToDeath);
        rateToDeath = (100f / (timeToDeath * 60f)); // Tỷ lệ tử vong sau 1 giây
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
        }
    }

    // Tăng tỷ lệ tử vong theo thời gian (1s)
    protected void IncreasedDeathRate()
    {
        if (Time.time > this.currentDelayTime)
        {
            this.currentDelayTime = Time.time + 1f;
            AddDeathRate(rateToDeath);
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
