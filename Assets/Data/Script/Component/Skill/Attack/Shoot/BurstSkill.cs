using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstSkill : ShootSkill
{
    //==========================================Variable==========================================
    [Header("Burst")]
    [SerializeField] protected int tempBurstCount;
    [SerializeField] protected int burstCount;
    [SerializeField] protected Cooldown burstCD;
    [SerializeField] protected bool canBurst;

    //===========================================Method===========================================
    protected override void UseSkill()
    {
        this.canBurst = true;
        base.UseSkill();
    }

    //===========================================Unity============================================
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        this.Bursting();
    }

    //===========================================Burst============================================
    protected virtual void Bursting()
    {
        if (!this.canBurst) return;
        this.Burst();
    }

    protected virtual void Burst()
    {
        this.burstCD.CoolingDown();

        if (!this.burstCD.IsReady) return;
        this.Shoot();
        this.tempBurstCount++;

        if (this.tempBurstCount < this.burstCount) return; 
        this.canBurst = false;
        this.tempBurstCount = 0;
        this.burstCD.ResetStatus();
    }

    protected virtual void Shoot()
    {
        Vector3 spawnPos = this.bulletObj.position;
        Quaternion spawnRot = this.bulletObj.rotation;
        Transform newBullet = BulletSpawner.Instance.SpawnByObj(this.bulletObj, spawnPos, spawnRot);

        if (newBullet == null)
        {
            Debug.LogError("New Bullet is null", transform.gameObject);
            return;
        }

        Bullet bullet = newBullet.GetComponent<Bullet>();
        bullet.SetUser(this);
        newBullet.gameObject.SetActive(true);
    }
}
