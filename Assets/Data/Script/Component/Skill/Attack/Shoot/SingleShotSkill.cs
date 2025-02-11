using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShotSkill : ShootSkill
{
    [Header("Single Shot")]
    [SerializeField] protected Transform newBullet;

    //==========================================Override==========================================
    protected override void UseSkill()
    {
        Transform bulletObj = this.bullet.transform;
        Vector3 spawnPos = this.firePoint.position;
        Quaternion spawnRot = this.firePoint.rotation;
        this.newBullet = BulletSpawner.Instance.SpawnByObj(bulletObj, spawnPos, spawnRot);

        if (this.newBullet == null)
        {
            Debug.LogError("New Bullet is null", transform.gameObject);
            return;
        }

        Bullet bullet = this.newBullet.GetComponent<Bullet>();
        bullet.SetUser(this);
        this.newBullet.gameObject.SetActive(true);
        base.UseSkill();

        if (this.shootMode == ShootMode.CHARGE) return;
        this.isFinish = true;
    }

    protected override void OnCharge()
    {
        this.newBullet.SetPositionAndRotation(this.firePoint.position, this.firePoint.rotation);
    }

    protected override bool CanMove(Bullet component)
    {
        if (this.newBullet == component.transform)
        {
            return this.isFinish;
        }

        Util.Instance.IComponentErrorLog(transform, component.transform);
        return false;
    }
}
