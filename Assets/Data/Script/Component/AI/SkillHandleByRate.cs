using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillHandleByRate : SkillHandler
{
    //==========================================Variable==========================================
    [Header("By Rate")]
    [SerializeField] protected List<int> rates;

    //==========================================Override==========================================
    public override Skill GetChosenSkill()
    {
        int randomIndex = this.GetRandomIndex();
        for (int i = 0; i < this.rates.Count; i++)
        {
            if (i >= this.user.Value.GetSkills(this).Count)
            {
                Debug.LogError("Rate is out of range", transform.gameObject);
                return null;
            }

            if (randomIndex > this.rates[i]) continue;
            return this.user.Value.GetSkills(this)[i];
        }

        Debug.LogError("Rate is not in range 0 to 100000", transform.gameObject);
        return null;
    }

    //===========================================Method===========================================
    protected virtual int GetRandomIndex()
    {
        return Random.Range(0, 100000);
    }
}
