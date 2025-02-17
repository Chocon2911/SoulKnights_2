using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDetector
{
    bool CanDetect(Detector component);
}


public abstract class Detector : HuyMonoBehaviour
{
    //==========================================Variable==========================================
    [SerializeField] protected InterfaceReference<IDetector> user;
    [Header("Detector")]
    [SerializeField] protected Transform target;
    [SerializeField] protected List<string> tags;

    //==========================================Get Set===========================================
    public IDetector User { get => user.Value; set => user.Value = value; }
    public virtual Transform Target => target;

    //===========================================public===========================================
    public abstract void ResetTarget();
}
