using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShootSkill : Skill, IBullet
{
    //==========================================Variable==========================================
    [Header("Shoot")]
    [SerializeField] private InterfaceReference<IShootSkill> user1;
    [SerializeField] protected Bullet bullet;
    [SerializeField] protected Transform firePoint;
    [SerializeField] protected ShootMode shootMode;
    [SerializeField] protected bool isFinish;

    // ShootMode.CHARGE
    [SerializeField] protected bool isCharging;

    //==========================================Get Set===========================================
    public IShootSkill User1 { get => user1.Value; set => user1.Value = value; }

    //===========================================Unity============================================
    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadChildComponent(ref this.bullet, transform, "LoadBullet()");
        this.bullet.gameObject.SetActive(false);
        this.LoadComponent(ref this.firePoint, transform.Find("FirePoint"), "LoadFirePoint()");
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        this.Finishing();
    }

    //==========================================Override==========================================
    protected override void UsingSkill()
    {
        if (this.user1.Value.GetShootState(this) <= 0)
        {
            if (this.shootMode == ShootMode.AUTO) return;
            else if (this.shootMode == ShootMode.CHARGE && this.isCharging) return;
        }

        else if (this.user1.Value.GetShootState(this) != 1
            && this.shootMode == ShootMode.SEMI) return;

        base.UsingSkill();
    }

    protected override void UseSkill()
    {
        if (this.shootMode == ShootMode.CHARGE) this.isCharging = true;
    }

    //===========================================Method===========================================
    protected virtual void Finishing()
    {
        if (!this.isFinish) return;
        this.Finish();
    }

    protected virtual void Finish()
    {
        this.isFinish = false;
        this.skillCD.ResetStatus();
        this.user1.Value.ConsumePower(this);
        this.isCharging = false;
    }

    protected virtual void OnCharging()
    {
        if (this.shootMode != ShootMode.CHARGE) return;
        if (!this.isCharging) return;
        this.OnCharge();
    }

    protected abstract void OnCharge();

    //==========================================IBullet===========================================
    Transform IBullet.GetShooter(Bullet component)
    {
        if (this.bullet == component)
        {
            return transform;
        }

        Util.Instance.IComponentErrorLog(transform, component.transform);
        return null;
    }
    
    bool IBullet.CanMove(Bullet component)
    {
        return this.CanMove(component);
    }

    protected abstract bool CanMove(Bullet component);

    bool IBullet.CanStartCharge(Bullet component)
    {
        if (this.bullet == component)
        {
            return this.shootMode == ShootMode.CHARGE && this.isCharging;
        }

        Util.Instance.IComponentErrorLog(transform, component.transform);
        return false;
    }

    bool IBullet.CanFinishCharge(Bullet component)
    {
        if (this.bullet == component)
        {
            return this.shootMode == ShootMode.CHARGE && !this.isCharging;
        }

        Util.Instance.IComponentErrorLog(transform, component.transform);
        return false;
    }
}
