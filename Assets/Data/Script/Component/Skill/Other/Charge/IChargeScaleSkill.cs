using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IChargeScaleSkill : ISkill
{
    void MulChargeScale(ChargeScaleSkill component, float value);
}
