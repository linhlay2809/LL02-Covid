using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleHealthInfo : MainBehaviour
{
    [SerializeField] protected VirusName virusName;
    [Tooltip("Tỷ lệ lây nhiễm")]
    [SerializeField] protected float infectionRate;
    [Tooltip("Tỷ lệ lây nhiễm tối đa")]
    [SerializeField] protected float maxInfectionRate;
    [Tooltip("Tỷ lệ tử vong")]
    [SerializeField] protected float deathRate;
    [Tooltip("Thời gian tử vong")]
    [SerializeField] protected float timeToDeath;
    //[Tooltip("Bị lây nhiễm")]
    //[SerializeField] protected bool isInfected = false;
    [Tooltip("Đang chữa trị")]
    [SerializeField] protected bool beingTreated = false;
    protected float rateToDeath;
    protected float currentDelayTime = 0;

    public VirusName VirusName
    {
        get { return virusName; }
        set { virusName = value; }
    }
    public float InfectionRate
    {
        get { return infectionRate; }
        set { infectionRate = value; }
    }
    protected override void Awake()
    {
        timeToDeath = Random.Range(6, 10); // Thời gian tử vong
        SetRateToDeath(timeToDeath);
        SetMaxInfectionRate((int)VirusName); // Lấy giá trị int của VirusName
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

    public bool GetBeTreated()
    {
        return this.beingTreated;
    }

    public void SetRateToDeath(float timeToDeath)
    {
        rateToDeath = (100f / (timeToDeath * 60f)); // Tỷ lệ tử vong sau 1 giây
    }

    // Gán giá trị truyền vào cho maxInfectionRate
    public void SetMaxInfectionRate(float index)
    {
        this.maxInfectionRate = index;
    }
    
    //// Gán giá trị bool cho IsInfected
    //public void SetIsInfected(bool value)
    //{
    //    this.isInfected = value;
    //}

    // Gán giá trị bool cho BeingTreated
    public void SetBeingTreated(bool value)
    {
        this.beingTreated = value;
    }

    // Tăng tỷ lệ lây nhiễm theo thời gian
    protected void IncreasedInfectionRate()
    {
        if (InfectionRate >= maxInfectionRate) return;
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
            this.maxInfectionRate = 0f;
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
