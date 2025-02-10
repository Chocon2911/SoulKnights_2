using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : HuyMonoBehaviour
{
    //==========================================Variable==========================================
    [Header("Skill")]
    [SerializeField] private InterfaceReference<ISkill> user;
    [SerializeField] protected int healthCost;
    [SerializeField] protected int manaCost;
    [SerializeField] protected Cooldown skillCD;

    //==========================================Get Set===========================================
    public ISkill User { get => this.user.Value; set => this.user.Value = value; }
    public int HealthCost => this.healthCost;
    public int ManaCost => this.manaCost;
    public Cooldown SkillCD => this.skillCD;

    //===========================================Unity============================================
    protected virtual void Update()
    {
        this.UsingSkill();
    }

    protected virtual void FixedUpdate()
    {
        this.RechargingSkill();
    }

    //==========================================Recharge==========================================
    protected virtual void RechargingSkill()
    {
        if (this.skillCD.IsReady) return;
        if (!this.user.Value.CanRechargeSkill(this)) return;
        this.RechargeSkill();
    }

    protected virtual void RechargeSkill()
    {
        this.skillCD.CoolingDown();
    }

    //============================================Use=============================================
    protected virtual void UsingSkill()
    {
        if (!this.skillCD.IsReady) return;
        this.UseSkill();
    }

    protected virtual void UseSkill()
    {
        this.user.Value.ConsumePower(this);
    }
}
