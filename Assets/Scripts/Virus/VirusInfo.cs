using System;
using UnityEngine;

[Serializable]
public class VirusInfo
{
    public VirusName virusName;
    [Tooltip("Tỷ lệ lây nhiễm")]
    public float infectionRate = 0; // Tỷ lệ lây nhiễm
}
    
