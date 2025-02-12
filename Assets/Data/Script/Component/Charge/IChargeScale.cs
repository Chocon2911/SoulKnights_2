using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IChargeScale : IChargement
{
    void ResetToDefaultScale(ChargeScale component);
    void MulChargeScale(ChargeScale component, float value);
}
