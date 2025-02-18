using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventory
{
    Vector2 GetWeaponHoldPos(Inventory component);
    void ChooseWeapon(Inventory component);
    bool CanWeaponSkillRecharge(Inventory component);
    int GetWeaponSkillState(Inventory component);
    void WeaponConsumePower(Inventory component);
}

public class Inventory : HuyMonoBehaviour, IWeapon
{
    //==========================================Variable==========================================
    [Header("Inventory")]
    [SerializeField] private InterfaceReference<IInventory> user;
    [SerializeField] protected List<Weapon> weapons;
    [SerializeField] protected int chosenSlot;
    [SerializeField] protected int maxAmount;

    //==========================================Get Set===========================================
    public IInventory User { get => user.Value; set => user.Value = value; }
    public List<Weapon> Weapons { get => weapons; set => weapons = value; }
    public int ChosenSlot { get => chosenSlot; set => chosenSlot = value; }
    public int MaxAmount { get => maxAmount; set => maxAmount = value; }

    //===========================================Unity============================================
    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadComponent(ref this.weapons, transform.Find("Weapons"), "LoadWeapons");
        foreach (Weapon weapon in this.weapons)
        {
            weapon.User = this;
            weapon.gameObject.SetActive(false);
        }
    }

    protected override void OnEnable()
    {
        this.GetChosenWeapon().gameObject.SetActive(true);
    }

    protected virtual void Update()
    {
        this.UpdateWeaponSlot();
    }

    //===========================================Method===========================================
    public void AddWeapon(Weapon weapon)
    {
        if (this.weapons.Count >= this.maxAmount) return;
        this.weapons.Add(weapon);
    }

    public void RemoveWeapon(Weapon weapon)
    {
        this.weapons.Remove(weapon);
    }

    public Weapon GetChosenWeapon()
    {
        if (this.chosenSlot > this.weapons.Count) return null;
        if (this.chosenSlot < 0) return null;
        return this.weapons[this.chosenSlot - 1];
    }

    public bool IsFull()
    {
        return this.weapons.Count >= this.maxAmount;
    }

    protected virtual void UpdateWeaponSlot()
    {
        this.user.Value.ChooseWeapon(this);
    }

    //=========================================IInventory=========================================
    Vector2 IWeapon.GetTargetPos(Weapon component)
    {
        foreach (Weapon weapon in this.weapons)
        {
            if (weapon != component) continue;
            return this.user.Value.GetWeaponHoldPos(this);
        }

        Util.Instance.IComponentErrorLog(transform, component.transform);
        return Vector2.zero;
    }

    Transform IWeapon.GetOwner(Weapon component)
    {
        foreach (Weapon weapon in this.weapons)
        {
            if (weapon != component) continue;
            return this.transform;
        }

        Util.Instance.IComponentErrorLog(transform, component.transform);
        return null;
    }

    bool IWeapon.CanSkillRecharge(Weapon component)
    {
        foreach (Weapon weapon in this.weapons)
        {
            if (weapon != component) continue;
            return this.user.Value.CanWeaponSkillRecharge(this);
        }

        Util.Instance.IComponentErrorLog(transform, component.transform);
        return false;
    }

    int IWeapon.GetSkillState(Weapon component)
    {
        foreach (Weapon weapon in this.weapons)
        {
            if (weapon != component) continue;
            return this.user.Value.GetWeaponSkillState(this);
        }

        Util.Instance.IComponentErrorLog(transform, component.transform);
        return -1;
    }

    void IWeapon.ConsumePower(Weapon component)
    {
        foreach (Weapon weapon in this.weapons)
        {
            if (weapon != component) continue;
            this.user.Value.WeaponConsumePower(this);
            return;
        }

        Util.Instance.IComponentErrorLog(transform, component.transform);
        return;
    }
}
