using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageReceiver
{
    void ReduceHealth(DamageReceiver component, int damage);
}
