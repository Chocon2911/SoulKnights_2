using System.Collections;
using UnityEngine;

public interface IPushBackSender
{
    Vector2 GetDir(PushBackSender component);
}

public class PushBackSender : HuyMonoBehaviour
{
    //==========================================Variable==========================================
    [Header("Push Back Sender")]
    [SerializeField] protected InterfaceReference<IPushBackSender> user;
    [SerializeField] protected float force;

    public IPushBackSender User { set => user.Value = value; }

    //===========================================Method===========================================
    public void Send(PushBackReceiver receiver)
    {
        Vector2 dir = this.user.Value.GetDir(this);
        receiver.Receive(this.force, dir);
    }
}