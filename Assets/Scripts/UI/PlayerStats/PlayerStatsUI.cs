using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerStatsUI : MonoBehaviour
{
    [SerializeField] protected Slider moraleSlider;
    [SerializeField] protected Slider energySlider;
    [SerializeField] protected Image moraleArrow;
    [SerializeField] protected Image energyArrow;

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
        energyArrow.transform.DOScaleX(1, 0f);
        energyArrow.gameObject.SetActive(true);
        energyArrow.color = Color.red;
        float moraleValue = moraleSlider.value;
        if (moraleValue <= 20)
            DOTweenModuleUI.DOValue(energySlider, energySlider.value - value - lowLevelValue, 1);
        else if (moraleValue <= 60)
            DOTweenModuleUI.DOValue(energySlider, energySlider.value - value - mediumLevelValue, 1);
        else
            DOTweenModuleUI.DOValue(energySlider, energySlider.value - value, 1);
        energyArrow.transform.DOLocalMoveX(200, 0.3f).SetEase(Ease.InQuart)
            .From(new Vector2(213, energyArrow.transform.position.y)).SetLoops(4, LoopType.Yoyo).OnComplete(DisableEnergyArrow);
    }
    void DisableEnergyArrow()
    {
        energyArrow.gameObject.SetActive(false);
    }
    //void DisableMoraleArrow()
    //{
    //    moraleArrow.gameObject.SetActive(false);
    //}


    // Tăng chỉ số tinh thần
    public void IncreaseMoraleStat(float value)
    {
        DOTweenModuleUI.DOValue(energySlider, moraleSlider.value + value, 1);
    }

    // Tăng chỉ số năng lượng
    public void IncreaseEnergyStat(float value)
    {
        energyArrow.transform.DOScaleX(-1, 0f);
        energyArrow.color = Color.green;
        DOTweenModuleUI.DOValue(energySlider, energySlider.value + value, 1);
        energyArrow.transform.DOLocalMoveX(213, 0.3f).SetEase(Ease.InQuart)
            .From(new Vector2(200, energyArrow.transform.position.y)).SetLoops(4, LoopType.Yoyo).OnComplete(DisableEnergyArrow);
    }
}
