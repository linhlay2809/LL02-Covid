using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MedicineStoreUI : MonoBehaviour
{
    public void OpenUI()
    {
        if (!this.gameObject.activeInHierarchy)
        {
            this.gameObject.SetActive(true);
            this.gameObject.transform.DOScale(Vector2.one, 0.5f).SetEase(Ease.OutBack).From(Vector2.zero);
        }
        else
        {
            this.gameObject.transform.DOScale(Vector2.zero, 0.5f).SetEase(Ease.InBack).OnComplete(DisableUI);
        }
            
    }
    protected void DisableUI()
    {
        this.gameObject.SetActive(false);
    }
}
