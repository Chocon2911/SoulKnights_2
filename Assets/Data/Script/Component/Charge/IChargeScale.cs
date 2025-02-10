using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IChargeScale : ISkill
{
    void MulChargeScale(ChargeScale component, float value);
}
