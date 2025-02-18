using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnByTime : Despawner
{
    //==========================================Variable==========================================
    [Header("By Time")]
    [SerializeField] protected Cooldown despawnCD;

    //==========================================Get Set===========================================
    public Cooldown DespawnCD { get => despawnCD; set => despawnCD = value; }

    //===========================================Unity============================================
    protected virtual void FixedUpdate()
    {
        this.Despawning();
    }

    protected override void OnEnable()
    {
        this.despawnCD.ResetStatus();
    }

    //==========================================Override==========================================
    protected override void Despawn()
    {
        this.despawnCD.CoolingDown();

        if (!this.despawnCD.IsReady) return;
        this.DespawnObj();
    }
}
