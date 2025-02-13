using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnByHealth : Despawner
{
    //==========================================Variable==========================================
    [Header("By Health")]
    [SerializeField] protected InterfaceReference<IDespawnByHealth> user1;

    //==========================================Get Set===========================================
    public IDespawnByHealth User1 { set => user1.Value = value; }

    //===========================================Unity============================================
    protected virtual void Update()
    {
        this.Despawning();
    }

    //===========================================Method===========================================
    protected override void Despawning()
    {
        if (this.user1.Value.GetCurrHealth(this) > 0) return;
        base.Despawning();
    }
}
