using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Detector : HuyMonoBehaviour
{
    //==========================================Variable==========================================
    [SerializeField] protected InterfaceReference<IDetector> user;
    [Header("Detector")]
    [SerializeField] protected List<string> tags;

    //==========================================Get Set===========================================
    public IDetector User { get => user.Value; set => user.Value = value; }
    public abstract Transform Target { get; }

    //===========================================public===========================================
    public abstract void ResetTarget();
}
