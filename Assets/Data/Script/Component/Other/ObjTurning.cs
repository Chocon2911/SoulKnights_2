using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjTurning : HuyMonoBehaviour
{
    //==========================================Variable==========================================
    [Header("Obj Turning")]
    [SerializeField] protected InterfaceReference<IObjTurning> user;

    //==========================================Get Set===========================================
    public IObjTurning User { set => user.Value = value; }

    //===========================================Unity============================================
    protected virtual void Update()
    {
        this.Turning();
    }

    //===========================================Method===========================================
    protected virtual void Turning()
    {
        if (!this.user.Value.CanTurn(this)) return;
        this.Turn();
    }

    protected virtual void Turn()
    {
        Transform mainObj = this.user.Value.GetMainObj(this);
        Transform turnObj = this.user.Value.GetTurnObj(this);

        float angle = mainObj.localEulerAngles.z;
        float xRot = turnObj.localEulerAngles.x;
        float yRot = turnObj.localEulerAngles.y;
        float zRot = turnObj.localEulerAngles.z;

        if (Mathf.Cos(angle * Mathf.Deg2Rad) > 0 && xRot > 0) // Turn Right
        {
            turnObj.localRotation = Quaternion.Euler(xRot + 180, yRot, zRot);
        }

        else if (Mathf.Cos(angle * Mathf.Deg2Rad) <= 0 && xRot <= 0) // Turn Left
        {
            turnObj.localRotation = Quaternion.Euler(xRot - 180, yRot, zRot);
        }
    }
}
