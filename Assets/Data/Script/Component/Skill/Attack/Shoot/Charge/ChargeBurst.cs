using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeBurst : ChargeShot
{
    //==========================================Variable==========================================
    [Header("Burst")]
    [SerializeField] protected int tempBurstCount;
    [SerializeField] protected int burstCount;
    [SerializeField] protected Cooldown burstCD;
    [SerializeField] protected bool isBursting;
    [SerializeField] protected List<Bullet> newBullets;

    protected override void FixedUpdate()
    {
        this.FinishingBurst();
        base.FixedUpdate();
        this.Bursting();
    }

    //==========================================Override==========================================
    protected override void UseSkill()
    {
        Transform bulletObj = this.bullet.transform;
        Vector3 spawnPos = this.bullet.transform.position;
        Quaternion spawnRot = this.bullet.transform.rotation;

        for (int i = 0; i < this.tempBurstCount; i++)
        {
            Transform newBullet = BulletSpawner.Instance.SpawnByObj(bulletObj, spawnPos, spawnRot);

            if (newBullet == null)
            {
                Debug.LogError("New Bullet is null", transform.gameObject);
                return;
            }

            Bullet bullet = newBullet.GetComponent<Bullet>();
            bullet.SetUser(this);
            this.newBullets.Add(bullet);
            newBullet.gameObject.SetActive(true);
        }

        base.UseSkill();
    }

    protected override void OnCharge()
    {
        Vector2 newPos = this.firePoint.position;
        Quaternion newRot = this.firePoint.rotation;

        foreach (Bullet newBullet in this.newBullets)
        {
            newBullet.transform.SetPositionAndRotation(newPos, newRot);
        }
    }

    protected override bool CanMove(Bullet component)
    {
        if (this.isCharging) return false;
        for (int i = 0; i < this.newBullets.Count; i++)
        {
            if (this.newBullets[i] != component) continue;
            if (i + 1 <= this.tempBurstCount) return true;
            return false;
        }

        return false;
    }

    //===========================================Burst============================================
    protected virtual void Bursting()
    {
        if (!this.isBursting) return;
        this.Burst();
    }

    protected virtual void Burst()
    {
        this.burstCD.CoolingDown();

        if (!this.burstCD.IsReady) return;
        this.tempBurstCount++;
        this.burstCD.ResetStatus();

        if (this.tempBurstCount < this.burstCount) return;
        this.isBursting = false;
    }

    //===========================================Finish===========================================
    protected virtual void FinishingBurst()
    {
        if (this.isCharging) return;
        if (this.isBursting) return;
        this.FinishBurst();
    }
    
    protected virtual void FinishBurst()
    {
        this.tempBurstCount = 1;
        this.newBullets.Clear();
    }
}
