using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkillHandler
{
    List<Skill> GetSkills(SkillHandler component);
}
