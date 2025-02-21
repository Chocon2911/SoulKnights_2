using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IDetectOnStay : IDetectByCollide
{
    bool CanRecharge(DetectOnStay component);
}

public class DetectOnStay : DetectByCollide
{
    //==========================================Variable==========================================
    [Header("On Stay")]
    [SerializeField] protected InterfaceReference<IDetectOnStay> user2;
    [SerializeField] protected Cooldown detectCD;

    //==========================================Get Set===========================================
    public IDetectOnStay User2 { set => user2.Value = value; }

    //===========================================Unity============================================
    public override void LoadComponents()
    {
        base.LoadComponents();
        this.detectCol.isTrigger = true;
    }

    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        this.Detecting(collision);
    }

    protected virtual void FixedUpdate()
    {
        this.Recharging();
    }

    protected virtual void LateUpdate()
    {
        this.ResettingCD();
    }

    //==========================================override==========================================
    protected override void Detecting(Collider2D col)
    {
        if (!this.detectCD.IsReady) return;
        base.Detecting(col);
    }

    //==========================================Recharge==========================================
    protected virtual void Recharging()
    {
        if (!this.user2.Value.CanRecharge(this)) return;
        this.Recharge();
    }

    protected virtual void Recharge()
    {
        this.detectCD.CoolingDown();
    }

    //===========================================Reset============================================
    protected virtual void ResettingCD()
    {
        if (!this.detectCD.IsReady) return;
        this.ResetCD();
    }
    protected virtual void ResetCD()
    {
        this.detectCD.ResetStatus();
    }
}
