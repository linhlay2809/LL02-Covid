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

        ReduceArrow(moraleArrow);
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

        ReduceArrow(energyArrow);
    }

    // Tăng chỉ số tinh thần
    public void IncreaseMoraleStat(float value)
    {
        DOTweenModuleUI.DOValue(energySlider, moraleSlider.value + value, 1);

        IncreaseArrow(moraleArrow);
    }

    // Tăng chỉ số năng lượng
    public void IncreaseEnergyStat(float value)
    {
        DOTweenModuleUI.DOValue(energySlider, energySlider.value + value, 1);

        IncreaseArrow(energyArrow);
    }

    void ReduceArrow(Image arrow)
    {
        arrow.transform.DOScaleX(1, 0f);
        arrow.color = Color.red;
        arrow.gameObject.SetActive(true);
        arrow.transform.DOLocalMoveX(200, 0.3f).SetEase(Ease.InQuart)
            .From(new Vector2(213, arrow.transform.position.y)).SetLoops(4, LoopType.Yoyo).OnComplete(() => DisableArrow(arrow));
    }
    void IncreaseArrow(Image arrow)
    {
        arrow.transform.DOScaleX(-1, 0f);
        arrow.color = Color.green;
        arrow.gameObject.SetActive(true);
        arrow.transform.DOLocalMoveX(213, 0.3f).SetEase(Ease.InQuart)
            .From(new Vector2(200, arrow.transform.position.y)).SetLoops(4, LoopType.Yoyo).OnComplete(() => DisableArrow(arrow));
    }

    // Disable arrow
    void DisableArrow(Image arrow)
    {
        arrow.gameObject.SetActive(false);
    }
}
