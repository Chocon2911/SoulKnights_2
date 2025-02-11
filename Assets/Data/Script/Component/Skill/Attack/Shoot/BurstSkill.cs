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
    [SerializeField] protected bool isBursting;
    [SerializeField] protected List<Transform> newBullets;

    //===========================================Unity============================================
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        this.Bursting();
    }

    //==========================================Override==========================================
    protected override void UsingSkill()
    {
        if (this.isBursting) return;
        base.UsingSkill();

        if (this.shootMode != ShootMode.CHARGE) return;
        for (int i = 0; i < this.burstCount; i++) this.Shoot();
    }

    protected override void UseSkill()
    {
        this.isBursting = true;
        base.UseSkill();
    }

    protected override void OnCharge()
    {
        foreach (Transform newBullet in this.newBullets)
        {
            newBullet.SetPositionAndRotation(this.firePoint.position, this.firePoint.rotation);
        }
    }

    protected override void Finish()
    {
        base.Finish();
        this.isBursting = false;
        this.tempBurstCount = 1;
        this.burstCD.ResetStatus();
    }

    protected override bool CanMove(Bullet component)
    {
        for (int i = 0; i < this.newBullets.Count; i++)
        {
            if (this.newBullets[i] != component.transform) continue;
            if (this.shootMode != ShootMode.CHARGE) return true;

            else
            {
                if (this.isCharging) return false;
                if (!this.isFinish) return false;

                if (i + 1 <= this.newBullets.Count - 1) return true;
                else return false;
            }
        }

        Util.Instance.IComponentErrorLog(transform, component.transform);
        return false;
    }

    //===========================================Method===========================================
    protected virtual void Bursting()
    {
        if (!this.isBursting) return;

        if (this.shootMode != ShootMode.CHARGE) this.NormalBurst();
        else this.ChargeBurst();
    }

    protected virtual void NormalBurst()
    {
        this.burstCD.CoolingDown();

        if (!this.burstCD.IsReady) return;
        this.Shoot();
        this.tempBurstCount++;

        if (this.tempBurstCount < this.burstCount) return;
        this.isFinish = true;
    }

    protected virtual void ChargeBurst()
    {
        if (!this.isBursting || this.isCharging || this.isFinish) return;
        this.burstCD.CoolingDown();

        if (!this.burstCD.IsReady) return;
        this.tempBurstCount++;
    }

    protected virtual void Shoot()
    {
        Transform bulletObj = this.bullet.transform;
        Vector3 spawnPos = this.firePoint.position;
        Quaternion spawnRot = this.firePoint.rotation;
        Transform newBullet = BulletSpawner.Instance.SpawnByObj(bulletObj, spawnPos, spawnRot);

        if (newBullet == null)
        {
            Debug.LogError("New Bullet is null", transform.gameObject);
            return;
        }

        this.newBullets.Add(newBullet);
        Bullet bullet = newBullet.GetComponent<Bullet>();
        bullet.SetUser(this);
        newBullet.gameObject.SetActive(true);
    }
}
