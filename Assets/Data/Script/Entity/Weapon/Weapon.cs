using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Entity, ISkill, IObjHolder, IShootSkill
{
    //==========================================Variable==========================================
    [Header("=====Weapon=====")]
    [SerializeField] private InterfaceReference<IWeapon> user;
    [SerializeField] protected ObjHolder objHolder;
    [SerializeField] protected Skill skill;

    //==========================================Get Set===========================================
    public IWeapon User { get => this.user.Value; set => this.user.Value = value; }
    public ObjHolder ObjHolder { get => this.objHolder; set => this.objHolder = value; }
    public Skill Skill { get => this.skill; set => this.skill = value; }

    //===========================================Unity============================================
    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadComponent(ref this.objHolder, transform.Find("Hold"), "LoadObjHolder()");
        this.objHolder.User = this;
        this.LoadComponent(ref this.skill, transform.Find("Skill"), "LoadSkill()");
        this.skill.User = this;

        // ShootSkill
        if (this.skill is ShootSkill shootSkill)
        {
            shootSkill.User1 = this;
        }
    }

    protected virtual void OnEnable()
    {
        this.objHolder.Holding();
    }

    //===========================================ISkill===========================================
    bool ISkill.CanRechargeSkill(Skill component)
    {
        if (component == this.skill)
        {
            return this.user.Value.CanSkillRecharge(this);
        }

        Util.Instance.IComponentErrorLog(transform, component.transform);
        return false;
    }

    bool ISkill.CanUseSkill(Skill component)
    {
        if (this.skill == component)
        {
            return this.user.Value.CanSkillRecharge(this);
        }

        Util.Instance.IComponentErrorLog(transform, component.transform);
        return false;
    }

    void ISkill.ConsumePower(Skill component)
    {
        if (component == this.skill)
        {
            this.user.Value.ConsumePower(this);
            return;
        }

        Util.Instance.IComponentErrorLog(transform, component.transform);
        return;
    }

    //=========================================IObjHolder=========================================
    bool IObjHolder.CanHold(ObjHolder component)
    {
        if (this.objHolder == component)
        {
            if (this.user.Value.GetOwner(this) == null) return false;
            return true;
        }

        Util.Instance.IComponentErrorLog(transform, component.transform);
        return false;
    }

    Transform IObjHolder.GetModelObj(ObjHolder component)
    {
        if (this.objHolder == component)
        {
            return this.model.transform;
        }

        Util.Instance.IComponentErrorLog(transform, component.transform);
        return null;
    }

    Vector2 IObjHolder.GetMainObjPos(ObjHolder component)
    {
        if (this.objHolder == component)
        {
            return this.user.Value.GetOwner(this).position;
        }

        Util.Instance.IComponentErrorLog(transform, component.transform);
        return Vector2.zero;
    }

    Vector2 IObjHolder.GetTargetPos(ObjHolder component)
    {
        if (this.objHolder == component)
        {
            return this.user.Value.GetTargetPos(this);
        }

        Util.Instance.IComponentErrorLog(transform, component.transform);
        return Vector2.zero;
    }

    Transform IObjHolder.GetHoldObj(ObjHolder component)
    {
        if (this.objHolder == component)
        {
            return transform;
        }

        Util.Instance.IComponentErrorLog(transform, component.transform);
        return null;
    }

    //========================================IShootSkill=========================================
    int IShootSkill.GetShootState(ShootSkill component)
    {
        if (this.skill == component)
        {
            return this.user.Value.GetSkillState(this);
        }

        Util.Instance.IComponentErrorLog(transform, component.transform);
        return -1;
    }
}
