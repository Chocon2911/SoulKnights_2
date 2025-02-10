using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkill
{
    bool CanUseSkill(Skill component);
    bool CanRechargeSkill(Skill component);
    void ConsumePower(Skill component);
}
