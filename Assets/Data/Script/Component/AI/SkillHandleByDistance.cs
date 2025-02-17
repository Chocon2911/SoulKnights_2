using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkillHandleByDistance : ISkillHandler
{
    Vector2 GetMainObjPos(SkillHandleByDistance component);
    Vector2 GetTargetPos(SkillHandleByDistance component);
}

public class SkillHandleByDistance : SkillHandler
{
    //==========================================Variable==========================================
    [Header("By Distance")]
    [SerializeField] protected InterfaceReference<ISkillHandleByDistance> user1;
    [SerializeField] protected List<float> distances;

    //==========================================Get Set===========================================
    public ISkillHandleByDistance User1 { set => this.user1.Value = value; }

    //==========================================Override==========================================
    public override Skill GetChosenSkill()
    {
        Vector2 mainObjPos = this.user1.Value.GetMainObjPos(this);
        Vector2 targetPos = this.user1.Value.GetTargetPos(this);
        float currDistance = Vector2.Distance(mainObjPos, targetPos);

        for (int i = 0; i < this.distances.Count; i++)
        {
            if (currDistance > this.distances[i]) continue;
            return this.user1.Value.GetSkills(this)[i];
        }

        return null;
    }
}
