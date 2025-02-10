using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovement
{
    bool CanMove(Movement component);
    Rigidbody2D GetRb(Movement component);
}
