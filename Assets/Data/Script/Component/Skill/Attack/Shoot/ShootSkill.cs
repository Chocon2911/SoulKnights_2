using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSkill : Skill, IBullet
{
    //==========================================Variable==========================================
    [Header("Shoot")]
    [SerializeField] private InterfaceReference<IShootSkill> user1;
    [SerializeField] protected Transform bulletObj;
    [SerializeField] protected Transform firePoint;
    [SerializeField] protected bool canAutoFire;

    //==========================================Get Set===========================================
    public IShootSkill User1 { get => user1.Value; set => user1.Value = value; }

    //==========================================Override==========================================
    protected override void UsingSkill()
    {
        if (this.user1.Value.GetShootState(this) <= 0 && this.canAutoFire) return;
        else if (this.user1.Value.GetShootState(this) != 1 && !this.canAutoFire) return;
        base.UsingSkill();
    }

    //===========================================Unity============================================
    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadComponent(ref this.bulletObj, transform.Find("Bullet"), "LoadBullet()");
        this.bulletObj.gameObject.SetActive(false);
        this.LoadComponent(ref this.firePoint, transform.Find("FirePoint"), "LoadFirePoint()");
    }

    //==========================================IBullet===========================================
    Transform IBullet.GetShooter(Bullet component)
    {
        return transform;
    }
    
    bool IBullet.CanMove(Bullet component)
    {
        return true;
    }
}
