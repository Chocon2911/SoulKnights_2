using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IChargeScale : IChargement
{
    void MulChargeScale(ChargeScale component, float value);
}
