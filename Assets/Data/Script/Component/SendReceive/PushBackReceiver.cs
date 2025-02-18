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
    [SerializeField] protected Vector2 dir;

    //==========================================Get Set===========================================
    public float RecvForce => recvForce;
    public Vector2 Dir => dir;

    //===========================================Method===========================================
    public void Receive(float force, Vector2 dir)
    {
        this.recvForce = force;
        this.dir = dir;
    }

    protected virtual void PushingBack()
    {
        if (!this.user.Value.CanPushBack(this)) return;
        this.PushBack();
    }

    protected virtual void PushBack()
    {
        this.user.Value.GetRb(this).velocity += this.recvForce * this.dir;
        this.recvForce = 0;
        this.dir = Vector2.zero;
    }

    //======================================ILateFixedUpdate======================================
    void ILateFixedUpdate.LateFixedUpdate()
    {
        this.PushingBack();
    }
}