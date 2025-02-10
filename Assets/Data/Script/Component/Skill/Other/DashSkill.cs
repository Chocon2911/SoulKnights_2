using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashSkill : Skill, IMovement
{
    //==========================================Variable==========================================
    [Header("Dash")]
    [SerializeField] private InterfaceReference<IDashSkill> user1;
    [SerializeField] protected Movement movement;
    [SerializeField] protected Cooldown dashCD;
    [SerializeField] protected bool canDash;

    //==========================================Get Set===========================================
    public IDashSkill User1 { get => this.user1.Value; set => this.user1.Value = value; }
    public Movement Movement { get => this.movement; set => this.movement = value; }
    public Cooldown DashCD { get => this.dashCD; set => this.dashCD = value; }
    public bool CanDash { get => this.canDash; set => this.canDash = value; }



    //===========================================Unity============================================
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        this.Dashing();
    }

    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadComponent(ref this.movement, transform.Find("Movement"), "LoadMovement()");
        this.movement.User = this;
    }



    //============================================================================================
    //===========================================Method===========================================
    //============================================================================================

    //==========================================Override==========================================
    protected override void UsingSkill()
    {
        if (!this.user1.Value.CanUseSkill(this)) return;
        base.UsingSkill();
    }

    protected override void UseSkill()
    {
        base.UseSkill();
        IDashSkill tempUser = this.user1.Value;
        this.canDash = true;
        this.movement.MoveDir = tempUser.GetDashDir(this);
    }

    //============================================Dash============================================
    protected virtual void Dashing()
    {
        if (!this.canDash) return;
        this.Dash();

        if (!this.dashCD.IsReady) return;
        this.FinishDash();
    }

    protected virtual void Dash()
    {
        this.dashCD.CoolingDown();
    }

    protected virtual void FinishDash()
    {
        this.dashCD.ResetStatus();
        this.skillCD.ResetStatus();
        this.canDash = false;
    }

    //=========================================IMovement==========================================
    bool IMovement.CanMove(Movement component)
    {
        if (this.movement == component)
        {
            if (this.canDash) return true;
            else return false;
        }

        Debug.LogError("component not found", transform.gameObject);
        Debug.LogError("wrong component source", component.gameObject);
        return false;
    }

    Rigidbody2D IMovement.GetRb(Movement component)
    {
        if (this.movement == component)
        {
            IDashSkill tempUser = this.user1.Value;
            return tempUser.GetRb(this);
        }

        Debug.LogError("component not found", transform.gameObject);
        Debug.LogError("wrong component source", component.gameObject);
        return null;
    }
}
