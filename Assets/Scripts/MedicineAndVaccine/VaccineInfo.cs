using System;
using UnityEngine;

[Serializable]
public class VaccineInfo
{
    public VaccineName vaccineName;
    [Tooltip("Số lượng")]
    public int quantily;
    [Tooltip("Độ bảo vệ [0 -> 2]")]
    [Range(0f, 2f)]
    public float protectionRate;
}
