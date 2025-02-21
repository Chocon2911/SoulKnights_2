using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Player : Character, IMovement, IRegen, IDashSkill, IInventory, IPickUpItem, 
    IDualWieldSkill, ILookByDir
{
    protected enum State
    {
        IDLE = 0,
        MOVE = 1,
    }

    //==========================================Variable==========================================
    [Header("=====Player=====")]
    [Header("Stat")]
    [SerializeField] protected int maxMana;
    [SerializeField] protected int mana;
    [SerializeField] protected int maxAmor;
    [SerializeField] protected int amor;
    [SerializeField] protected State state;

    [Header("Component")]
    [SerializeField] protected Movement movement;
    [SerializeField] protected DashSkill dash;
    [SerializeField] protected Regen regenAmor;
    [SerializeField] protected Inventory inventory;
    [SerializeField] protected PickUpItem pickUpItem;
    [SerializeField] protected Skill characterSkill;
    [SerializeField] protected LookByDir lookByMoveDir;



    //===========================================Unity============================================
    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadComponent(ref this.movement, transform.Find("Movement"), "LoadMovement()");
        this.movement.User = this;
        this.LoadComponent(ref this.dash, transform.Find("Dash"), "LoadDash()");
        this.dash.User = this;
        this.dash.User1 = this;
        this.LoadComponent(ref this.regenAmor, transform.Find("RegenAmor"), "LoadRegenAmor()");
        this.regenAmor.User = this;
        this.LoadComponent(ref this.inventory, transform.Find("Inventory"), "LoadInventory()");
        this.inventory.User = this;
        this.LoadComponent(ref this.pickUpItem, transform.Find("PickUp"), "LoadPickUpItem()");
        this.pickUpItem.User = this;
        this.LoadComponent(ref this.characterSkill, transform.Find("CharacterSkill"), "LoadCharacterSkill()");
        this.characterSkill.User = this;
        this.LoadComponent(ref this.lookByMoveDir, transform.Find("Look"), "LoadLook()");
        this.lookByMoveDir.User = this;

        // DualWieldSkill
        if (this.characterSkill is DualWieldSkill dualWieldSkill)
        {
            dualWieldSkill.User1 = this;
        }
    }

    protected virtual void Update()
    {
        this.StateHandler();
        this.AnimationHandler();
    }


    //=========================================Animation==========================================
    protected virtual void StateHandler()
    {
        if (this.movement.IsMove) this.state = State.MOVE;
        else this.state = State.IDLE;
    }

    protected virtual void AnimationHandler()
    {
        this.animator.SetInteger("State", (int)this.state);
    }



    //============================================================================================
    //=========================================Interface==========================================
    //============================================================================================

    //=========================================IMovement==========================================
    bool IMovement.CanMove(Movement component)
    {
        if (this.movement == component)
        {
            if (this.dash.CanDash) return false;
            if (this.pushBackRecv.IsPushBack) return false;
            else return true;
        }

        Debug.LogError("component not found", transform.gameObject);
        Debug.LogError("wrong component source", component.gameObject);
        return false;
    }
    
    Rigidbody2D IMovement.GetRb(Movement component)
    {
        if (this.movement == component)
        {
            return this.rb;
        }

        Debug.LogError("component not found", transform.gameObject);
        Debug.LogError("wrong component source", component.gameObject);
        return null;
    }

    //===========================================IRegen===========================================
    bool IRegen.CanRegen(Regen component)
    {
        if (this.regenAmor == component)
        {
            if (this.amor < this.maxAmor) return true;
            else return false;
        }

        Debug.LogError("component not found", transform.gameObject);
        Debug.LogError("wrong component source", component.gameObject);
        return false;
    }
    
    void IRegen.RestoreValue(Regen component)
    {
        if (this.regenAmor == component)
        {
            this.amor += (int)component.RegenValue;
            return;
        }

        Debug.LogError("component not found", transform.gameObject);
        Debug.LogError("wrong component source", component.gameObject);
        return;
    }

    //===========================================ISkill===========================================
    bool ISkill.CanUseSkill(Skill component)
    {
        // Dash
        if (this.dash == component)
        {
            return InputManager.Instance.SpaceState == 1;
        }

        // CharacterSkill
        if (this.characterSkill == component)
        {
            return InputManager.Instance.ShiftState >= 1;
        }

        Debug.LogError("component not found", transform.gameObject);
        Debug.LogError("wrong component source", component.gameObject);
        return false;
    }

    bool ISkill.CanRechargeSkill(Skill component)
    {
        // Dash
        if (this.dash == component)
        {
            return true;
        }

        // CharacterSkill
        if (this.characterSkill == component)
        {
            return true;
        }

        Debug.LogError("component not found", transform.gameObject);
        Debug.LogError("wrong component source", component.gameObject);
        return false;
    }

    void ISkill.ConsumePower(Skill skill)
    {
        // Dash
        if (this.dash == skill)
        {
            this.health -= skill.HealthCost;
            this.mana -= skill.ManaCost;
            return;
        }

        // CharacterSkill
        if (this.characterSkill == skill)
        {
            this.health -= skill.HealthCost;
            this.mana -= skill.ManaCost;
            return;
        }

        Debug.LogError("component not found", transform.gameObject);
        Debug.LogError("wrong component source", skill.gameObject);
        return;
    }

    //=========================================IDashSkill=========================================
    Vector2 IDashSkill.GetDashDir(DashSkill component)
    {
        if (this.dash == component)
        {
            return InputManager.Instance.MoveDir;
        }

        Debug.LogError("component not found", transform.gameObject);
        Debug.LogError("wrong component source", component.gameObject);
        return Vector2.zero;
    }

    Rigidbody2D IDashSkill.GetRb(DashSkill component)
    {
        if (this.dash == component)
        {
            return this.rb;
        }

        Debug.LogError("component not found", transform.gameObject);
        Debug.LogError("wrong component source", component.gameObject);
        return null;
    }

    //=========================================IInventory=========================================
    Vector2 IInventory.GetWeaponHoldPos(Inventory component)
    {
        if (this.inventory == component)
        {
            return InputManager.Instance.MousePos;
        }

        Util.Instance.IComponentErrorLog(transform, component.transform);
        return Vector2.zero;
    }
    
    void IInventory.ChooseWeapon(Inventory inventory)
    {
        if (this.inventory == inventory)
        {
            if (InputManager.Instance.HotBarState <= 0) return;
            if (InputManager.Instance.HotBarState > this.inventory.Weapons.Count) return;
            int prevSlot = this.inventory.ChosenSlot;
            this.inventory.ChosenSlot = InputManager.Instance.HotBarState;

            Transform prevWeapon = this.inventory.Weapons[prevSlot - 1].transform;
            Transform newWeapon = this.inventory.Weapons[this.inventory.ChosenSlot - 1].transform;

            prevWeapon.gameObject.SetActive(false);
            newWeapon.gameObject.SetActive(true);
            return;
        }

        Debug.LogError("component not found", transform.gameObject);
        Debug.LogError("wrong component source", inventory.gameObject);
        return;
    }

    bool IInventory.CanWeaponSkillRecharge(Inventory component)
    {
        if (this.inventory == component)
        {
            return true;
        }

        Debug.LogError("component not found", transform.gameObject);
        Debug.LogError("wrong component source", component.gameObject);
        return false;
    }

    int IInventory.GetWeaponSkillState(Inventory component)
    {
        if (this.inventory == component)
        {
            return InputManager.Instance.LeftClickState;
        }

        Debug.LogError("component not found", transform.gameObject);
        Debug.LogError("wrong component source", component.gameObject);
        return -1;
    }
    void IInventory.WeaponConsumePower(Inventory component)
    {
        if (this.inventory == component)
        {
            this.health -= this.inventory.GetChosenWeapon().Skill.HealthCost;
            this.mana -= this.inventory.GetChosenWeapon().Skill.ManaCost;
            return;
        }

        Debug.LogError("component not found", transform.gameObject);
        Debug.LogError("wrong component source", component.gameObject);
        return;
    }

    //========================================IPickUpItem=========================================
    public bool CanPickUp(PickUpItem component)
    {
        if (this.pickUpItem == component)
        {
            return true;
        }

        Debug.LogError("component not found", transform.gameObject);
        Debug.LogError("wrong component source", component.gameObject);
        return false;
    }

    public Inventory GetInventory(PickUpItem component)
    {
        if (this.pickUpItem == component)
        {
            return this.inventory;
        }

        Debug.LogError("component not found", transform.gameObject);
        Debug.LogError("wrong component source", component.gameObject);
        return null;
    }

    //======================================IDualWieldSkill=======================================
    Weapon IDualWieldSkill.GetChosenWeapon(DualWieldSkill component)
    {
        if (this.characterSkill == component)
        {
            return this.inventory.GetChosenWeapon();
        }

        Util.Instance.IComponentErrorLog(transform, component.transform);
        return null;
    }

    bool IDualWieldSkill.CanFinishSkillImmediately(DualWieldSkill component)
    {
        if (this.characterSkill == component)
        {
            if (this.inventory.GetChosenWeapon() == component.ChosenWeapon) return false;
            else return true;
        }

        Util.Instance.IComponentErrorLog(transform, component.transform);
        return false;
    }

    Vector2 IDualWieldSkill.GetWeaponTargetPos(DualWieldSkill component)
    {
        if (this.characterSkill == component)
        {
            return InputManager.Instance.MousePos;
        }

        Util.Instance.IComponentErrorLog(transform, component.transform);
        return Vector2.zero;
    }

    bool IDualWieldSkill.CanWeaponSkillRecharge(DualWieldSkill component)
    {
        if (this.characterSkill == component)
        {
            return true;
        }

        Util.Instance.IComponentErrorLog(transform, component.transform);
        return false;
    }

    int IDualWieldSkill.GetWeaponSkillState(DualWieldSkill component)
    {
        if (this.characterSkill == component)
        {
            return InputManager.Instance.LeftClickState;
        }

        Util.Instance.IComponentErrorLog(transform, component.transform);
        return -1;
    }

    void IDualWieldSkill.WeaponConsumePower(DualWieldSkill component)
    {
        if (this.characterSkill == component)
        {
            this.health -= this.inventory.GetChosenWeapon().Skill.HealthCost;
            this.mana -= this.inventory.GetChosenWeapon().Skill.ManaCost;
            return;
        }

        Util.Instance.IComponentErrorLog(transform, component.transform);
        return;
    }

    //=========================================ILookByDir=========================================
    bool ILookByDir.CanLook(LookByDir component)
    {
        return true;
    }

    Transform ILookByDir.GetMainObj(LookByDir component)
    {
        return this.model.transform;
    }

    float ILookByDir.GetXDir(LookByDir component)
    {
        return InputManager.Instance.MoveDir.x;
    }
}
