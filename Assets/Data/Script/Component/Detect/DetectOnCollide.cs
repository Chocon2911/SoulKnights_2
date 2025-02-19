using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectOnCollide : DetectByCollide, ILateFixedUpdate
{
    //===========================================Unity============================================
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        this.Detecting(collision);
    }

    //======================================ILateFixedUpdate======================================
    void ILateFixedUpdate.LateFixedUpdate()
    {
        this.targets.Clear();
    }
}
