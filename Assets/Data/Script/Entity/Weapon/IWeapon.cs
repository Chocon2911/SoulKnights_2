using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    Vector2 GetTargetPos(Weapon component);
    Transform GetOwner(Weapon component);
    bool CanSkillRecharge(Weapon component);
    int GetSkillState(Weapon component);
    void ConsumePower(Weapon component);
}
