using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargableBullet : Bullet, IChargeMoveSpeedSkill, IChargeScaleSkill
{
    //==========================================Variable==========================================
    [Header("Chargable")]
    [SerializeField] private InterfaceReference<IChargableBullet> user1;
    [SerializeField] protected List<ChargeSkill> chargeSkills;

    //==========================================Get Set===========================================
    public IChargableBullet User1 { get => user1.Value; set => user1.Value = value; }

    //============================================================================================
    //=========================================Interface==========================================
    //============================================================================================

    //===========================================ISkill===========================================
    bool ISkill.CanUseSkill(Skill component)
    {
        throw new System.NotImplementedException();
    }

    bool ISkill.CanRechargeSkill(Skill component)
    {
        throw new System.NotImplementedException();
    }

    void ISkill.ConsumePower(Skill component)
    {
        throw new System.NotImplementedException();
    }

    //===================================IChargeMoveSpeedSkill====================================
    void IChargeMoveSpeedSkill.SetMoveSpeed(ChargeMoveSpeedSkill component, float value)
    {
        throw new System.NotImplementedException();
    }

    //=====================================IChargeScaleSkill======================================
    void IChargeScaleSkill.MulChargeScale(ChargeScaleSkill component, float value)
    {
        throw new System.NotImplementedException();
    }
}
