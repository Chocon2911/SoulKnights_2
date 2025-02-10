using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CircleCollider2D))]
public class DetectByCollide : Detector
{
    //==========================================Variable==========================================
    [Header("By Collide")]
    [SerializeField] protected Collider2D colObj;
    [SerializeField] protected CircleCollider2D detectCollider;

    //===========================================Unity============================================
    private void OnTriggerStay2D(Collider2D collision)
    {
        this.colObj = collision;
        this.Detecting();
    }

    //==========================================Override==========================================
    protected override void Detect()
    {
        foreach (LayerMask mask in this.layerMasks)
        {
            if (this.colObj.gameObject.layer != mask.value) continue;
            this.target = colObj.transform;
        }

        this.colObj = null;
    }
}
