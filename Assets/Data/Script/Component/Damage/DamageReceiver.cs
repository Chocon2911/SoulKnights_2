using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceiver : HuyMonoBehaviour
{
    //==========================================Variable==========================================
    [Header("Damage Receiver")]
    [SerializeField] private InterfaceReference<IDamageReceiver> user;

    //==========================================Get Set===========================================
    public IDamageReceiver User { get => user.Value; set => user.Value = value; }

    //===========================================Method===========================================
    public void Receive(DamageSender sender)
    {
        this.user.Value.ReduceHealth(this, sender.Damage);
    }
}
