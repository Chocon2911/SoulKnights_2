using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShootSkill : Skill, IBullet
{
    //==========================================Variable==========================================
    [Header("Shot")]
    [SerializeField] protected InterfaceReference<IShootSkill> user1;
    [SerializeField] protected Transform firePoint;
    [SerializeField] protected Bullet bullet;

    //==========================================Get Set===========================================
    public IShootSkill User1 { set => user1.Value = value; }

    //===========================================Unity============================================
    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadComponent(ref this.firePoint, transform.Find("FirePoint"), "LoadFirePoint()");
        this.LoadChildComponent(ref this.bullet, transform, "LoadBullet()");
        if (this.bullet != null) this.bullet.gameObject.SetActive(false);
    }

    //===========================================Finish===========================================
    protected virtual void Finish()
    {
        this.skillCD.ResetStatus();
    }

    //==========================================IBullet===========================================
    Transform IBullet.GetShooter(Bullet component)
    {
        return transform;
    }

    bool IBullet.CanMove(Bullet component)
    {
        return this.CanMove(component);
    }

    protected abstract bool CanMove(Bullet component);

    bool IBullet.CanStartCharge(Bullet component)
    {
        return this.CanCharge(component);
    }

    protected abstract bool CanCharge(Bullet component);

    bool IBullet.CanFinishCharge(Bullet component)
    {
        return this.CanFinishCharge(component);
    }

    protected abstract bool CanFinishCharge(Bullet component);
}
