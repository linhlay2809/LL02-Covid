using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MainBehaviour
{
    public List<string> strCovidList = new List<string>() { "No Virus", "Delta Virus", "Alpha Virus", "Beta Virus", "Gamma Virus" };
    public List<string> strVaccineList = new List<string>() { "No Vaccine", "Pfizer", "Astra Zeneca", "Vero Cell" };
    public List<string> strDoseList = new List<string>() { "Zero Dose", "One Dose", "Two Dose" };
    public List<Sprite> avatarPeople = new List<Sprite>();

    private static GameManager instance;

    public static GameManager Instance => instance;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadAvatarPeople();
    }

    protected void LoadAvatarPeople()
    {
        if (avatarPeople.Count != 0) return;
        for (int i = 0; i < 10; i++)
        {
            Sprite sprite = Resources.Load<Sprite>("AvatarPeople/AvatarPeople" + i);
            if (sprite == null) break;
            avatarPeople.Add(sprite);
        }
    }

    protected override void Awake()
    {
        base.Awake();
        if (instance != null) return;
        instance = this;
    }

}
