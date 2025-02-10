using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargableBullet : Bullet, IChargeMoveSpeed, IChargeScale
{
    //==========================================Variable==========================================
    [Header("Chargable")]
    [SerializeField] private InterfaceReference<IChargableBullet> user1;
    [SerializeField] protected List<Chargement> chargements;

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
    void IChargeMoveSpeed.SetMoveSpeed(ChargeMoveSpeed component, float value)
    {
        foreach (ChargeMoveSpeed chargement in this.chargements)
        {
            if (component != chargement) continue;
            this.movement.MoveSpeed = value;
        }

        Util.Instance.IComponentErrorLog(transform, component.transform);
        return;
    }

    //=====================================IChargeScaleSkill======================================
    void IChargeScale.MulChargeScale(ChargeScale component, float value)
    {
        foreach (ChargeScale chargement in this.chargements)
        {
            if (component != chargement) continue;
            transform.localScale *= value;
        }

        Util.Instance.IComponentErrorLog(transform, component.transform);
        return;
    }
}
