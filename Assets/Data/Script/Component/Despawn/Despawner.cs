using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Despawner : HuyMonoBehaviour
{
    //==========================================Variable==========================================
    [Header("Despawner")]
    [SerializeField] private InterfaceReference<IDespawner> user;

    //==========================================Get Set===========================================
    public IDespawner User { get => user.Value; set => user.Value = value; }

    //===========================================Method===========================================
    protected virtual void Despawning()
    {
        if (!this.user.Value.CanDespawn(this)) return;
        this.Despawn();
    }

    protected virtual void Despawn()
    {
        this.DespawnObj();
    }

    protected virtual void DespawnObj()
    {
        Transform despawnObj = this.user.Value.GetDespawnObj(this);
        this.user.Value.GetSpawner(this).Despawn(despawnObj);
    }
}
