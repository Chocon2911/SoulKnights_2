using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IChargeMoveSpeed : IChargement
{
    void SetMoveSpeed(ChargeMoveSpeed component, float value);
}
