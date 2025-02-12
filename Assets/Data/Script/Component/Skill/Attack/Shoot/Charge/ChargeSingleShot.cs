using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeSingleShot : ChargeShot
{
    //==========================================Variable==========================================
    [Header("Single Shot")]
    [SerializeField] protected Bullet newBullet;

    //==========================================Override==========================================
    protected override void UseSkill()
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

        this.newBullet = newBullet.GetComponent<Bullet>();
        this.newBullet.SetUser(this);
        newBullet.gameObject.SetActive(true);
        base.UseSkill();
    }

    protected override void OnCharge()
    {
        Vector2 newPos = this.firePoint.position;
        Quaternion newRot = this.firePoint.rotation;
        this.newBullet.transform.SetPositionAndRotation(newPos, newRot);
    }
}
