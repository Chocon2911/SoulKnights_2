using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRegen
{
    bool CanRegen(Regen component);
    void RestoreValue(Regen component);
}


public class Regen : HuyMonoBehaviour
{
    //==========================================Variable==========================================
    [Header("Regen")]
    [SerializeField] private InterfaceReference<IRegen> user;
    [SerializeField] protected Cooldown regenCD;
    [SerializeField] protected float regenValue;

    //==========================================Get Set===========================================
    public IRegen User { get => user.Value; set => user.Value = value; }
    public Cooldown RegenCD { get => regenCD; set => regenCD = value; }
    public float RegenValue { get => regenValue; set => regenValue = value; }

    //===========================================Unity============================================
    protected virtual void FixedUpdate()
    {
        this.Regening();
    }

    //===========================================Method===========================================
    protected virtual void Regening()
    {
        if (!this.user.Value.CanRegen(this)) return;
        this.RechargeRegen();

        if (!this.regenCD.IsReady) return;
        this.Restore();
    }

    protected virtual void RechargeRegen()
    {
        this.regenCD.CoolingDown();
    }

    protected virtual void Restore()
    {
        this.user.Value.RestoreValue(this);
        this.regenCD.ResetStatus();
    }
}
