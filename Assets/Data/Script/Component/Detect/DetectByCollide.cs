using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDetectByCollide : IDetector
{
    Transform GetOwner(DetectByCollide component);
}


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
    public override Transform Target
    {
        get
        {
            this.GetClosestTarget();
            return base.Target;
        }
    }

    //===========================================Unity============================================
    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadComponent(ref this.detectCol, transform, "LoadDetectCol()");
    }

    //==========================================Override==========================================
    public override void ResetTarget()
    {
        this.targets.Clear();
    }

    //===========================================Method===========================================
    protected virtual void GetClosestTarget()
    {
        this.target = null;
        if (this.targets.Count == 0) return;
        Transform owner = this.user1.Value.GetOwner(this);
        List<Transform> deletedTargets = new List<Transform>();

        foreach (Transform child in this.targets)
        {
            if (!this.IsObjInRange(child))
            {
                deletedTargets.Add(child);
                continue;
            }

            if (this.target == null)
            {
                this.target = child;
                continue;
            }

            float targetDistance = Vector2.Distance(child.position, owner.position);
            float currTargetDistance = Vector2.Distance(this.target.position, owner.position);

            if (targetDistance >= currTargetDistance) continue;
            this.target = child;
        }
    }

    protected bool IsObjInRange(Transform obj)
    {
        if (obj == null) return false;
        Transform owner = this.user1.Value.GetOwner(this);
        return Vector2.Distance(obj.position, owner.position) <= this.detectCol.radius;
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
            foreach (Transform child in this.targets)
            {
                if (child != col.transform) continue;
                return;
            }

            this.targets.Add(col.transform);
        }
    }
}
