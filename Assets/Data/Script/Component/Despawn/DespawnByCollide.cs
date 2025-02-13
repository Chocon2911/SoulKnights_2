using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnByCollide : Despawner
{
    //==========================================Variable==========================================
    [Header("By Collide")]
    [SerializeField] private InterfaceReference<IDespawnByCollide> user1;
    [SerializeField] protected List<LayerMask> layerMasks;
    [SerializeField] protected List<Transform> collidedObjs;
    [SerializeField] protected int maxCollide;
    [SerializeField] protected int currCollide;

    //==========================================Get Set===========================================
    public IDespawnByCollide User1 { get => user1.Value; set => user1.Value = value; }
    public int MaxCollide { get => maxCollide; set => maxCollide = value; }
    public int CurrCollide { get => currCollide; set => currCollide = value; }

    //===========================================Unity============================================
    protected virtual void FixedUpdate()
    {
        this.Despawning();
    }

    //===========================================Method===========================================
    protected override void Despawn()
    {
        CapsuleCollider2D col = user1.Value.GetCollider(this);
        Vector2 mainObjPos = col.transform.position;
        Vector2 size = col.size * col.transform.localScale;
        float angle = col.transform.eulerAngles.z;
        Collider2D[] hit = Physics2D.OverlapCapsuleAll(mainObjPos, size, col.direction, angle);

        foreach (Collider2D obj in hit)
        {
            foreach(LayerMask mask in this.layerMasks)
            {
                if (obj.gameObject.layer != mask.value) continue;
                if (this.collidedObjs.Contains(obj.transform)) continue;
                this.collidedObjs.Add(obj.transform);
                this.currCollide += 1;
                break;
            }

            if (this.currCollide < this.maxCollide) continue;
            this.DespawnObj();
            return;
        }
    }
}
