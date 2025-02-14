using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjHolder : HuyMonoBehaviour
{
    //==========================================Variable==========================================
    [Header("Obj Holder")]
    [SerializeField] private InterfaceReference<IObjHolder> user;
    [SerializeField] protected float holdRange;

    //==========================================Get Set===========================================
    public IObjHolder User { get => user.Value; set => user.Value = value; }
    public float HoldRange { get => holdRange; set => holdRange = value; }

    //===========================================Unity============================================
    protected virtual void Update()
    {
        this.Holding();
    }

    //===========================================Method===========================================
    public virtual void Holding()
    {
        if (!this.user.Value.CanHold(this)) return;
        this.Hold();
    }

   protected virtual void Hold()
    {
        Vector2 mainObjPos = this.user.Value.GetMainObjPos(this);
        Vector2 targetPos = this.user.Value.GetTargetPos(this);
        Transform holdObj = this.user.Value.GetHoldObj(this);

        float angle = -Vector2.SignedAngle(targetPos - mainObjPos, Vector2.right);
        Vector2 pos = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad))
            * this.holdRange;

        holdObj.position = pos + mainObjPos; // Pos
        holdObj.rotation = Quaternion.Euler(0, 0, angle); // Rot
    }
}
