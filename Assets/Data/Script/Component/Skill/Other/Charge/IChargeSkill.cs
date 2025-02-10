using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IChargeSkill : ISkill
{
    bool CanFinishSkill(ChargeSkill component);
}
