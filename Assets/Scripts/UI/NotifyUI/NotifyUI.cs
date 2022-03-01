using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class NotifyUI : MonoBehaviour
{
    [SerializeField] protected GameObject notifyObj;
    [SerializeField] protected TextMeshProUGUI notifyText;

    // Hiển thị thông báo
    public void ShowNotify(string content)
    {
        notifyObj.transform.DOKill();
        notifyObj.SetActive(true);
        notifyText.text = content;
        notifyObj.transform.localScale = Vector2.zero;
        notifyObj.transform.DOScale(Vector2.one, 0.5f).SetEase(Ease.OutBack).OnComplete(HideNotify);
    }

    // Ẩn thông báo
    protected void HideNotify()
    {
        notifyObj.transform.DOScale(Vector2.zero, 0.4f).SetEase(Ease.InBack).OnComplete(DisableNotify).SetDelay(2);
        
    }

    // Disable Notify
    protected void DisableNotify()
    {
        notifyObj.SetActive(false);
    }
}
