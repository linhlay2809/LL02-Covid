using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public enum NotifyName
{
    notTestedVirus = 0,
    maxNumOfDose = 1,
    hasInfected = 2,
    notInfected = 3,
    hasTreated = 4,

}
public class NotifyUI : MonoBehaviour
{
    
    [SerializeField] protected TextMeshProUGUI notifyText;
    [Header("NotifyOS List")]
    [SerializeField] protected List<NotifySO> notifys;
    // Hiển thị thông báo
    public void ShowNotify(string content)
    {
        this.transform.DOKill();
        this.gameObject.SetActive(true);
        notifyText.text = content;
        this.gameObject.transform.localScale = Vector2.zero;
        this.gameObject.transform.DOScale(Vector2.one, 0.5f).SetEase(Ease.OutBack).OnComplete(HideNotify);
    }

    // Ẩn thông báo
    protected void HideNotify()
    {
        this.gameObject.transform.DOScale(Vector2.zero, 0.4f).SetEase(Ease.InBack).OnComplete(DisableNotify).SetDelay(2);
        
    }

    // Disable Notify
    protected void DisableNotify()
    {
        this.gameObject.SetActive(false);
    }

    // Tìm và hiện Notify với NotifyName truyền vào
    public void FindAndShowNotify(NotifyName notifyName)
    {
        foreach (var nt in notifys)
        {
            if (nt.notifyName == notifyName)
            {
                MainUISetting.Instance.notifyUI.ShowNotify(nt.content);
                return;
            }
        }
    }
}
