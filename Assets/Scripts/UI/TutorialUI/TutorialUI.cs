using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public enum TutorialName
{
    mission = 0,
    playerStats = 1,
    inventory = 2,
    resource = 3,
    controller = 4,
    interact = 5,

}
public class TutorialUI : MainBehaviour
{
    [SerializeField] protected Image tutorialImg;
    [SerializeField] protected TextMeshProUGUI tittleText;
    [SerializeField] protected TextMeshProUGUI contentText;
    [SerializeField] protected GameObject pressSpace;

    [SerializeField] protected bool isSpace;



    [Header("TutorialOS List")]
    [SerializeField] protected TutorialSO currentTutorial;
    [SerializeField] protected List<TutorialSO> tutorials;

    protected override void Update()
    {
        if (isSpace)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                HideTutorial();
            }
        }
       
    }
    // Hi?n th? h??ng d?n
    public void ShowTutorial(Sprite sprite, string tittle, string content)
    {
        this.isSpace = false;
        this.transform.DOKill();
        this.gameObject.SetActive(true);

        tutorialImg.sprite = sprite;
        tittleText.text = tittle;
        contentText.text = content;

        this.gameObject.transform.localScale = Vector2.zero;
        this.gameObject.transform.DOScale(Vector2.one, 0.5f).SetEase(Ease.OutBack);

        SoundManager.Instance.Play("Tutorial");
        StartCoroutine(ShowPressSpace());
    }

    IEnumerator ShowPressSpace()
    {
        pressSpace.SetActive(false);
        yield return new WaitForSeconds(2f);
        pressSpace.SetActive(true);
        isSpace = true;
    }

    // ?n h??ng d?n
    protected void HideTutorial()
    {
        this.isSpace = false;
        if(this.currentTutorial.nextTutorial == null)
            this.gameObject.transform.DOScale(Vector2.zero, 0.4f).SetEase(Ease.InExpo).OnComplete(DisableTutorial);
        else
            this.gameObject.transform.DOScale(Vector2.zero, 0.4f).SetEase(Ease.InExpo).OnComplete(NextTutorial);
    }

    // Disable Tutorial
    protected void DisableTutorial()
    {
        this.gameObject.SetActive(false);
    }

    // Show next tutorial cua tutorialSO
    protected void NextTutorial()
    {
        var nextTutorial = this.currentTutorial.nextTutorial;
        this.currentTutorial = nextTutorial;
        this.ShowTutorial(nextTutorial.tutorialImg, nextTutorial.tittle, nextTutorial.content);
    }

    // R?m v? hi?n Tutorial v?i TutorialName truy?n v?o
    public void FindAndShowTutorial(TutorialName tutorialName)
    {
        foreach (var tu in tutorials)
        {
            if (tu.tutorialName == tutorialName)
            {
                if (tu.appeared) return;
                this.currentTutorial = tu;
                tu.appeared = true;
                ShowTutorial(tu.tutorialImg, tu.tittle, tu.content);
                return;
            }
        }
    }
    
    public void ActiveTutorial(bool value)
    {
        foreach (TutorialSO tutorialSO in tutorials)
        {
            tutorialSO.appeared = value;
        }
    }
}

