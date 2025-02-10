using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShotSkill : ShootSkill
{
    //===========================================Method===========================================
    protected override void UseSkill()
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
        this.skillCD.ResetStatus();
        base.UseSkill();
    }
}
