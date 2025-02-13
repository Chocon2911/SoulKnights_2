using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveByKeyboard : Movement
{
    //==========================================Override==========================================
    protected override void Move()
    {
        this.moveDir = InputManager.Instance.MoveDir;
        base.Move();
    }
}
