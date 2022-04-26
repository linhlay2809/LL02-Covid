using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceUI : MainBehaviour
{
    [SerializeField] protected int money;
    [SerializeField] protected Resource moneyRes;
    [SerializeField] protected Resource infectionRateRes;
    [SerializeField] protected Resource numberOfPeopleRes;

    [SerializeField] protected float waitingTime;
    [SerializeField] protected bool stoped = false;
    float currentTime = 2f;

    protected void Start()
    {
        DisplayMoney();
    }
    protected override void Update()
    {
        Timer();
    }
    public int GetMoney()
    {
        return this.money;
    }
    // Thêm số tiền
    public void AddMoney(int value)
    {
        this.money += value;
        DisplayMoney();
    }
    // Bộ đếm thời gian
    protected void Timer() // Gọi hàm sau waitingTime
    {
        if (stoped) return;
        if (Time.time > currentTime)
        {
            currentTime = Time.time + waitingTime;
            DisplayIRRes();
            DisplayNumberOfPeo();
            DisplayMoney();
        }
    }

    // Hiển thị thông số tỷ lệ lây nhiễm lên ResourceUI
    protected void DisplayIRRes()
    {
        float allIR = PeopleManager.Instance.GetAllInfectionRate();
        if (allIR <= 0) stoped = true;
        infectionRateRes.SetResText(allIR.ToString("0.##") + " / 100");
    }

    protected void DisplayNumberOfPeo()
    {
        numberOfPeopleRes.SetResText(PeopleManager.Instance.GetAllPeople().ToString());
    }
    protected void DisplayMoney()
    {
        moneyRes.SetResText(GetMoney() + " $");
    }
}
