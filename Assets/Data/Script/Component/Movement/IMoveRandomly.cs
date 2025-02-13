using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveRandomly : IMovement
{
    bool CanFinishMove(MoveRandomly component);
}
