using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDashSkill : ISkill
{
    Rigidbody2D GetRb(DashSkill component);
    Vector2 GetDashDir(DashSkill component);
}
