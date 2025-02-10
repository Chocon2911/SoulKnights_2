using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDualWieldSkill : ISkill
{
    Weapon GetChosenWeapon(DualWieldSkill component);
    bool CanFinishSkillImmediately(DualWieldSkill component);
    Vector2 GetWeaponTargetPos(DualWieldSkill component);
    bool CanWeaponSkillRecharge(DualWieldSkill component);
    int GetWeaponSkillState(DualWieldSkill component);
    void WeaponConsumePower(DualWieldSkill component);
}
