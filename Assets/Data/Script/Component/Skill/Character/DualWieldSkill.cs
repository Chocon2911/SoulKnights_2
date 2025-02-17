using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDualWieldSkill : ISkill
{
    Weapon GetChosenWeapon(DualWieldSkill component);
    bool CanFinishSkillImmediately(DualWieldSkill component);
    Vector2 GetWeaponTargetPos(DualWieldSkill component);
    bool CanWeaponSkillRecharge(DualWieldSkill component);
    int GetWeaponSkillState(DualWieldSkill component);
    void WeaponConsumePower(DualWieldSkill component);
}

public class DualWieldSkill : Skill, IWeapon
{
    //============================================Get=============================================
    [Header("Dual Wield")]
    [SerializeField] private InterfaceReference<IDualWieldSkill> user1;
    [SerializeField] protected Cooldown dualWieldCD;
    [SerializeField] protected Weapon chosenWeapon;
    [SerializeField] protected Weapon cloneWeapon;
    [SerializeField] protected Transform leftArm;
    [SerializeField] protected bool canDualWield;

    //==========================================Get Set===========================================
    public IDualWieldSkill User1 { get => user1.Value; set => user1.Value = value; }
    public Weapon ChosenWeapon { get => chosenWeapon; set => chosenWeapon = value; }

    //===========================================Unity============================================
    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadComponent(ref this.leftArm, transform.Find("LeftArm"), "LoadLeftArm()");
    }

    protected override void FixedUpdate()
    {
        this.RechargingDualWield();
        base.FixedUpdate();
        this.FinishingSkill();
        this.FinishingImediately();
    }

    //==========================================Override==========================================
    protected override void UsingSkill()
    {
        IDualWieldSkill tempUser = this.user1.Value;
        if (!tempUser.CanUseSkill(this)) return;
        base.UsingSkill();
    }

    protected override void UseSkill()
    {
        base.UseSkill();
        this.canDualWield = true;
        IDualWieldSkill tempUser = this.user1.Value;
        this.chosenWeapon = tempUser.GetChosenWeapon(this);
        this.CloneWeapon();
    }

    //==========================================Recharge==========================================
    protected virtual void RechargingDualWield()
    {
        if (!this.canDualWield) return;
        this.RechargeDualWield();
    }

    protected virtual void RechargeDualWield()
    {
        this.dualWieldCD.CoolingDown();
    }

    //===========================================Finish===========================================
    protected virtual void FinishingSkill()
    {
        if (!this.dualWieldCD.IsReady) return;
        this.FinishSkill();
    }

    protected virtual void FinishSkill()
    {
        this.canDualWield = false;
        this.dualWieldCD.ResetStatus();
        this.skillCD.ResetStatus();
        WeaponSpawner.Instance.Despawn(this.cloneWeapon.transform);
        this.chosenWeapon = null;
        this.cloneWeapon = null;
    }

    //=====================================Finish Imediately======================================
    protected virtual void FinishingImediately()
    {
        if (!this.canDualWield) return;
        if (!this.user1.Value.CanFinishSkillImmediately(this)) return;
        this.FinishSkill();
    }

    //===========================================Other============================================
    protected virtual void CloneWeapon()
    {
        Transform spawnObj = this.chosenWeapon.transform;
        Vector3 spawnPos = this.leftArm.position;
        Quaternion spawnRot = this.leftArm.rotation;
        Transform newObj = WeaponSpawner.Instance.SpawnByObj(spawnObj, spawnPos, spawnRot);

        if (newObj == null)
        {
            Debug.LogError("New Weapon is null (transform)", transform.gameObject);
            Debug.LogError("New Weapon is null (chosenWeapon)", spawnObj.gameObject);
            return;
        }

        this.cloneWeapon = newObj.GetComponent<Weapon>();
        this.cloneWeapon.User = this;
        newObj.gameObject.SetActive(true);
    }

    //============================================================================================
    //=========================================Interface==========================================
    //============================================================================================

    //==========================================IWeapon===========================================
    Vector2 IWeapon.GetTargetPos(Weapon component)
    {
        if (this.cloneWeapon == component)
        {
            return this.user1.Value.GetWeaponTargetPos(this);
        }

        Util.Instance.IComponentErrorLog(transform, component.transform);
        return Vector2.zero;
    }

    Transform IWeapon.GetOwner(Weapon component)
    {
        if (this.cloneWeapon == component)
        {
            return this.leftArm;
        }

        Util.Instance.IComponentErrorLog(transform, component.transform);
        return null;
    }

    bool IWeapon.CanSkillRecharge(Weapon component)
    {
        if (this.cloneWeapon == component)
        {
            return this.user1.Value.CanWeaponSkillRecharge(this);
        }

        Util.Instance.IComponentErrorLog(transform, component.transform);
        return false;
    }

    int IWeapon.GetSkillState(Weapon component)
    {
        if (this.cloneWeapon == component)
        {
            return this.user1.Value.GetWeaponSkillState(this);
        }

        Util.Instance.IComponentErrorLog(transform, component.transform);
        return -1;
    }

    void IWeapon.ConsumePower(Weapon component)
    {
        if (this.cloneWeapon == component)
        {
            this.user1.Value.WeaponConsumePower(this);
            return;
        }

        Util.Instance.IComponentErrorLog(transform, component.transform);
        return;
    }
}