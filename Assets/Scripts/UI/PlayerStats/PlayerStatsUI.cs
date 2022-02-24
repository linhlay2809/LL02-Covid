using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerStatsUI : MonoBehaviour
{
    [SerializeField] protected Slider moraleSlider;
    [SerializeField] protected Slider energySlider;

    [Header("Energy Value Detriment Details")]
    [Tooltip("Năng lượng hao tốn cộng thêm khi mức tinh thần <= 60")]
    [SerializeField] protected float mediumLevelValue;
    [Tooltip("Năng lượng hao tốn cộng thêm khi mức tinh thần <= 20")]
    [SerializeField] protected float lowLevelValue;

    // Giảm chỉ số tinh thần
    public void ReduceMoraleStat(float value)
    {
        DOTweenModuleUI.DOValue(energySlider, moraleSlider.value - value, 1);
        
    }

    // Giảm chỉ số năng lượng
    public void ReduceEnergyStat(float value)
    {
        float moraleValue = moraleSlider.value;
        if (moraleValue <= 20)
            DOTweenModuleUI.DOValue(energySlider, energySlider.value - value - lowLevelValue, 1);
        else if (moraleValue <= 60)
            DOTweenModuleUI.DOValue(energySlider, energySlider.value - value - mediumLevelValue, 1);
        else
            DOTweenModuleUI.DOValue(energySlider, energySlider.value - value, 1);

    }

    // Tăng chỉ số tinh thần
    public void IncreaseMoraleStat(float value)
    {
        DOTweenModuleUI.DOValue(energySlider, moraleSlider.value + value, 1);
    }

    // Tăng chỉ số năng lượng
    public void IncreaseEnergyStat(float value)
    {
        DOTweenModuleUI.DOValue(energySlider, energySlider.value + value, 1);
    }
}
