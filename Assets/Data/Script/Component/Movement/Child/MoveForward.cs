using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : Movement
{
    //==========================================Variable==========================================
    [Header("Move Forward")]
    [SerializeField] private InterfaceReference<IMoveForward> user1;

    //==========================================Get Set===========================================
    public IMoveForward User1 { get => user1.Value; set => user1.Value = value; }

    //===========================================Method===========================================
    protected override void Move()
    {
        float angle = this.user1.Value.GetAngle(this);
        float xDir = Mathf.Cos(angle * Mathf.Deg2Rad);
        float yDir = Mathf.Sin(angle * Mathf.Deg2Rad);
        this.moveDir = new Vector2(xDir, yDir);
        base.Move();
    }
}
