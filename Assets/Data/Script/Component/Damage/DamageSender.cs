using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageSender
{
    void OnSending(DamageSender component);
}

public class DamageSender : HuyMonoBehaviour
{
    //==========================================Variable==========================================
    [Header("Damage Sender")]
    [SerializeField] private InterfaceReference<IDamageSender> user;
    [SerializeField] protected int damage;

    //==========================================Get Set===========================================
    public IDamageSender User { get => user.Value; set => user.Value = value; }
    public int Damage { get => damage; set => damage = value; }

    //===========================================Method===========================================
    public void Send(DamageReceiver receiver)
    {
        receiver.Receive(this);
        this.user.Value.OnSending(this);
    }
}
