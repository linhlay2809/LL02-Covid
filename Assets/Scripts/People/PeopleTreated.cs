using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleTreated : MainBehaviour
{
    [HideInInspector]
    public PeopleCtrl peopleCtrl;
    [SerializeField] protected VaccineName vaccine;
    [Tooltip("Tỷ lệ giảm khi bị nhiễm")]
    [SerializeField] protected float reduceInfectionRate = 0;
    [Header("Time To Death Details")]
    [Tooltip("Thời gian tử vong")]
    [SerializeField] protected float timeToDeath;
    [SerializeField] protected float defaultTTDeath;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPeopleCtrl();
    }

    protected override void Awake()
    {
        ResetTimeToDeath();
    }

    public VaccineName Vaccine
    {
        get { return vaccine; }
        set { vaccine = value; }
    }

    // Load PeopleCtrl trên inspector
    protected virtual void LoadPeopleCtrl()
    {
        if (peopleCtrl != null) return;
        peopleCtrl = GetComponent<PeopleCtrl>();
        Debug.Log(transform.name + ": LoadPeopleCtrl");
    }

    public float GetTimeToDeath()
    {
        return this.timeToDeath;
    }

    // Set thời gian tử vong
    public void ResetTimeToDeath()
    {
        this.timeToDeath = this.defaultTTDeath;
    }

    // Cộng thêm timeToDeath với giá trị truyền vào
    public void AddTimeToDeath(float value)
    {
        this.timeToDeath += value;
    }

    // Set giảm tỷ lệ lây nhiễm khi tiêm vaccine
    public void SetReduceInfectionRate(float value)
    {
        this.reduceInfectionRate = value;
    }

    // Get tỷ lệ giảm
    public float GetReduceInfectionRate()
    {
        return this.reduceInfectionRate;
    }

    // Được chưa trị 
    public void BeTreated()
    {

        if (peopleCtrl.peopleHealthInfo.GetBeTreated())
        {
            MainUISetting.Instance.notifyUI.FindAndShowNotify(NotifyName.hasTreated); // Hiện thông báo khi đang được chữa trị
            return;
        }
        if (peopleCtrl.peopleHealthInfo.VirusName == VirusName.noVirus)
        {
            MainUISetting.Instance.notifyUI.FindAndShowNotify(NotifyName.notInfected); // Hiện thông báo khi không bị nhiễm
            return;
        }

        int medicineIndex = (int) peopleCtrl.peopleHealthInfo.VirusName - 1; // Lấy index của virusname -1
        // Trả về khi sô lượng thuốc <= 0
        if (MainUISetting.Instance.inventoryUI.GetMedicineInfo((int)peopleCtrl.peopleHealthInfo.VirusName - 1).quantily <= 0) return;

        this.peopleCtrl.peopleHealthInfo.SetBeingTreated(true);

        MainUISetting.Instance.inventoryUI.AddMedicineQuantily(medicineIndex, -1); // Cập nhật số lượng thuốc

        MainUISetting.Instance.playerStatsUI.ReduceEnergyStat(10); // Giảm chỉ số năng lượng
    }

    // Được tiêm vaccine
    public void Vaccination(VaccineInfo vaccineInfo)
    {
        if ((int)this.peopleCtrl.peopleHealthInfo.NumberOfDoses == 2)
        {
            MainUISetting.Instance.notifyUI.FindAndShowNotify(NotifyName.maxNumOfDose); // Hiện thông báo khi tiêm đủ 2 mũi
            return;
        }
        if (this.peopleCtrl.peopleHealthInfo.VirusName != VirusName.noVirus)
        {
            MainUISetting.Instance.notifyUI.FindAndShowNotify(NotifyName.hasInfected); // Hiện thông báo khi đã bị nhiễm bệnh
            return;
        }
        if (this.Vaccine == VaccineName.noVaccine || this.Vaccine == vaccineInfo.vaccineName)
        {
            this.Vaccine = vaccineInfo.vaccineName;
            this.SetReduceInfectionRate(vaccineInfo.protectionRate);

            this.peopleCtrl.peopleHealthInfo.NumberOfDoses += 1; // Thêm số mũi tiêm 
            MainUISetting.Instance.inventoryUI.AddVaccineQuantily((int)vaccineInfo.vaccineName, -1); // Cập nhật số lượng vaccine

            MainUISetting.Instance.playerStatsUI.ReduceEnergyStat(10); // Giảm chỉ số năng lượng
        }
    }

}
