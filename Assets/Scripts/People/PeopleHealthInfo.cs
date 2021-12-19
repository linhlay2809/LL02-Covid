using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleHealthInfo : MainBehaviour
{
    public VirusInfo virusInfo;

    [Tooltip("Tỷ lệ tử vong")] 
    [SerializeField] protected float deathRate;

}
