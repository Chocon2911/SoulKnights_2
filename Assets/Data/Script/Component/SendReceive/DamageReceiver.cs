using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageReceiver
{
    void ReduceHealth(DamageReceiver component, int damage);
}


public class DamageReceiver : HuyMonoBehaviour
{
    //==========================================Variable==========================================
    [Header("Damage Receiver")]
    [SerializeField] private InterfaceReference<IDamageReceiver> user;
    [SerializeField] private bool isDamage;

    //==========================================Get Set===========================================
    public IDamageReceiver User { get => user.Value; set => user.Value = value; }
    public bool IsDamage { get => isDamage; }

    protected virtual void LateUpdate()
    {
        this.isDamage = false;
    }

    //===========================================Method===========================================
    public void Receive(int damage)
    {
        this.user.Value.ReduceHealth(this, damage);
        this.isDamage = true;
    }
}
