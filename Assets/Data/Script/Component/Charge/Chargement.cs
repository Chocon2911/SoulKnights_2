using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IChargement
{
    bool CanStart(Chargement component);
    bool CanFinishCharge(Chargement component);
}

public class Chargement : HuyMonoBehaviour
{
    //==========================================Variable==========================================
    [Header("Charge")]
    [SerializeField] private InterfaceReference<IChargement> user;
    [SerializeField] protected List<Cooldown> chargeCDs;
    [SerializeField] protected int chargeState;
    [SerializeField] protected bool isCharging;
    [SerializeField] protected bool isFinish;
    [SerializeField] protected bool isFullyCharge;

    //==========================================Get Set===========================================
    public IChargement User { set => user.Value = value; }
    public List<Cooldown> ChargeCDs { get => chargeCDs; }
    public int ChargeState { get => chargeState; }
    public bool IsCharging { get => isCharging; }
    public bool IsFinish { get => isFinish; }
    public bool IsFullyCharge { get => isFullyCharge; }

    //===========================================Unity============================================
    protected virtual void Update()
    {
        this.IncreasingState();
        this.FinishingSkill();
        this.CheckingState();
    }
    
    protected virtual void FixedUpdate()
    {
        this.Charging();
        this.StartingCharge();
    }

    protected override void OnEnable()
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

    //=======================================Increase State=======================================
    protected virtual void IncreasingState()
    {
        if (this.chargeState >= this.chargeCDs.Count) return;
        if (!this.chargeCDs[this.chargeState - 1].IsReady) return;
        this.IncreaseState();
    }

    protected virtual void IncreaseState()
    {
        this.chargeState++;
    }

    //========================================Check State=========================================
    protected virtual void CheckingState()
    {
        if (this.chargeState < this.chargeCDs.Count)
        {
            this.isFullyCharge = false;
            return;
        }

        this.isFullyCharge = true;
    }
}