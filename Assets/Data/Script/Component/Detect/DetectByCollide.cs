using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CircleCollider2D))]
public class DetectByCollide : Detector
{
    //==========================================Variable==========================================
    [Header("By Collide")]
    [SerializeField] protected InterfaceReference<IDetectByCollide> user1;
    [SerializeField] protected CircleCollider2D detectCol;
    [SerializeField] protected Transform tempTargetObj;

    //==========================================Get Set===========================================
    public IDetectByCollide User1 { set => user1.Value = value; }

    //===========================================Unity============================================
    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadComponent(ref this.detectCol, transform, "LoadDetectCollider()");
        this.detectCol.isTrigger = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (this.tempTargetObj == null) this.tempTargetObj = collision.transform;
        else this.GetClosestTarget(this.tempTargetObj, collision.transform);
        this.Detecting();
    }

    //==========================================Override==========================================
    protected override void Detect()
    {
        foreach (string tag in this.tags)
        {
            if (!this.tempTargetObj.gameObject.CompareTag(tag)) continue;
            this.target = tempTargetObj.transform;
        }

        this.tempTargetObj = null;
    }

    //===========================================Method===========================================
    protected virtual void GetClosestTarget(Transform obj1, Transform obj2)
    {
        Vector2 ownerPos = this.user1.Value.GetOwner(this).position;
        float distance1 = Vector2.Distance(obj1.position, ownerPos);
        float distance2 = Vector2.Distance(obj2.position, ownerPos);

        if (distance1 <= distance2) this.target = obj1;
        else this.target = obj2;
    }
}
