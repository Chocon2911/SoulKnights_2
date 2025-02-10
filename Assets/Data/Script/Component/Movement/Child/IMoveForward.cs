using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveForward : IMovement
{
    float GetAngle(MoveForward component);
}
