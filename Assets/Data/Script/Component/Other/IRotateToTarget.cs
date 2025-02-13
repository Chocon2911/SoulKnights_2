using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRotateToTarget
{
    bool CanRotate(RotateToTarget component);
    Transform GetTarget(RotateToTarget component);
}
