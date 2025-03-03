using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class Character : Entity, IDamageReceiver, IPushBackReceiver
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
        this.damageRecv.User = this;
        this.LoadComponent(ref this.pushBackRecv, transform.Find("PushBackRecv"), "LoadPushBackRecv()");
        this.pushBackRecv.User = this;
    }



    //============================================================================================
    //=========================================Interface==========================================
    //============================================================================================

    //======================================IDamageReceiver=======================================
    void IDamageReceiver.ReduceHealth(DamageReceiver component, int damage)
    {
        this.health -= damage;
    }

    //=====================================IPushBackReceiver======================================
    bool IPushBackReceiver.CanPushBack(PushBackReceiver component)
    {
        return true;
    }

    Rigidbody2D IPushBackReceiver.GetRb(PushBackReceiver component)
    {
        return this.rb;
    }
}
