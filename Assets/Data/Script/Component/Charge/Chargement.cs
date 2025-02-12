using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chargement : HuyMonoBehaviour
{
    //==========================================Variable==========================================
    [Header("Charge")]
    [SerializeField] private InterfaceReference<IChargement> user;
    [SerializeField] protected List<Cooldown> chargeCDs;
    [SerializeField] protected int chargeState;
    [SerializeField] protected bool isCharging;
    [SerializeField] protected bool isFinish;

    //==========================================Get Set===========================================
    public IChargement User { get => user.Value; set => user.Value = value; }
    public List<Cooldown> ChargeCDs { get => chargeCDs; set => chargeCDs = value; }
    public int ChargeState { get => chargeState; set => chargeState = value; }
    public bool IsCharging { get => isCharging; set => isCharging = value; }

    //===========================================Unity============================================
    protected virtual void FixedUpdate()
    {
        this.Charging();
        this.CheckingState();
        this.FinishingSkill();
        this.StartingCharge();
    }

    protected virtual void OnEnable()
    {
        this.isFinish = false;
    }

    //============================================Use=============================================
    protected virtual void StartingCharge()
    {
        if (this.isFinish) return;
        if (!this.user.Value.CanStart(this)) return;
        this.StartCharge();
    }

    protected virtual void StartCharge()
    {
        this.isCharging = true;
    }

    //===========================================Charge===========================================
    protected virtual void Charging()
    {
        if (this.isFinish) return;
        if (this.chargeCDs[this.chargeState - 1].IsReady) return;
        if (!this.isCharging) return;
        this.Charge();
    }

    protected virtual void Charge()
    {
        this.chargeCDs[this.chargeState - 1].CoolingDown();
    }

    //===========================================Finish===========================================
    protected virtual void FinishingSkill()
    {
        if (this.isFinish) return;
        if (!this.user.Value.CanFinishCharge(this)) return;
        this.FinishSkill();
    }

    protected virtual void FinishSkill()
    {
        foreach (Cooldown cd in this.chargeCDs) cd.ResetStatus();
        this.chargeState = 1;
        this.isCharging = false;
        this.isFinish = true;
    }

    //===========================================State============================================
    protected virtual void CheckingState()
    {
        if (this.chargeState >= this.chargeCDs.Count) return;
        if (!this.chargeCDs[this.chargeState - 1].IsReady) return;
        this.IncreaseState();
    }

    protected virtual void IncreaseState()
    {
        this.chargeState++;
    }
}
