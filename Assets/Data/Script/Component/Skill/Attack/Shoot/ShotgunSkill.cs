using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunSkill : ShootSkill
{
    //==========================================Variable==========================================
    [Header("Shotgun")]
    [SerializeField] private int bulletCount;
    [SerializeField] private float spreadAngle;

    //===========================================Method===========================================
    protected override void UseSkill()
    {
        Vector2 spawnPos = this.firePoint.position;
        float lowestBulletAngle = this.firePoint.eulerAngles.z - this.spreadAngle / 2;
        List<Transform> bullets = new List<Transform>();

        for (int i = 0; i < this.bulletCount; i++)
        {
            float angle = lowestBulletAngle + i * this.spreadAngle / (this.bulletCount - 1);
            Quaternion spawnRot = Quaternion.Euler(0, 0, angle);
            Transform newBullet = BulletSpawner.Instance.SpawnByObj(this.bulletObj, spawnPos, spawnRot);

            if (newBullet == null)
            {
                Debug.LogError("New Bullet is null", transform.gameObject);
                return;
            }

            bullets.Add(newBullet);
        }

        foreach (Transform newBullet in bullets)
        {
            Bullet bullet = newBullet.GetComponent<Bullet>();
            bullet.SetUser(this);
            newBullet.gameObject.SetActive(true);
        }

        this.skillCD.ResetStatus();
        base.UseSkill();
    }
}
