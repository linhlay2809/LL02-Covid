using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class TooltipTrigger : MainBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] protected string header;
    [Multiline()]
    [SerializeField] protected string content;
    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipSystem.Show(content, header);
        SoundManager.Instance.Play("Tooltip");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.Hide();
    }
}
