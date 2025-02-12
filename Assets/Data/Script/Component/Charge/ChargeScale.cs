using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeScale : Chargement
{
    //==========================================Variable==========================================
    [Header("Scale")]
    [SerializeField] private InterfaceReference<IChargeScale> user1;
    [SerializeField] protected List<float> scaleMul; // Scale Multiplier

    //==========================================Get Set===========================================
    public IChargeScale User1 { get => user1.Value; set => user1.Value = value; }
    public List<float> ScaleMul { get => scaleMul; set => scaleMul = value; }

    //===========================================Unity============================================
    protected virtual void OnDisable()
    {
        this.user1.Value.ResetToDefaultScale(this);
    }

    //==========================================Override==========================================
    protected override void Charge()
    {
        base.Charge();
        IChargeScale tempUser = this.user1.Value;
        float value = this.scaleMul[this.chargeState - 1] * Time.fixedDeltaTime;
        tempUser.MulChargeScale(this, value);
    }
}
