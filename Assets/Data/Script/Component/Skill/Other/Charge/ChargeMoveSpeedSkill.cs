using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeMoveSpeedSkill : ChargeSkill
{
    //==========================================Variable==========================================
    [Header("Move Speed")]
    [SerializeField] private InterfaceReference<IChargeMoveSpeedSkill> user2;
    [SerializeField] protected List<float> moveSpeeds;

    //==========================================Get Set===========================================
    public IChargeMoveSpeedSkill User2 { get => this.user2.Value; set => this.user2.Value = value; }
    public List<float> MoveSpeeds { get => this.moveSpeeds; set => this.moveSpeeds = value; }

    //==========================================Override==========================================
    protected override void IncreaseState()
    {
        base.IncreaseState();
        IChargeMoveSpeedSkill tempUser = this.user2.Value;
        float value = this.moveSpeeds[this.chargeState - 1];
        tempUser.SetMoveSpeed(this, value);
    }
}
