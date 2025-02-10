using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
public class Bullet : Entity, IMovement, IDamageSender, IDespawner, IDespawnByDistance, IMoveForward,
    IDespawnByCollide
{
    //==========================================Variable==========================================
    [Header("=====Bullet=====")]
    [Header("Stat")]
    [SerializeField] protected InterfaceReference<IBullet> user;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected CapsuleCollider2D col;

    [Header("Component")]
    [SerializeField] protected Movement movement;
    [SerializeField] protected DamageSender damageSender;
    [SerializeField] protected List<Despawner> despawners;

    public void SetUser(IBullet user)
    {
        this.user.Value = user;
    }

    //===========================================Unity============================================
    public override void LoadComponents()
    {
        base.LoadComponents();
        // Stat
        this.LoadComponent(ref this.rb, transform, "LoadRb()");
        this.LoadComponent(ref this.col, transform, "LoadCol()");
        
        // Component
        this.LoadComponent(ref this.movement, transform.Find("Movement"), "LoadMovement()");
        this.movement.User = this;
        this.LoadComponent(ref this.damageSender, transform.Find("DamageSender"), "LoadDamageSender()");
        this.damageSender.User = this;
        this.LoadComponent(ref this.despawners, transform.Find("Despawn"), "LoadDespawners()");
        foreach (Despawner despawner in this.despawners) despawner.User = this;

        // MoveForward
        if (this.movement is MoveForward moveForward) moveForward.User1 = this;

        // DespawnByDistance
        foreach (Despawner despawner in this.despawners)
        {
            if (despawner is DespawnByDistance despawnByDistance) despawnByDistance.User1 = this;
        }

        // DespawnByCollide
        foreach (Despawner despawner in this.despawners)
        {
            if (despawner is DespawnByCollide despawnByCollide) despawnByCollide.User1 = this;
        }
    }

    //=========================================IMovement==========================================
    bool IMovement.CanMove(Movement component)
    {
        if (this.movement == component)
        {
            return this.user.Value.CanMove(this);
        }

        Util.Instance.IComponentErrorLog(transform, component.transform);
        return false;
    }

    Rigidbody2D IMovement.GetRb(Movement component)
    {
        if (this.movement == component)
        {
            return this.rb;
        }

        Util.Instance.IComponentErrorLog(transform, component.transform);
        return null;
    }

    //======================================IDamageReceiver=======================================
    void IDamageSender.OnSending(DamageSender component)
    {
        if (this.damageSender == component)
        {
            BulletSpawner.Instance.Despawn(transform);
        }

        Util.Instance.IComponentErrorLog(transform, component.transform);
        return;
    }

    //=========================================IDespawner=========================================
    bool IDespawner.CanDespawn(Despawner component)
    {
        foreach (Despawner despawner in this.despawners)
        {
            if (despawner != component) continue;
            return true;
        }

        Util.Instance.IComponentErrorLog(transform, component.transform);
        return false;
    }

    Spawner IDespawner.GetSpawner(Despawner component)
    {
        foreach (Despawner despawner in this.despawners)
        {
            if (despawner != component) continue;
            return BulletSpawner.Instance;
        }

        Util.Instance.IComponentErrorLog(transform, component.transform);
        return null;
    }

    Transform IDespawner.GetDespawnObj(Despawner component)
    {
        foreach (Despawner despawner in this.despawners)
        {
            if (despawner != component) continue;
            return transform;
        }

        Util.Instance.IComponentErrorLog(transform, component.transform);
        return null;
    }

    //=====================================IDespawnByDistance=====================================
    public Transform GetTarget(DespawnByDistance component)
    {
        foreach (Despawner despawner in this.despawners)
        {
            if (despawner != component) continue;
            return this.user.Value.GetShooter(this);
        }

        Util.Instance.IComponentErrorLog(transform, component.transform);
        return null;
    }

    //========================================IMoveForward========================================
    float IMoveForward.GetAngle(MoveForward component)
    {
        if (this.movement == component)
        {
            return transform.eulerAngles.z;
        }

        Util.Instance.IComponentErrorLog(transform, component.transform);
        return -1;
    }

    //=====================================IDespawnByCollide======================================
    CapsuleCollider2D IDespawnByCollide.GetCollider(DespawnByCollide component)
    {
        foreach (Despawner despawner in this.despawners)
        {
            if (despawner != component) continue;
            return this.col;
        }

        Util.Instance.IComponentErrorLog(transform, component.transform);
        return null;
    }
}
