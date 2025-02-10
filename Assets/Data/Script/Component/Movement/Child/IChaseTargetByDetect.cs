using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IChaseTargetByDetect : IMoveForward
{
    bool CanRotateToTarget(ChaseTargetByDetect component);
}
