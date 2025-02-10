using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeScale : Chargement
{
    //==========================================Variable==========================================
    [Header("Scale")]
    [SerializeField] private InterfaceReference<IChargeScale> user2;
    [SerializeField] protected List<float> scaleMul; // Scale Multiplier

    //==========================================Get Set===========================================
    public IChargeScale User2 { get => user2.Value; set => user2.Value = value; }
    public List<float> ScaleMul { get => scaleMul; set => scaleMul = value; }

    //==========================================Override==========================================
    protected override void Charge()
    {
        base.Charge();
        IChargeScale tempUser = this.user2.Value;
        float value = this.scaleMul[this.chargeState - 1] * Time.fixedDeltaTime;
        tempUser.MulChargeScale(this, value);
    }
}
