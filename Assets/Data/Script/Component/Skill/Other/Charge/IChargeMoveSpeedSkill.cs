using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IChargeMoveSpeedSkill : ISkill
{
    void SetMoveSpeed(ChargeMoveSpeedSkill component, float value);
}
