using System.Collections;
using UnityEngine;

public interface IPushBackReceiver
{
    bool CanPushBack(PushBackReceiver component);
    Rigidbody2D GetRb(PushBackReceiver component);
}

public class PushBackReceiver : HuyMonoBehaviour, ILateFixedUpdate
{
    //==========================================Variable==========================================
    [Header("Push Back Receiver")]
    [SerializeField] protected InterfaceReference<IPushBackReceiver> user;
    [SerializeField] protected float recvForce;
    [SerializeField] protected Cooldown cd;
    [SerializeField] protected Vector2 dir;

    //==========================================Get Set===========================================
    public float RecvForce => recvForce;
    public Vector2 Dir => dir;

    //===========================================Unity============================================
    protected virtual void Update()
    {
        this.Finishing();
    }

    protected virtual void FixedUpdate()
    {
        this.Recharging();
        this.PushingBack();
    }

    //===========================================Method===========================================
    public void Receive(float force, Vector2 dir, float duration)
    {
        this.recvForce = force;
        this.dir = dir;
        this.cd.TimeLimit = duration;
    }

    //=========================================Push Back==========================================
    protected virtual void PushingBack()
    {
        if (!this.user.Value.CanPushBack(this)) return;
        this.PushBack();
    }

    protected virtual void PushBack()
    {
        this.user.Value.GetRb(this).velocity += this.recvForce * this.dir;
        this.recvForce = 0;
    }

    //==========================================Recharge==========================================
    protected virtual void Recharging()
    {
        if (this.cd.TimeLimit <= 0) return;
        if (!this.cd.IsReady) return;
        this.Recharge();
    }

    protected virtual void Recharge()
    {
        this.cd.CoolingDown();
    }

    //===========================================Finish===========================================
    protected virtual void Finishing()
    {
        if (!this.cd.IsReady) return;
        this.Finish();
    }

    protected virtual void Finish()
    {
        this.recvForce = 0;
        this.dir = Vector2.zero;
        this.cd.TimeLimit = 0;
        this.cd.ResetStatus();
    }

    //======================================ILateFixedUpdate======================================
    void ILateFixedUpdate.LateFixedUpdate()
    {
        this.PushingBack();
    }
}