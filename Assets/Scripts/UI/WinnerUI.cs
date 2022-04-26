using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WinnerUI : MainBehaviour
{
 
    public void Winner()
    {
        this.gameObject.SetActive(true);
        this.gameObject.transform.DOLocalMoveY(0f, 1f).From(new Vector2(0f, 777f)).SetEase(Ease.Linear);
    }
}
