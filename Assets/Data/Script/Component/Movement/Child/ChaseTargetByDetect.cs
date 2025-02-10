using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseTargetByDetect : MoveForward, IRotateToTarget
{
    //==========================================Variable==========================================
    [Header("Chase Target By Detect")]
    [SerializeField] private InterfaceReference<IChaseTargetByDetect> user2;
    [SerializeField] protected Detector detector;
    [SerializeField] protected RotateToTarget rotToTarget;

    //==========================================Get Set===========================================
    public IChaseTargetByDetect User2 { get => user2.Value; set => user2.Value = value; }

    //===========================================Unity============================================
    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadComponent(ref this.detector, this.transform.Find("Detector"), "LoadDetector()");
        this.LoadComponent(ref this.rotToTarget, this.transform.Find("RotateToTarget"), 
            "LoadRotateToTarget()");
    }

    //======================================IRotateToTarget=======================================
    public bool CanRotate(RotateToTarget rotToTarget)
    {
        return this.user2.Value.CanRotateToTarget(this);
    }

    Transform IRotateToTarget.GetTarget(RotateToTarget component)
    {
        if (this.rotToTarget == component)
        {
            return this.detector.Target;
        }

        Debug.LogError("component not found", transform.gameObject);
        Debug.LogError("wrong component source", component.gameObject);
        return null;
    }
}
