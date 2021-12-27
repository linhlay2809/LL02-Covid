using System;
using UnityEngine;

[Serializable]
public class VaccineInfo
{
    public VaccineName vaccineName;
    [Tooltip("Số lượng")]
    public int quantily;
    [Tooltip("Độ bảo vệ")]
    public float protectionRate;
}
