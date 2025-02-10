using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShootSkill : ISkill
{
    int GetShootState(ShootSkill component);
}
