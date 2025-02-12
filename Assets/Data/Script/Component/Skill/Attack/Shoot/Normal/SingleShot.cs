using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShot : NormalShot
{
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

        Bullet bullet = newBullet.GetComponent<Bullet>();
        bullet.SetUser(this);
        newBullet.gameObject.SetActive(true);
        this.Finish();
    }
}
