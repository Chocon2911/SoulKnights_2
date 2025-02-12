using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burst : NormalShot
{
    //==========================================Variable==========================================
    [Header("Burst")]
    [SerializeField] protected int tempBurstCount;
    [SerializeField] protected int burstCount;
    [SerializeField] protected Cooldown burstCD;
    [SerializeField] protected bool isBursting;

    //===========================================Unity============================================
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        this.Bursting();
    }

    //==========================================Override==========================================
    protected override void UseSkill()
    {
        this.isBursting = true;
    }

    protected override void Finish()
    {
        this.isBursting = false;
        this.tempBurstCount = 1;
    }

    //===========================================Burst============================================
    protected virtual void Bursting()
    {
        if (!this.isBursting) return;
        this.DoBurst();
    }

    protected virtual void DoBurst()
    {
        this.burstCD.CoolingDown();

        if (!this.burstCD.IsReady) return;
        this.CreateBullet();
        this.tempBurstCount++;
        this.burstCD.ResetStatus();

        if (this.tempBurstCount < this.burstCount) return;
        this.Finish();
    }

    protected virtual void CreateBullet()
    {
        Transform bulletObj = this.bullet.transform;
        Vector3 spawnPos = this.bullet.transform.position;
        Quaternion spawnRot = this.bullet.transform.rotation;
        Transform newBullet = BulletSpawner.Instance.SpawnByObj(bulletObj, spawnPos, spawnRot);

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
