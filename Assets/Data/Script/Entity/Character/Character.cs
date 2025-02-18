using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class Character : Entity
{
    //==========================================Variable==========================================
    [Header("=====Character=====")]
    [Header("Stat")]
    [SerializeField] protected int maxHealth;
    [SerializeField] protected int health;

    [Header("Component")]
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected CapsuleCollider2D bodyCol;
    [SerializeField] protected DamageReceiver damageRecv;
    [SerializeField] protected PushBackReceiver pushBackRecv;

    //===========================================Unity============================================
    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadComponent(ref this.rb, transform, "LoadRb()");
        this.LoadComponent(ref this.bodyCol, transform, "LoadBodyCol()");
        this.LoadComponent(ref this.damageRecv, transform.Find("DamageRecv"), "LoadDamageRecv()");
        this.LoadComponent(ref this.pushBackRecv, transform.Find("PushBackRecv"), "LoadPushBackRecv()");
    }
}
