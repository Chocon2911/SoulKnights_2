using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToTarget : HuyMonoBehaviour
{
    //==========================================Variable==========================================
    [Header("Rotate To Target")]
    [SerializeField] private InterfaceReference<IRotateToTarget> user;
    [SerializeField] protected float rotateSpeed;

    //===========================================Unity============================================
    protected virtual void FixedUpdate()
    {
        this.Rotating();
    }

    //===========================================Method===========================================
    protected virtual void Rotating()
    {
        if (!this.user.Value.CanRotate(this)) return;
        this.Rotate();
    }

    protected virtual void Rotate()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, 
            user.Value.GetTarget(this).transform.rotation, rotateSpeed * Time.fixedDeltaTime);
    }
}
