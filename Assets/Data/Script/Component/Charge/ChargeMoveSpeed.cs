using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IChargeMoveSpeed : IChargement
{
    void SetMoveSpeed(ChargeMoveSpeed component, float value);
}

public class ChargeMoveSpeed : Chargement
{
    //==========================================Variable==========================================
    [Header("Move Speed")]
    [SerializeField] private InterfaceReference<IChargeMoveSpeed> user1;
    [SerializeField] protected List<float> moveSpeeds;

    //==========================================Get Set===========================================
    public IChargeMoveSpeed User1 { get => this.user1.Value; set => this.user1.Value = value; }
    public List<float> MoveSpeeds { get => this.moveSpeeds; set => this.moveSpeeds = value; }

    //==========================================Override==========================================
    protected override void IncreaseState()
    {
        base.IncreaseState();
        IChargeMoveSpeed tempUser = this.user1.Value;
        float value = this.moveSpeeds[this.chargeState - 1];
        tempUser.SetMoveSpeed(this, value);
    }
}
