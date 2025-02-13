using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObjHolder
{
    bool CanHold(ObjHolder component);
    Transform GetModelObj(ObjHolder component);
    Vector2 GetMainObjPos(ObjHolder component);
    Vector2 GetTargetPos(ObjHolder component);
    Transform GetHoldObj(ObjHolder component);
}
