using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseTarget : MoveForward, IRotateToTarget
{
    //==========================================Variable==========================================
    [Header("Chase Target")]
    [SerializeField] private InterfaceReference<IChaseTarget> user2;
    [SerializeField] protected RotateToTarget rotToTarget;

    //==========================================Get Set===========================================
    public IChaseTarget User2 { get => this.user2.Value; set => this.user2.Value = value; }

    //===========================================Unity============================================
    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadComponent(ref this.rotToTarget, this.transform.Find("RotateToTarget"), 
            "LoadRotateToTarget()");
    }

    //======================================IRotateToTarget=======================================
    bool IRotateToTarget.CanRotate(RotateToTarget component)
    {
        if (this.rotToTarget == component)
        {
            if (this.user2.Value.CanRotate(this)) return true;
            else return false;
        }

        Debug.LogError("component not found", transform.gameObject);
        Debug.LogError("wrong component source", component.gameObject);
        return false;
    }
    
    Transform IRotateToTarget.GetTarget(RotateToTarget component)
    {

        if (this.rotToTarget == component)
        {
            return this.user2.Value.GetTarget(this);
        }

        Debug.LogError("component not found", transform.gameObject);
        Debug.LogError("wrong component source", component.gameObject);
        return null;
    }
}
