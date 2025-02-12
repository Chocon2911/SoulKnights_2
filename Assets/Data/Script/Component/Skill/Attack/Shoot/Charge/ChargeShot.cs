using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public abstract class ChargeShot : ShootSkill
{
    //==========================================Variable==========================================
    [Header("Charge")]
    [SerializeField] protected bool isCharging;

    //===========================================Unity============================================
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        this.Finishing();
        this.OnCharging();
    }

    //==========================================Override==========================================
    protected override void UsingSkill()
    {
        if (this.isCharging) return;
        if (this.user1.Value.GetShootState(this) <= 0) return;
        base.UsingSkill();
    }

    protected override void UseSkill()
    {
        this.isCharging = true;
    }

    protected override void Finish()
    {
        base.Finish();
        this.isCharging = false;
    }

    protected override bool CanMove(Bullet component)
    {
        return this.isCharging == false;
    }

    protected override bool CanCharge(Bullet component)
    {
        return this.isCharging;
    }

    protected override bool CanFinishCharge(Bullet component)
    {
        return this.isCharging == false;
    }

    //===========================================Method===========================================
    protected virtual void Finishing()
    {
        if (!this.isCharging) return;
        if (this.user1.Value.GetShootState(this) >= 1) return;
        this.Finish();
    }

    protected virtual void OnCharging()
    {
        if (!this.isCharging) return;
        this.OnCharge();
    }

    protected abstract void OnCharge();
}
