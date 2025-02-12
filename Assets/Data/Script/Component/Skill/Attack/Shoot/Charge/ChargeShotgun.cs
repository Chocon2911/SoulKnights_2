using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeShotgun : ChargeShot
{
    //==========================================Variable==========================================
    [Header("Shotgun")]
    [SerializeField] private int bulletCount;
    [SerializeField] private float spreadAngle;
    [SerializeField] protected List<Bullet> newBullets;

    //==========================================Override==========================================
    protected override void UseSkill()
    {
        Transform bulletObj = this.bullet.transform;
        Vector2 spawnPos = this.firePoint.position;
        float lowestBulletAngle = this.firePoint.eulerAngles.z - this.spreadAngle / 2;
        List<Transform> newBullets = new List<Transform>();

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

            newBullets.Add(newBullet);
        }

        foreach (Transform newBullet in newBullets)
        {
            Bullet bullet = newBullet.GetComponent<Bullet>();
            bullet.SetUser(this);
            this.newBullets.Add(bullet);
            newBullet.gameObject.SetActive(true);
        }

        base.UseSkill();
    }
    
    protected override void OnCharge()
    {
        Vector2 spawnPos = this.firePoint.position;
        float lowestBulletAngle = this.firePoint.eulerAngles.z - this.spreadAngle / 2;

        for (int i = 0; i < this.bulletCount; i++)
        {
            float angle = lowestBulletAngle + i * this.spreadAngle / (this.bulletCount - 1);
            Quaternion newRot = Quaternion.Euler(0, 0, angle);
            this.newBullets[i].transform.SetPositionAndRotation(spawnPos, newRot);
        }
    }

    protected override void Finish()
    {
        base.Finish();
        this.newBullets.Clear();
    }
}
