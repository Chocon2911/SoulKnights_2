using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : HuyMonoBehaviour, IDetector
{
    //==========================================Variable==========================================
    [Header("Pick Up Item")]
    [SerializeField] private InterfaceReference<IPickUpItem> user;
    [SerializeField] protected List<Detector> detectors;

    //==========================================Get Set===========================================
    public IPickUpItem User { get => this.user.Value; set => this.user.Value = value; }
    public List<Detector> Detectors { get => this.detectors; set => this.detectors = value; }

    //===========================================Unity============================================
    protected virtual void FixedUpdate()
    {
        this.PickingUp();
    }

    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadComponent(ref this.detectors, transform.Find("Detectors"), "LoadDetectors()");
        foreach (Detector detector in this.detectors) detector.User = this;
    }

    //===========================================Method===========================================
    protected virtual void PickingUp()
    {
        if (!this.user.Value.CanPickUp(this)) return;
        foreach (Detector detector in this.detectors)
        {
            if (detector.Target == null) continue;
            Weapon weapon = detector.Target.GetComponent<Weapon>();

            if (weapon == null) continue;
            this.PickUp(weapon);
            detector.ResetTarget();
            return;
        }
    }

    protected virtual void PickUp(Weapon weapon)
    {
        Inventory inventory = this.user.Value.GetInventory(this);
        weapon.User = inventory;

        if (inventory.IsFull())
        {
            inventory.GetChosenWeapon().User = null;
            WeaponSpawner.Instance.Despawn(inventory.GetChosenWeapon().transform);
            inventory.Weapons[inventory.ChosenSlot - 1] = weapon;
            // TODO: Drop prev Weapon
        }

        else
        {
            inventory.AddWeapon(weapon);
        }
    }

    //=========================================IDetector==========================================
    public bool CanDetect(Detector component)
    {
        foreach (Detector detector in this.detectors)
        {
            if (detector != component) continue;
            return this.user.Value.CanPickUp(this);
        }

        Debug.LogError("component not found", transform.gameObject);
        Debug.LogError("wrong component source", component.gameObject);
        return false;
    }
}
