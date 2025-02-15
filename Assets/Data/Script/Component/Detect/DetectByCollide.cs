using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DetectByCollide : Detector
{
    //==========================================Variable==========================================
    [Header("By Collide")]
    [SerializeField] protected InterfaceReference<IDetectByCollide> user1;
    [SerializeField] protected CircleCollider2D detectCol;
    [SerializeField] protected List<Transform> targets;

    //==========================================Get Set===========================================
    public IDetectByCollide User1 { set => this.user1.Value = value; }
    public List<Transform> Targets => targets;
    public override Transform Target => this.GetClosestTarget();

    //==========================================Override==========================================
    public override void ResetTarget()
    {
        this.targets.Clear();
    }

    //===========================================Method===========================================
    protected virtual Transform GetClosestTarget()
    {
        if (this.targets.Count == 0) return null;
        Transform owner = this.user1.Value.GetOwner(this);
        Transform closestTarget = null;

        foreach (Transform target in this.targets)
        {
            if (closestTarget == null)
            {
                closestTarget = target;
                continue;
            }

            float targetDistance = Vector2.Distance(target.position, owner.position);
            float currTargetDistance = Vector2.Distance(closestTarget.position, owner.position);

            if (targetDistance >= currTargetDistance) continue;
            closestTarget = target;
        }

        return closestTarget;
    }

    //===========================================Detect===========================================
    protected virtual void Detecting(Collider2D col)
    {
        if (!this.user.Value.CanDetect(this)) return;
        this.Detect(col);
    }

    protected virtual void Detect(Collider2D col)
    {
        foreach (string tag in this.tags)
        {
            if (tag != col.tag) continue;
            this.targets.Add(col.transform);
        }
    }
}
