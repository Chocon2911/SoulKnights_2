using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillHandler : HuyMonoBehaviour
{
    //==========================================Variable==========================================
    [Header("Skill Handler")]
    [SerializeField] protected InterfaceReference<ISkillHandler> user;

    //==========================================Get Set===========================================
    public ISkillHandler User { set => this.user.Value = value; }

    //===========================================Method===========================================
    public abstract Skill GetChosenSkill();
}
