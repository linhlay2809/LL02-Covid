using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsUI : MonoBehaviour
{
    [SerializeField] protected Slider moraleSlider;
    [SerializeField] protected Slider energySlider;

    // Giảm chỉ số tinh thần
    public void ReduceMoraleStat(int value)
    {
        moraleSlider.value -= value;
    }

    // Giảm chỉ số năng lượng
    public void ReduceEnergyStat(int value)
    {
        energySlider.value -= value;
    }

    // Tăng chỉ số tinh thần
    public void IncreaseMoraleStat(int value)
    {
        moraleSlider.value += value;
    }

    // Tăng chỉ số năng lượng
    public void IncreaseEnergyStat(int value)
    {
        energySlider.value += value;
    }
}
