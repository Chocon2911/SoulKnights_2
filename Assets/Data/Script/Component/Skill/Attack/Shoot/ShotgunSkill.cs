using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunSkill : ShootSkill
{
    //==========================================Variable==========================================
    [Header("Shotgun")]
    [SerializeField] private int bulletCount;
    [SerializeField] private float spreadAngle;
    [SerializeField] private List<Transform> newBullets;

    //==========================================Override==========================================
    protected override void UseSkill()
    {
        Transform bulletObj = this.bullet.transform;
        Vector2 spawnPos = this.firePoint.position;
        float lowestBulletAngle = this.firePoint.eulerAngles.z - this.spreadAngle / 2;

        for (int i = 0; i < this.bulletCount; i++)
        {
            float angle = lowestBulletAngle + i * this.spreadAngle / (this.bulletCount - 1);
            Quaternion spawnRot = Quaternion.Euler(0, 0, angle);
            Transform newBullet = BulletSpawner.Instance.SpawnByObj(bulletObj, spawnPos, spawnRot);

            if (newBullet == null)
            {
                Debug.LogError("New Bullet is null", transform.gameObject);
                return;
            }

            this.newBullets.Add(newBullet);
        }

        foreach (Transform newBullet in this.newBullets)
        {
            Bullet bullet = newBullet.GetComponent<Bullet>();
            bullet.SetUser(this);
            newBullet.gameObject.SetActive(true);
        }
        base.UseSkill();

        if (this.shootMode == ShootMode.CHARGE) return;
        this.isFinish = true;
    }

    protected override void OnCharge()
    {
        Vector2 newPos = this.firePoint.position;
        float lowestBulletAngle = this.firePoint.eulerAngles.z - this.spreadAngle / 2;

        for (int i = 0; i < this.bulletCount; i++)
        {
            float angle = lowestBulletAngle + i * this.spreadAngle / (this.bulletCount - 1);
            Quaternion newRot = Quaternion.Euler(0, 0, angle);
            this.newBullets[i].SetPositionAndRotation(newPos, newRot);
        }
    }

    protected override bool CanMove(Bullet component)
    {
        foreach (Transform newBullet in this.newBullets)
        {
            if (newBullet != component.transform) continue;
            return this.isFinish;
        }

        Util.Instance.IComponentErrorLog(transform, component.transform);
        return false;
    }
}
