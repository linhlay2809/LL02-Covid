using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TooltipSystem : MainBehaviour
{
    private static TooltipSystem instance;
    [SerializeField] protected Tooltip tooltip;

    protected override void Awake()
    {
        instance = this;
    }

    public static void Show(string content, string header = "")
    {
        instance.tooltip.SetText(content, header);
        instance.tooltip.GetComponent<CanvasGroup>().DOFade(1, 0.5f).SetDelay(0.5f);
        instance.tooltip.gameObject.SetActive(true);

    }

    public static void Hide()
    {
        instance.tooltip.GetComponent<CanvasGroup>().alpha = 0;
        DOTween.KillAll();
        instance.tooltip.gameObject.SetActive(false);
    }

}
