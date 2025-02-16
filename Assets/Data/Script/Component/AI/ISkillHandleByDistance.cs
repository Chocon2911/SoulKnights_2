using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkillHandleByDistance : ISkillHandler
{
    Vector2 GetMainObjPos(SkillHandleByDistance component);
    Vector2 GetTargetPos(SkillHandleByDistance component);
}
