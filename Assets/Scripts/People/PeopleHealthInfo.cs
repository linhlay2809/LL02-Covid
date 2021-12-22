using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleHealthInfo : MainBehaviour
{
    public VirusInfo virusInfo;

    [Tooltip("Tỷ lệ tử vong")]
    [SerializeField] protected float maxInfectionRate;
    [SerializeField] protected float deathRate;
    [SerializeField] protected float timeToDeath;
    [SerializeField] protected bool isInfected = false;
    float rate;
    float currentDelayTime = 0;

    public VirusName VirusName
    {
        get { return virusInfo.virusName; }
        set { virusInfo.virusName = value; }
    }
    public float InfectionRate
    {
        get { return virusInfo.infectionRate; }
        set { virusInfo.infectionRate = value; }
    }

    // Gán giá trị truyền vào cho maxInfectionRate
    public void SetMaxInfectionRate(int index)
    {
        this.maxInfectionRate = index;
    }
    
    public void SetIsInfected(bool value)
    {
        this.isInfected = value;
    }
    protected override void Awake()
    {
        timeToDeath = Random.Range(6, 10);
        rate = (100f / (timeToDeath * 60f));
    }
    protected override void Update()
    {
        if (!isInfected) return;
        this.IncreasedInfectionRate();
        this.IncreasedDeathRate();
    }

    // Tăng tỷ lệ lây nhiễm theo thời gian khi bị nhiễm virus
    protected void IncreasedInfectionRate()
    {
        if (InfectionRate >= maxInfectionRate) return;
        this.InfectionRate += 0.0001f;
        
    }
    protected void IncreasedDeathRate()
    {
        if (Time.time > currentDelayTime)
        {
            currentDelayTime = Time.time + 1f;
            AddDeathRate(rate);
        }
    }

    protected void AddDeathRate(float value)
    {
        this.deathRate += value;
        if (deathRate > 100f)
        {
            Destroy(gameObject);
        }
    }


}
