using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Detector : HuyMonoBehaviour
{
    //==========================================Variable==========================================
    [SerializeField] private InterfaceReference<IDetector> user;
    [Header("Detector")]
    [SerializeField] protected Transform target;
    [SerializeField] protected List<string> tags;

    //==========================================Get Set===========================================
    public IDetector User { get => user.Value; set => user.Value = value; }
    public Transform Target => target;

    //===========================================Method===========================================
    public void ResetTarget()
    {
        this.target = null;
    }
    
    protected virtual void Detecting()
    {
        if (!this.user.Value.CanDetect(this)) return;
        this.Detect();
    }

    protected abstract void Detect();
}
