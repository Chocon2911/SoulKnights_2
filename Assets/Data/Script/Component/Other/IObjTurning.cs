using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObjTurning
{
    Transform GetMainObj(ObjTurning component);
    Transform GetTurnObj(ObjTurning component);
    bool CanTurn(ObjTurning component);
}
