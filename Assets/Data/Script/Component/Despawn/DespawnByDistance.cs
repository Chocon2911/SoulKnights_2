using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnByDistance : Despawner
{
    //==========================================Variable==========================================
    [Header("By Distance")]
    [SerializeField] private InterfaceReference<IDespawnByDistance> user1;
    [SerializeField] protected float currDistance;
    [SerializeField] protected float despawnDistance;

    //==========================================Get Set===========================================
    public IDespawnByDistance User1 { get => user1.Value; set => user1.Value = value; }
    public float CurrDistance { get => currDistance; set => currDistance = value; }
    public float DespawnDistance { get => despawnDistance; set => despawnDistance = value; }

    //===========================================Unity============================================
    protected virtual void FixedUpdate()
    {
        this.Despawn();
    }

    //==========================================Override==========================================
    protected override void Despawn()
    {
        this.GetCurrDistance();
        
        if (this.currDistance < this.despawnDistance) return;
        base.Despawn();
    }

    protected virtual void GetCurrDistance()
    {
        Vector2 despawnObjPos = this.user1.Value.GetDespawnObj(this).position;
        Vector2 targetPos = this.user1.Value.GetTarget(this).position;

        float xDistance = Mathf.Abs(targetPos.x - despawnObjPos.x);
        float yDistance = Mathf.Abs(targetPos.y - despawnObjPos.y);

        this.currDistance = Mathf.Sqrt(Mathf.Pow(xDistance, 2) + Mathf.Pow(yDistance, 2));
    }
}
