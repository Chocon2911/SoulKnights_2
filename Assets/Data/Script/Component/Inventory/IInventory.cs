using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventory
{
    Vector2 GetWeaponHoldPos(Inventory component);
    void ChooseWeapon(Inventory component);
    bool CanWeaponSkillRecharge(Inventory component);
    int GetWeaponSkillState(Inventory component);
    void WeaponConsumePower(Inventory component);
}
