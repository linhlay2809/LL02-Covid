using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleHealthInfo : MainBehaviour
{
    public VirusInfo virusInfo;

    [Tooltip("Tỷ lệ tử vong")] 
    [SerializeField] protected float deathRate;

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
}
