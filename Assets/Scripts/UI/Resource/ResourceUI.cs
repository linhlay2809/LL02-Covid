using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceUI : MainBehaviour
{
    [SerializeField] protected Resource moneyRes;
    [SerializeField] protected IRResource infectionRateRes;
    [SerializeField] protected Resource numberOfPeopleRes;

    [SerializeField] protected float waitingTime;
    float currentTime = 2f;

    protected override void Update()
    {
        Timer();
    }

    // Bộ đếm thời gian
    protected void Timer() // Gọi hàm sau waitingTime
    {
        if (Time.time > currentTime)
        {
            currentTime = Time.time + waitingTime;
            DisplayIRRes();
        }
    }

    // Hiển thị thông số tỷ lệ lây nhiễm lên ResourceUI
    protected void DisplayIRRes()
    {
        infectionRateRes.SetResText(PeopleManager.Instance.GetAllInfectionRate());
    }
}
