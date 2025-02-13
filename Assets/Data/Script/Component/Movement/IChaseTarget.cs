using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IChaseTarget : IMoveForward
{
    Transform GetTarget(ChaseTarget component);
    bool CanRotate(ChaseTarget component);
}
