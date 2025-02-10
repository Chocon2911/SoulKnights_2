using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chargement : HuyMonoBehaviour
{
    //==========================================Variable==========================================
    [Header("Charge")]
    [SerializeField] private InterfaceReference<IChargement> user1;
    [SerializeField] protected List<Cooldown> chargeCDs;
    [SerializeField] protected int chargeState;
    [SerializeField] protected bool isCharging;

    //==========================================Get Set===========================================
    public IChargement User1 { get => user1.Value; set => user1.Value = value; }
    public List<Cooldown> ChargeCDs { get => chargeCDs; set => chargeCDs = value; }
    public int ChargeState { get => chargeState; set => chargeState = value; }
    public bool IsCharging { get => isCharging; set => isCharging = value; }

    //===========================================Unity============================================
    protected virtual void FixedUpdate()
    {
        this.CheckingState();
    }

    //============================================Use=============================================
    protected virtual void StartingCharge()
    {
        if (!this.user1.Value.CanStart(this)) return;
        this.StartCharge();
    }

    protected virtual void StartCharge()
    {
        this.isCharging = true;
    }

    //===========================================Charge===========================================
    protected virtual void Charging()
    {
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
        if (!this.user1.Value.CanFinishSkill(this)) return;
        this.FinishSkill();
    }

    protected virtual void FinishSkill()
    {
        foreach (Cooldown cd in this.chargeCDs) cd.ResetStatus();
        this.chargeState = 0;
    }

    //===========================================State============================================
    protected virtual void CheckingState()
    {
        if (!this.chargeCDs[this.chargeState - 1].IsReady) return;
        this.IncreaseState();
    }

    protected virtual void IncreaseState()
    {
        this.chargeState++;
    }
}
