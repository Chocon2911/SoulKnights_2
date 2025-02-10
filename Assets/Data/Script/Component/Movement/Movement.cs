using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : HuyMonoBehaviour
{
    //==========================================Variable==========================================
    [Header("Movement")]
    [SerializeField] private InterfaceReference<IMovement> user;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected Vector2 moveDir;
    [SerializeField] protected bool isMove;

    //==========================================Get Set===========================================
    public virtual IMovement User { get => this.user.Value; set => this.user.Value = value; }
    public float MoveSpeed { get => this.moveSpeed; set => this.moveSpeed = value; }
    public Vector2 MoveDir { get => this.moveDir; set => this.moveDir = value; }
    public bool IsMove { get => this.isMove; set => this.isMove = value; }

    //===========================================Unity============================================
    protected virtual void FixedUpdate()
    {
        this.Moving();
        this.CheckingMove();
    }

    //============================================Move============================================
    protected virtual void Moving()
    {
        if (!this.user.Value.CanMove(this)) return;
        this.Move();
    }

    protected virtual void Move()
    {
        this.user.Value.GetRb(this).velocity = this.moveDir * this.moveSpeed;
    }

    //===========================================Check============================================
    protected virtual void CheckingMove()
    {
        if (this.moveDir == Vector2.zero) this.isMove = false;
        else this.isMove = true;
    }
}
