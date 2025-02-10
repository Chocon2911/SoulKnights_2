using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeSkill : Skill
{
    //==========================================Variable==========================================
    [Header("Charge")]
    [SerializeField] private InterfaceReference<IChargeSkill> user1;
    [SerializeField] protected List<Cooldown> chargeCDs;
    [SerializeField] protected int chargeState;
    [SerializeField] protected bool canCharge;

    //==========================================Get Set===========================================
    public IChargeSkill User1 { get => user1.Value; set => user1.Value = value; }
    public List<Cooldown> ChargeCDs { get => chargeCDs; set => chargeCDs = value; }
    public int ChargeState { get => chargeState; set => chargeState = value; }
    public bool CanCharge { get => canCharge; set => canCharge = value; }

    //===========================================Unity============================================
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        this.CheckingState();
    }

    //==========================================Override==========================================
    protected override void UsingSkill()
    {
        if (this.canCharge) return;
        base.UsingSkill();
    }

    protected override void UseSkill()
    {
        this.canCharge = true;
    }

    //===========================================Charge===========================================
    protected virtual void Charging()
    {
        if (!this.canCharge) return;
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
        this.skillCD.ResetStatus();
        foreach (Cooldown cd in this.chargeCDs) cd.ResetStatus();
        this.chargeState = 0;
        this.user1.Value.ConsumePower(this);
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
