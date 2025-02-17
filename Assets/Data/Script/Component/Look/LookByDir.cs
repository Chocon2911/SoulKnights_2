using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILookByDir
{
    bool CanLook(LookByDir component);
    Transform GetMainObj(LookByDir component);
    float GetXDir(LookByDir component);
}


public class LookByDir : HuyMonoBehaviour
{
    [Header("Look By Dir")]
    [SerializeField] protected InterfaceReference<ILookByDir> user;

    public ILookByDir User { set => this.user.Value = value; }

    protected virtual void Update()
    {
        this.Looking();
    }

    protected virtual void Looking()
    {
        if (!this.user.Value.CanLook(this)) return;
        this.Look();
    }

    protected virtual void Look()
    {
        float xDir = this.user.Value.GetXDir(this);
        Transform mainObj = this.user.Value.GetMainObj(this);
        float xRot = mainObj.eulerAngles.x;
        float yRot = mainObj.eulerAngles.y;
        float zRot = mainObj.eulerAngles.z;

        if (xDir > 0 && yRot >= 180) // Turn Right
        {
            mainObj.localRotation = Quaternion.Euler(xRot, yRot - 180, zRot);
        }

        else if (xDir < 0 && yRot < 180) // Turn Left
        {
            mainObj.localRotation = Quaternion.Euler(xRot, yRot + 180, zRot);
        }
    }
}
