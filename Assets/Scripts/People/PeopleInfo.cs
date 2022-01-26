using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PeopleInfo : MainBehaviour
{
    [SerializeField] protected Sprite avatar;
    [SerializeField] protected int id;

    public Sprite GetAvatarPeople()
    {
        return this.avatar;
    }

    public int GetIDPeople()
    {
        return this.id;
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
    }
    protected void Start()
    {
        avatar = GameManager.Instance.avatarPeople[0];
        id = Random.Range(0, 1000000);
    }

}
