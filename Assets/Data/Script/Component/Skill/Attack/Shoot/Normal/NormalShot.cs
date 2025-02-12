using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NormalShot : ShootSkill
{
    //==========================================Variable==========================================
    [Header("Normal")]
    [SerializeField] protected NormalShotMode mode;

    //==========================================Override==========================================
    protected override void UsingSkill()
    {
        if (this.mode == NormalShotMode.AUTO)
        {
            if (this.user1.Value.GetShootState(this) <= 0) return;
        }

        else if (this.mode == NormalShotMode.SEMI)
        {
            if (this.user1.Value.GetShootState(this) != 1) return;
        }

        base.UsingSkill();
    }

    protected override bool CanMove(Bullet component)
    {
        return true;
    }

    protected override bool CanCharge(Bullet component)
    {
        return false;
    }

    protected override bool CanFinishCharge(Bullet component)
    {
        return false;
    }
}
