using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IChargeMoveSpeed : ISkill
{
    void SetMoveSpeed(ChargeMoveSpeed component, float value);
}
